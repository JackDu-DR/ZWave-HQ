using RabbitMQ.Client;
using System.Runtime.InteropServices;

namespace CommonLib.MessageHandler.Interface
{
    [ComVisible(false)]
    public interface IAppMessageHandler
    {
        event EventHandler<ShutdownEventArgs> OnConnectionShutdown;

        event EventHandler<EventArgs> OnConnectionRecovered;

        event EventHandler<bool> IsMachineAliveChanged;

        /// <summary>
        /// Routing key of the ping message which is used to check if Machine is alive
        /// </summary>
        string PingRoutingKey { get; }

        bool IsConnectionOpen { get; }

        bool IsMachineAlive { get; }

        /// <summary>
        /// Establish the RabbitMQ Connection: Create Connection, init queues, and channels
        /// </summary>
        /// <param name="hostName"></param>
        void EstablishConnection(string hostName);

        /// <summary>
        /// Create the connection to RabbitMQ Server
        /// </summary>
        void Connect(string hostName);

        /// <summary>
        /// Init the Channel
        /// </summary>
        void InitChannel();

        /// <summary>
        /// Init all queues: Fetching, Alarm, Stream, and General
        /// </summary>
        void InitQueues();

        /// <summary>
        /// Publish directly to RPC machine queue with specific Id and Data
        /// </summary>
        bool PublishToMachine(string id, object data);

        /// <summary>
        /// Request data from Machine through RPC queue
        /// </summary>
        Task<byte[]> Request(string id, object data, CancellationToken cancellationToken = default);

        /// <summary>
        /// Event of General Message received from Machine
        /// </summary>
        public event Action<object, byte[], string> OnGeneralMessageReceived;

        /// <summary>
        /// Subscribe the General Exchange with specific topic
        /// </summary>
        /// <returns>true if subscribing successfully</returns>
        bool SubcribeGeneral(string routingKey);

        /// <summary>
        /// Unsubscribe General Exchange with specific Routing key
        /// </summary>
        /// <param name="routingKey">Routing key of General Exchange</param>
        void UnsubcribeGeneral(string routingKey);

        /// <summary>
        /// Event of Live Camera image received from Machine
        /// </summary>
        event Action<object, byte[]> OnStreamMessageReceived;

        /// <summary>
        /// Subscribe the Stream queue to receive the Live Camera images
        /// </summary>
        /// <returns>true if subscribing successfully</returns>
        bool SubcribeStream();

        /// <summary>
        /// Unsubscribe Stream queue to stop receiving Live Camera images from Machine
        /// </summary>
        /// <param name="routingKey">Routing key of Stream Exchange</param>
        void UnsubcribeStream();

        bool SubcribeCamera1Stream();
        void UnsubcribeCamera1Stream();
        event Action<object, byte[]> OnSubcribeCamera1StreamMessageReceived;

        bool SubcribeCamera2Stream();
        void UnsubcribeCamera2Stream();
        event Action<object, byte[]> OnSubcribeCamera2StreamMessageReceived;

        bool SubcribeCamera3Stream();
        void UnsubcribeCamera3Stream();
        event Action<object, byte[]> OnSubcribeCamera3StreamMessageReceived;
    }
}
