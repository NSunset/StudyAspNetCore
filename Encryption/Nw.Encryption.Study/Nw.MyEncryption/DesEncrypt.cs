using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nw.MyEncryption
{
    public class DesEncrypt
    {
        private static byte[] _rgbKey = ASCIIEncoding.ASCII.GetBytes(Constant.DesKey.Substring(0, 8));
        private static byte[] _rgbIV = ASCIIEncoding.ASCII.GetBytes(Constant.DesKey.Insert(0, "w").Substring(0, 8));


        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Encrypt(string text)
        {
            DESCryptoServiceProvider dESCrypto = new DESCryptoServiceProvider();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCrypto.CreateEncryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);

                StreamWriter streamWriter = new StreamWriter(cryptoStream);

                //streamWriter.Write(text);
                WriteBytes(Encoding.UTF8.GetBytes(text), streamWriter.BaseStream);
                streamWriter.Flush();
                cryptoStream.FlushFinalBlock();
                memoryStream.Flush();

                return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);

            }
        }


        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="encryptText"></param>
        /// <returns></returns>
        public static string Decrypt(string encryptText)
        {
            DESCryptoServiceProvider dESCrypto = new DESCryptoServiceProvider();

            byte[] bytes = Convert.FromBase64String(encryptText);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                CryptoStream cryptoStream = new CryptoStream(memoryStream, dESCrypto.CreateDecryptor(_rgbKey, _rgbIV), CryptoStreamMode.Write);

                //cryptoStream.Write(bytes, 0, bytes.Length);
                WriteBytes(bytes, cryptoStream);
                cryptoStream.FlushFinalBlock();
                return ASCIIEncoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// 分多次写入字节到文件流
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="stream"></param>
        private static void WriteBytes(byte[] bytes,Stream stream)
        {
            int length = 1024;
            int offset = 0;
            while (bytes.Length > offset)
            {
                //cryptoStream.Position = offset;
                if (offset + length > bytes.Length)
                {
                    int count = bytes.Length - offset;
                    stream.Write(bytes, offset, count);
                }
                else
                {
                    stream.Write(bytes, offset, length);
                }
                offset += length;
            }
        }
    }
}
