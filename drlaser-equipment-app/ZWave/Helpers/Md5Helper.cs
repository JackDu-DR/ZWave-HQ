using System.Text;
using XSystem.Security.Cryptography;

namespace ZWave.Helpers
{
    public class Md5Helper
    {
        public static string HashMd5(string myString)
        {
            if (string.IsNullOrEmpty(myString))
            {
                return string.Empty;
            }

            var md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(Encoding.ASCII.GetBytes(myString));
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }
    }
}
