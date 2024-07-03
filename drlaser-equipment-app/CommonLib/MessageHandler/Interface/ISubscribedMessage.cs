using System.Runtime.InteropServices;

namespace CommonLib.MessageHandler.Interface
{
    [Guid("38D5C554-70C6-4926-BDBA-932CA6E1089C")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [ComVisible(true)]
    public interface ISubscribedMessage
    {
        string QueueName { get; set; }

        void Callback(string replyQueue, string correlationId, ulong deliveryTag, string data);
    }
}
