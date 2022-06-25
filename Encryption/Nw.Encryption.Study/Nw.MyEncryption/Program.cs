using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Nw.MyEncryption
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {


                //MD5加密信息
                {
                    //string msg = MD5Encrypt.Encrypt("你好啊");

                    //string msg1 = MD5Encrypt.Encrypt("你好啊", 16);
                    //Console.WriteLine(msg);
                    //Console.WriteLine(msg1);
                }


                //文件摘要MD5
                {
                    //string dirPath = @"E:\GitRepository\虚拟机git\Work\study\Encryption\ceshi";
                    //if (!Directory.Exists(dirPath))
                    //{
                    //    Directory.CreateDirectory(dirPath);
                    //}

                    //string filePath = Path.Combine(dirPath, "one.txt");
                    //if (!File.Exists(filePath))
                    //{
                    //    using (FileStream fileStream = File.Create(filePath))
                    //    {
                    //        string msg = "哈哈哈哈";

                    //        byte[] bytes = Encoding.UTF8.GetBytes(msg);

                    //        //fileStream.Write(bytes, 0, bytes.Length);


                    //        int length = 4;
                    //        int offset = 0;
                    //        while (bytes.Length > offset)
                    //        {
                    //            fileStream.Position = offset;
                    //            if (offset + length > bytes.Length)
                    //            {
                    //                int count = bytes.Length - offset;
                    //                fileStream.Write(bytes, offset, count);
                    //            }
                    //            else
                    //            {
                    //                fileStream.Write(bytes, offset, length);
                    //            }
                    //            offset += length;
                    //        }
                    //    }
                    //}
                    //string fileSummary1 = MD5Encrypt.AbstractFile((filePath));


                    ////都是压缩文件，内部内容不变，外部名称修改，生成的摘要相同
                    //string fileSummary2 = MD5Encrypt.AbstractFile(Path.Combine(dirPath, "one.rar"));

                    //string fileSummary3 = MD5Encrypt.AbstractFile(Path.Combine(dirPath, "one - 副本.rar"));

                    //Console.WriteLine(fileSummary1);
                    //Console.WriteLine(fileSummary2);
                    //Console.WriteLine(fileSummary3);
                }

                //DES加密，解密
                {
                    //加密
                    //string value = DesEncrypt.Encrypt("你好啊");
                    //Console.WriteLine(value);

                    ////解密
                    //string msg = DesEncrypt.Decrypt(value);
                    //Console.WriteLine(msg);

                }

                //RSA加密，解密
                {
                    //KeyValuePair<string, string> key = RsaEncrypt.GetKeyPair();
                    //string value = RsaEncrypt.Encrypt("你好啊", key.Key);

                    //string msg = RsaEncrypt.Decrypt(value, key.Value);

                    //Console.WriteLine(value);
                    //Console.WriteLine(msg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            Console.ReadLine();
        }
    }
}
