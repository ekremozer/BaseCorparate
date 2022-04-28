using System;
using System.Security.Cryptography;
using System.Text;

namespace BaseCorporate.Utility.Helper
{
    public static class CryptoHelper
    {
        private const string EncryptionKey = "66'3KR3M**-c75d4de3-f515-47a2-b5a2-dd8cd532910e-**0Z3R'66";

        public static string ToMd5(this string input)
        {
            var md5CryptoServiceProvider = new MD5CryptoServiceProvider();
            var inputArray = ConvertToByteArray(input);
            inputArray = md5CryptoServiceProvider.ComputeHash(inputArray);
            var stringBuilder = new StringBuilder();
            foreach (var arrayItem in inputArray)
            {
                stringBuilder.Append(arrayItem.ToString("x2").ToLower());
            }
            return stringBuilder.ToString();
        }

        public static string ToTripleDesMd5Encryption(string input)
        {
            var data = Encoding.UTF8.GetBytes(input);
            var md5 = new MD5CryptoServiceProvider();
            var keys = md5.ComputeHash(Encoding.UTF8.GetBytes(EncryptionKey));
            var tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
            var transform = tripDes.CreateEncryptor();
            var results = transform.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(results, 0, results.Length);
        }

        public static string ToTripleDesMd5Decrypt(string input)
        {
            var data = Convert.FromBase64String(input);
            var md5 = new MD5CryptoServiceProvider();
            var keys = md5.ComputeHash(Encoding.UTF8.GetBytes(EncryptionKey));
            var tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
            var transform = tripDes.CreateDecryptor();
            var results = transform.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetString(results);
        }

        private static byte[] ConvertToByteArray(string input)
        {
            var unicodeEncoding = new UnicodeEncoding();
            return unicodeEncoding.GetBytes(input);
        }
    }
}
