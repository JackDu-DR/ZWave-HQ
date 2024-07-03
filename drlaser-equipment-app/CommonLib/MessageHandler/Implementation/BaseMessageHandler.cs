using CommonLib.DTO.Implementation;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace CommonLib.MessageHandler.Implementation
{
    public abstract class BaseMessageHandler
    {
        protected readonly string STREAM_EXCHANGE = "StreamExchange";
        protected readonly string GENERAL_EXCHANGE = "GeneralExchange";

        protected readonly string CAMERA1_STREAM_QUEUE = "Camera1StreamQueue";
        protected readonly string CAMERA2_STREAM_QUEUE = "Camera2StreamQueue";
        protected readonly string CAMERA3_STREAM_QUEUE = "Camera3StreamQueue";

        protected readonly string STREAM_VISION_QUEUE = "VisionStreamQueue";

        protected readonly string MACHINE_QUEUE = "rpc_queue";
        protected readonly string RABBITMQ_USERNAME = "ZWave";
        protected readonly string RABBITMQ_PASSWORD = "ZWave@123";

        protected string HostName;
        protected string Username;
        protected string Password;
        protected IConnection Connection;
        protected IModel BaseChannel;

        public string PingRoutingKey => "Ping";

        public event EventHandler<ShutdownEventArgs>? OnConnectionShutdown;
        public event EventHandler<EventArgs>? OnConnectionRecovered;

        public bool IsConnectionOpen => Connection != null && Connection.IsOpen;

        public void Connect(string hostName)
        {
            var factory = new ConnectionFactory
            {
                HostName = hostName,
                Port = Protocols.DefaultProtocol.DefaultPort,
                UserName = RABBITMQ_USERNAME,
                Password = RABBITMQ_PASSWORD,
                VirtualHost = "/",
                ContinuationTimeout = new TimeSpan(10, 0, 0, 0),
                RequestedHeartbeat = new TimeSpan(0, 0, 5),
                RequestedConnectionTimeout = new TimeSpan(0, 0, 5)
            };

            //In case the connection cannot be established, the popup would be shown up to ask user Retry manually
            //https://sioux.atlassian.net/browse/DRL-215
            Connection = factory.CreateConnection();
            Connection.ConnectionShutdown += Connection_ConnectionShutdown;

            if (Connection is IAutorecoveringConnection connection)
            {
                connection.RecoverySucceeded += Connection_RecoverySucceeded;
            }
        }

        private void Connection_RecoverySucceeded(object? sender, EventArgs e)
        {
            OnConnectionRecovered?.Invoke(sender, e);
        }

        private void Connection_ConnectionShutdown(object? sender, ShutdownEventArgs e)
        {
            OnConnectionShutdown?.Invoke(sender, e);
        }

        public virtual void InitChannel()
        {
            BaseChannel = Connection.CreateModel();
        }

        protected byte[] GetBytesArray(string id, object data)
        {
            var serializedData = JsonConvert.SerializeObject(data);

            var message = new Message()
            {
                Id = id,
                Data = serializedData
            };

            var serializedMessage = JsonConvert.SerializeObject(message);

            return Encoding.UTF8.GetBytes(serializedMessage);
        }

        protected byte[] GetBytesArray(object data)
        {
            var serializedData = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(serializedData);
        }
    }
}
