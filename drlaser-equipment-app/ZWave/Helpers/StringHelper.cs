using System.Text;

namespace ZWave.Helpers
{
    public static class StringHelper
    {
        public static string ByteArrayToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
