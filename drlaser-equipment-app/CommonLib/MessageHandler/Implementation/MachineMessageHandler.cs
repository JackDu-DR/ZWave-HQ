using CommonLib.MessageHandler.Interface;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime.InteropServices;
using System.Text;

namespace CommonLib.MessageHandler.Implementation
{
    [Guid("81A3D110-2B41-440D-BAE7-1A93887BB22C")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class MachineMessageHandler : BaseMessageHandler, IMachineMessageHandler
    {
        public string MachineQueue => MACHINE_QUEUE;

        public void Subscribe(ISubscribedMessage subscribedMessage)
        {
            BaseChannel.QueueDeclare(queue: subscribedMessage.QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var consumer = new EventingBasicConsumer(BaseChannel);
            BaseChannel.BasicConsume(queue: subscribedMessage.QueueName,
                                 autoAck: false,
                                 consumer: consumer);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var props = ea.BasicProperties;
                var replyProps = BaseChannel.CreateBasicProperties();
                replyProps.CorrelationId = props.CorrelationId;

                subscribedMessage.Callback(props.ReplyTo, replyProps.CorrelationId, ea.DeliveryTag, message);
            };
        }

        public void Reply(string queueName, string correlationId, ulong deliveryTag, string id, object data)
        {
            var bytes = GetBytesArray(id, data);
            Reply(queueName, correlationId, deliveryTag, bytes);
        }

        public void Reply(string queueName, string correlationId, ulong deliveryTag, byte[] data)
        {
            try
            {
                var replyProps = BaseChannel.CreateBasicProperties();
                replyProps.CorrelationId = correlationId;

                BaseChannel.BasicPublish(exchange: string.Empty,
                                     routingKey: queueName,
                                     basicProperties: replyProps,
                                     body: data);

                BaseChannel.BasicAck(deliveryTag, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void BasicAck(ulong deliveryTag, bool multiple)
        {
            BaseChannel.BasicAck(deliveryTag, multiple);
        }

        public void PublishToGeneral(object data, string routingKey)
        {
            var bytesArray = GetBytesArray(data);
            Console.WriteLine(routingKey);
            try
            {
                using var channel = Connection.CreateModel();
                channel.ExchangeDeclare(exchange: GENERAL_EXCHANGE, type: ExchangeType.Topic);

                channel.BasicPublish(exchange: GENERAL_EXCHANGE,
                                     routingKey: routingKey,
                                     basicProperties: null,
                                     body: bytesArray);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void PublishToStream(object data)
        {
            var bytesArray = GetBytesArray(data);
            try
            {
                using var channel = Connection.CreateModel();
                channel.BasicPublish(exchange: "", routingKey: STREAM_VISION_QUEUE, basicProperties: null, body: bytesArray);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void PublishToCamera1Stream(byte[] data)
        {
            PublishToStreamQueue(data, CAMERA1_STREAM_QUEUE);
        }

        public void PublishToCamera2Stream(byte[] data)
        {
            PublishToStreamQueue(data, CAMERA2_STREAM_QUEUE);
        }

        public void PublishToCamera3Stream(byte[] data)
        {
            PublishToStreamQueue(data, CAMERA3_STREAM_QUEUE);
        }

        private void PublishToStreamQueue(byte[] data, string queueName)
        {
            try
            {
                using var channel = Connection.CreateModel();
                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
