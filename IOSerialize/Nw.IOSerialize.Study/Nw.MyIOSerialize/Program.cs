using System;
using System.IO;
using System.Text;

namespace Nw.MyIOSerialize
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {


                //文件夹操作
                {
                    //string test = @"E:\GitRepository\虚拟机git\Work\study\IOSerialize\test";
                    ////检查test文件夹是否存在
                    //if (Directory.Exists(test))
                    //{
                    //    Console.WriteLine("文件夹存在");
                    //}
                    //else
                    //{
                    //    Console.WriteLine("文件夹不存在");
                    //}

                    //获取文件夹描述信息,文件夹不存在不会报错
                    // DirectoryInfo directoryInfo = new DirectoryInfo(test);
                    //获取去文件夹里面的文件集合，文件夹不存在会报错
                    //FileInfo[] fileInfos = directoryInfo.GetFiles();

                    //创建文件夹，根据路径创建,创建全部子路径
                    {
                        //string path = Path.Combine(test, "01test");
                        //if (!Directory.Exists(path))
                        //{
                        //    Directory.CreateDirectory(path);
                        //}
                    }

                    //移动，原路径文件夹就不存在了
                    {
                        //string path = Path.Combine(test, "01test");

                        //if (Directory.Exists(path))
                        //{
                        //    string newPath = @"E:\GitRepository\虚拟机git\Work\study\IOSerialize\01test";
                        //    if (!Directory.Exists(newPath))
                        //    {
                        //        Directory.Move(path, newPath);
                        //    }                            
                        //}                        
                    }

                    //删除文件夹，如果文件夹内部有内容，无法删除,指定recursive为true可以递归删除
                    {
                        //string path = Path.Combine(test);

                        //if (Directory.Exists(path))
                        //{
                        //    Directory.Delete(path,true);
                        //}                        
                    }


                }

                //文件操作
                {

                    string fileDirPath = Path.Combine(@"E:\GitRepository\虚拟机git\Work\study\IOSerialize", @"test");

                    string filePath = Path.Combine(fileDirPath, "one.txt");
                    //检查文件是否存在
                    {
                        if (File.Exists(filePath))
                        {
                            Console.WriteLine("文件存在");
                        }
                        else
                        {
                            Console.WriteLine("文件不存在");
                        }
                    }

                    //获取文件描述对象,文件不存在不报错
                    {
                        FileInfo fileInfo = new FileInfo(filePath);
                    }

                    //创建文件
                    {


                        if (!File.Exists(filePath))
                        {

                            if (!Directory.Exists(fileDirPath))
                            {
                                Directory.CreateDirectory(fileDirPath);
                            }

                            //文件夹路径不存在，创建文件报错
                            using (FileStream fileStream = File.Create(filePath))
                            {
                                string str = "1111111";

                                byte[] data = Encoding.UTF8.GetBytes(str);
                                fileStream.Write(data);
                            }

                        }

                        //文件写入
                        if (File.Exists(filePath))
                        {
                            //当文件已存在时，这样写，会吧原来内容覆盖
                            {
                                //using (FileStream fileStream = File.Create(filePath))
                                //{
                                //    string str = "333";

                                //    byte[] data = Encoding.UTF8.GetBytes(str);
                                //    fileStream.Write(data);
                                //}
                            }

                            //原有内容后面追加内容
                            {
                                //using (StreamWriter streamWriter = File.AppendText(filePath))
                                //{
                                //    //streamWriter.WriteLine("哈哈哈哈哈");//后面追加

                                //    //streamWriter.Write("你好啊");//换行写

                                //    //推荐使用流写入
                                //    byte[] data = Encoding.Default.GetBytes("你好啊");
                                //    streamWriter.BaseStream.Write(data,0,data.Length);
                                //    streamWriter.Flush();//后面追加
                                //}
                            }

                        }


                        //文件读取
                        {
                            //一次性读取所有
                            {
                                //foreach (string item in File.ReadAllLines(filePath))
                                //{
                                //    Console.WriteLine(item);
                                //}

                                ////直接读取所有
                                //string msg = File.ReadAllText(filePath);
                                //Console.WriteLine(msg);

                                ////直接读取所有字节
                                //byte[] bytes = File.ReadAllBytes(filePath);
                                //string msg1 = Encoding.UTF8.GetString(bytes);
                                //Console.WriteLine(msg1);
                            }

                            //分批读取
                            {
                                //using (FileStream fileStream = File.OpenRead(filePath))
                                //{
                                //    int length = 100;
                                //    byte[] bytes = new byte[length];
                                //    while (fileStream.Read(bytes, 0, length) > 0)
                                //    {
                                //        string msg = Encoding.UTF8.GetString(bytes);
                                //        Console.WriteLine(msg);
                                //    }
                                //}
                            }
                        }


                    }


                    //复制,移动，删除文件
                    {
                        ////复制文件
                        //if (File.Exists(filePath))
                        //{
                        //    string copyFilePath = Path.Combine(fileDirPath, "two.txt");
                        //    File.Copy(filePath, copyFilePath);
                        //}

                        ////移动文件
                        //if (File.Exists(filePath))
                        //{
                        //    string moveFilePath = Path.Combine(fileDirPath, "three.txt");
                        //    File.Move(filePath, moveFilePath);
                        //}

                        //删除文件
                        //if (File.Exists(Path.Combine(fileDirPath, "two.txt")))
                        //{
                        //    File.Delete(Path.Combine(fileDirPath, "two.txt"));
                        //}
                    }
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
