using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Nw.MyEncryption
{
    public class MD5Encrypt
    {
        /// <summary>
        /// MD5方式加密
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="length">密文长度16/32</param>
        /// <returns></returns>
        public static string Encrypt(string source, int length = 32)
        {
            if (string.IsNullOrWhiteSpace(source)) return string.Empty;

            HashAlgorithm hashAlgorithm = CryptoConfig.CreateFromName("MD5") as HashAlgorithm;
            byte[] bytes = Encoding.UTF8.GetBytes(source);

            byte[] hashValue = hashAlgorithm.ComputeHash(bytes);

            StringBuilder stringBuilder = new StringBuilder(); 
            switch (length)
            {
                case 16://16位密文是32位密文的9到24位字符
                    for (int i = 4; i < 12; i++)
                    {
                        stringBuilder.Append(hashValue[i].ToString("x2"));
                    }
                    break;
                case 32:
                    for (int i = 0; i < 16; i++)
                    {
                        stringBuilder.Append(hashValue[i].ToString("x2"));
                    }
                    break;
                default:
                    for (int i = 0; i < hashValue.Length; i++)
                    {
                        stringBuilder.Append(hashValue[i].ToString("x2"));
                    }
                    break;
            }
            return stringBuilder.ToString();

        }


        /// <summary>
        /// 获取文件摘要
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string AbstractFile(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return string.Empty;
            using (FileStream reader = new FileStream(fileName,FileMode.Open))
            {
                return AbstractFile(reader);
            }
        }

        /// <summary>
        /// 根据文件流获取文件摘要
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static string AbstractFile(Stream stream)
        {
            MD5 mD5 = new MD5CryptoServiceProvider();

            byte[] retVal = mD5.ComputeHash(stream);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
