using System.Text;
using RabbitMQ.Client;
using System.Collections.Concurrent;
using RabbitMQ.Client.Events;
using CommonLib.MessageHandler.Interface;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CommonLib.MessageHandler.Implementation
{
    [ComVisible(false)]
    public class AppMessageHandler : BaseMessageHandler, IAppMessageHandler
    {
        private readonly int MACHINE_HEARTBEAT_TIMESPAN = 5000;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<byte[]>> _callbackMapper = new ConcurrentDictionary<string, TaskCompletionSource<byte[]>>();

        private IModel _streamChannel;
        private string _fetchingQueue;
        private string _generalQueue;

        private System.Timers.Timer _timer;

        private bool _isMachineAlive;
        public bool IsMachineAlive
        {
            get { return _isMachineAlive; }
            set
            {
                if (_isMachineAlive != value || (value == false && !_alreadyInvoked))
                {
                    _isMachineAlive = value;

                    IsMachineAliveChanged?.Invoke(this, value);
                    _alreadyInvoked = true;
                }
            }
        }

        public AppMessageHandler()
        {
            _fetchingQueue = Guid.NewGuid().ToString();
            _generalQueue = Guid.NewGuid().ToString();

            _timer = new System.Timers.Timer(MACHINE_HEARTBEAT_TIMESPAN);
            _timer.Elapsed += OnTimerElapsed;
            OnGeneralMessageReceived += AppMessageHandler_OnGeneralMessageReceived;
        }

        public event EventHandler<bool> IsMachineAliveChanged;
        private void AppMessageHandler_OnGeneralMessageReceived(object sender, byte[] byteArray, string routingKey)
        {
            if (routingKey == PingRoutingKey)
            {
                UnsubcribeGeneral(PingRoutingKey);
                _isPinging = false;
                IsMachineAlive = true;
            }
        }

        private bool _alreadyInvoked;
        private bool _isPinging;
        private void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (!IsConnectionOpen)
            {
                return;
            }

            if (!_isPinging)
            {
                SubcribeGeneral(PingRoutingKey);
                _isPinging = true;
            }
            else
            {
                IsMachineAlive = false;
            }

            PublishToMachine(PingRoutingKey, string.Empty);
        }

        public void InitQueues()
        {
            BaseChannel.ExchangeDeclare(exchange: GENERAL_EXCHANGE, type: ExchangeType.Topic);

            InitFetchingQueue();
            _generalQueue = BaseChannel.QueueDeclare(_generalQueue).QueueName;
        }

        private void InitFetchingQueue()
        {
            _fetchingQueue = BaseChannel.QueueDeclare(_fetchingQueue).QueueName;
            var consumer = new EventingBasicConsumer(BaseChannel);
            consumer.Received += (model, ea) =>
            {
                if (_callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcsr))
                {
                    tcsr.TrySetResult(ea.Body.ToArray());
                }

                _timer.Stop();
                _timer.Start();
            };

            BaseChannel.BasicConsume(consumer: consumer, queue: _fetchingQueue, autoAck: true);
        }

        public override void InitChannel()
        {
            base.InitChannel();
            _streamChannel = Connection.CreateModel();
            _streamChannel.BasicQos(0, 10, false);

            _streamCamera1Channel = Connection.CreateModel();
            _streamCamera1Channel.BasicQos(0, 10, false);
            _streamCamera2Channel = Connection.CreateModel();
            _streamCamera2Channel.BasicQos(0, 10, false);
            _streamCamera3Channel = Connection.CreateModel();
            _streamCamera3Channel.BasicQos(0, 10, false);
        }

        public void EstablishConnection(string hostName)
        {
            Connect(hostName);
            InitChannel();
            InitQueues();

            _timer.Stop();
            _timer.Start();
        }

        public Task<byte[]> Request(string id, object data, CancellationToken cancellationToken = default)
        {
            var correlationId = Guid.NewGuid().ToString();

            var props = BaseChannel.CreateBasicProperties();
            props.CorrelationId = correlationId;
            props.ReplyTo = _fetchingQueue;

            var messageBytes = GetBytesArray(id, data);
            var tcs = new TaskCompletionSource<byte[]>();
            _callbackMapper.TryAdd(correlationId, tcs);

            BaseChannel.BasicPublish(exchange: string.Empty, routingKey: MACHINE_QUEUE, basicProperties: props, body: messageBytes);

            cancellationToken.Register(() => _callbackMapper.TryRemove(correlationId, out _));

            return tcs.Task;
        }

        public bool PublishToMachine(string id, object data)
        {
            var bytes = GetBytesArray(id, data);
            var result = true;
            try
            {
                using var channel = Connection.CreateModel();
                channel.BasicPublish(exchange: string.Empty, routingKey: MACHINE_QUEUE, basicProperties: null, body: bytes);
            }
            catch (Exception ex)
            {
                result = false;
                Debug.WriteLine(ex);
            }

            return result;
        }

        public event Action<object, byte[], string> OnGeneralMessageReceived;
        public bool SubcribeGeneral(string routingKey)
        {
            var result = false;
            try
            {
                BaseChannel.QueueBind(queue: _generalQueue, exchange: GENERAL_EXCHANGE, routingKey: routingKey);

                var consumer = new EventingBasicConsumer(BaseChannel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;

                    OnGeneralMessageReceived?.Invoke(this, ea.Body.ToArray(), routingKey);
                    _timer.Stop();
                    _timer.Start();
                };
                BaseChannel.BasicConsume(queue: _generalQueue, autoAck: true, consumer: consumer);

                result = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return result;
        }

        public void UnsubcribeGeneral(string routineKey)
        {
            BaseChannel.QueueUnbind(queue: _generalQueue, exchange: GENERAL_EXCHANGE, routingKey: routineKey);
        }

        public event Action<object, byte[]> OnStreamMessageReceived;

        private EventingBasicConsumer _consumerVisionStream;
        public bool SubcribeStream()
        {
            var result = false;
            try
            {
                _streamChannel.QueueDeclare(queue: STREAM_VISION_QUEUE, durable: true, exclusive: false, autoDelete: false, arguments: STREAM_QUEUE_ARGUMENTS);

                // Create a consumer to receive messages
                _consumerVisionStream = new EventingBasicConsumer(_streamChannel);
                _consumerVisionStream.Received += (model, ea) =>
                {
                    _streamChannel.BasicAck(ea.DeliveryTag, false);

                    Task.Run(() =>
                    {
                        var body = ea.Body.ToArray();
                        OnStreamMessageReceived?.Invoke(this, body);
                    });

                    _timer?.Stop();
                    _timer?.Start();
                };

                // Start consuming messages from the stream queue
                _streamChannel.BasicConsume(queue: STREAM_VISION_QUEUE, autoAck: false, "", STREAM_QUEUE_ARGUMENTS, consumer: _consumerVisionStream);
                result = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return result;
        }

        public void UnsubcribeStream()
        {
            _consumerVisionStream?.ConsumerTags?.ToList().ForEach(tag =>
            {
                _streamChannel?.BasicCancel(tag);
            });
        }

        public event Action<object, byte[]> OnSubcribeCamera1StreamMessageReceived;
        public event Action<object, byte[]> OnSubcribeCamera2StreamMessageReceived;
        public event Action<object, byte[]> OnSubcribeCamera3StreamMessageReceived;

        private IModel _streamCamera1Channel;
        private IModel _streamCamera2Channel;
        private IModel _streamCamera3Channel;

        private EventingBasicConsumer _consumerCamera1;
        private EventingBasicConsumer _consumerCamera2;
        private EventingBasicConsumer _consumerCamera3;

        private static readonly Dictionary<string, object> STREAM_QUEUE_ARGUMENTS = new Dictionary<string, object>
                {
                    { "x-queue-type", "stream" },
                    { "x-max-age", "1s" },
                    { "max-length-bytes", 2000 }
                };

        public bool SubcribeCamera1Stream()
        {
            var result = false;
            try
            {
                _streamCamera1Channel.QueueDeclare(queue: CAMERA1_STREAM_QUEUE, durable: true, exclusive: false, autoDelete: false, arguments: STREAM_QUEUE_ARGUMENTS);

                // Create a consumer to receive messages
                _consumerCamera1 = new EventingBasicConsumer(_streamCamera1Channel);
                _consumerCamera1.Received += (model, ea) =>
                {
                    _streamCamera1Channel.BasicAck(ea.DeliveryTag, false);

                    Task.Run(() =>
                    {
                        var body = ea.Body.ToArray();
                        OnSubcribeCamera1StreamMessageReceived?.Invoke(this, body);
                    });
                };

                // Start consuming messages from the stream queue
                _streamCamera1Channel.BasicConsume(queue: CAMERA1_STREAM_QUEUE, autoAck: false, "", STREAM_QUEUE_ARGUMENTS, consumer: _consumerCamera1);
                result = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return result;
        }

        public bool SubcribeCamera2Stream()
        {
            var result = false;
            try
            {
                _streamCamera2Channel.QueueDeclare(queue: CAMERA2_STREAM_QUEUE, durable: true, exclusive: false, autoDelete: false, arguments: STREAM_QUEUE_ARGUMENTS);

                _consumerCamera2 = new EventingBasicConsumer(_streamCamera2Channel);
                _consumerCamera2.Received += (model, ea) =>
                {
                    _streamCamera2Channel.BasicAck(ea.DeliveryTag, false);

                    Task.Run(() =>
                    {
                        var body = ea.Body.ToArray();
                        OnSubcribeCamera2StreamMessageReceived?.Invoke(this, body);
                    });
                };

                _streamCamera2Channel.BasicConsume(queue: CAMERA2_STREAM_QUEUE, autoAck: false, "", STREAM_QUEUE_ARGUMENTS, consumer: _consumerCamera2);
                result = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return result;
        }

        public bool SubcribeCamera3Stream()
        {
            var result = false;
            try
            {
                _streamCamera3Channel.QueueDeclare(queue: CAMERA3_STREAM_QUEUE, durable: true, exclusive: false, autoDelete: false, arguments: STREAM_QUEUE_ARGUMENTS);

                _consumerCamera3 = new EventingBasicConsumer(_streamCamera3Channel);
                _consumerCamera3.Received += (model, ea) =>
                {
                    _streamCamera3Channel.BasicAck(ea.DeliveryTag, false);

                    Task.Run(() =>
                    {
                        var body = ea.Body.ToArray();
                        OnSubcribeCamera3StreamMessageReceived?.Invoke(this, body);
                    });
                };

                _streamCamera3Channel.BasicConsume(queue: CAMERA3_STREAM_QUEUE, autoAck: false, "", STREAM_QUEUE_ARGUMENTS, consumer: _consumerCamera3);
                result = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return result;
        }

        public void UnsubcribeCamera1Stream()
        {
            _consumerCamera1?.ConsumerTags?.ToList().ForEach(tag =>
            {
                _streamCamera1Channel?.BasicCancel(tag);
            });
        }

        public void UnsubcribeCamera2Stream()
        {
            _consumerCamera2?.ConsumerTags?.ToList().ForEach(tag =>
            {
                _streamCamera2Channel?.BasicCancel(tag);
            });
        }

        public void UnsubcribeCamera3Stream()
        {
            _consumerCamera3?.ConsumerTags?.ToList().ForEach(tag =>
            {
                _streamCamera3Channel?.BasicCancel(tag);
            });
        }
    }
}
