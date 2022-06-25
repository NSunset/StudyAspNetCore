using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Nw.LiveReceptionManage.WebApi.JwtHelper
{
    public class RsaHelper
    {
        /// <summary>
        /// 从本地文件中读取用来签发 Token 的 RSA Key
        /// </summary>
        /// <param name="filePath">存放密钥的文件夹路径</param>
        /// <param name="RSA"></param>
        /// <returns></returns>
        public static bool TryGetKeyParameters(string filePath, bool isPrivateKey, out RSA rSA)
        {
            rSA = default(RSA);
            if (!File.Exists(filePath))
            {
                return false;
            }
            string key = File.ReadAllText(filePath);
            key = key.Replace("-----BEGIN RSA PRIVATE KEY-----", "").Replace("-----END RSA PRIVATE KEY-----", "").Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace("\r\n", "");

            rSA = RSA.Create();
            if (isPrivateKey)
            {
                rSA.ImportRSAPrivateKey(Convert.FromBase64String(key), out _);
            }
            else
            {
                rSA.ImportSubjectPublicKeyInfo(Convert.FromBase64String(key), out _);
            }
            return true;
        }
    }
}
