using CommonLib.Enums;
using System.Runtime.InteropServices;

namespace CommonLib.MessageHandler.Interface
{
    [Guid("EFBAD983-F985-4B51-A77B-C92B411D0E28")]
    [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    [ComVisible(true)]
    public interface IMachineMessageHandler
    {
        string MachineQueue { get; }

        bool IsConnectionOpen { get; }

        /// <summary>
        /// Routing key of the ping message which is used to check if Machine is alive
        /// </summary>
        string PingRoutingKey { get; }

        void Connect(string hostName);

        void InitChannel();

        /// <summary>
        /// Subscribe the Machine queue
        /// </summary>
        void Subscribe(ISubscribedMessage subscribedMessage);

        /// <summary>
        /// Reply the fetching object message
        /// </summary>
        void Reply(string replyQueue, string correlationId, ulong deliveryTag, string id, object data);

        /// <summary>
        /// Reply the fetching byte array message
        /// </summary>
        void Reply(string replyQueue, string correlationId, ulong deliveryTag, byte[] data);

        /// <summary>
        /// Manually acknowledge the message by deliveryTag
        /// </summary>
        void BasicAck(ulong deliveryTag, bool multiple);

        /// <summary>
        /// Publish message through General Exchange
        /// </summary>
        void PublishToGeneral(object data, string routingKey);

        /// <summary>
        /// Publish message through Stream Exchange
        /// </summary>
        void PublishToStream(object data);

        /// <summary>
        /// Publish message through Stream Exchange
        /// </summary>
        void PublishToCamera1Stream(byte[] data);

        /// <summary>
        /// Publish message through Stream Exchange
        /// </summary>
        void PublishToCamera2Stream(byte[] data);

        /// <summary>
        /// Publish message through Stream Exchange
        /// </summary>
        void PublishToCamera3Stream(byte[] data);
    }
}
