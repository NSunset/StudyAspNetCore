using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Nw.MyThread
{
    class Program
    {
        static void Main(string[] args)
        {
            //通过委托的异步回调创建一个线程执行
            {
                Action<string> action = (a) =>
                {
                    Console.WriteLine($"输出信息：{a}");
                };

                AsyncCallback callback = result =>
                {
                    Console.WriteLine($"执行回调操作，传进来的参数是：{result.AsyncState}");
                };

                //委托的异步回调，core不支持。只能在.netFeromWork使用
                //action.BeginInvoke("你好", callback, "这个是回调委托需要的参数");
                //action.EndInvoke(null);

                Func<string> func = () =>
                {
                    return "你好啊";
                };

                AsyncCallback asyncCallback = result =>
                {
                    string msg = func.EndInvoke(result);
                    Console.WriteLine($"执行回调操作的返回值是：{msg}，执行回调方法,传进来的参数是：{result.AsyncState}");
                };

                //委托的异步回调，core不支持。只能在.netFeromWork使用
                //IAsyncResult asyncResult = func.BeginInvoke(asyncCallback, "这个是回调委托需要的参数");
                //func.EndInvoke(asyncResult);

                //获取委托的返回值，func.EndInvoke(asyncResult)只能执行一次，如果指定两次会报错
                //string msg = func.EndInvoke(asyncResult);


            }

            //Task.Wait()阻塞主线程,等待线程完成
            {
                //Task task = new Task(() =>
                //{
                //    Thread.Sleep(3000);
                //    Console.WriteLine("新开线程执行任务");
                //});
                //task.Start();

                //Console.WriteLine("主线程任务");
                //task.Wait();

                //Console.WriteLine("主线程任务1");
            }

            //Task.WaitAll();Task.WaitAny() 阻塞主线程
            {
                //List<Task> tasks = new List<Task>();

                //Task task1 = Task.Run(() =>
                // {
                //     Thread.Sleep(1000);
                //     Console.WriteLine("新开线程执行任务1");
                // });

                //Task task2 = Task.Run(() =>
                //{
                //    Thread.Sleep(2000);
                //    Console.WriteLine("新开线程执行任务2");
                //});

                //Task task3 = Task.Run(() =>
                //{
                //    Thread.Sleep(3000);
                //    Console.WriteLine("新开线程执行任务3");
                //});

                //tasks.Add(task1);
                //tasks.Add(task2);
                //tasks.Add(task3);

                //Task.WaitAll()阻塞主线程,等待所有线程执行完成，在执行主线程
                {
                    //Console.WriteLine("主线程任务");
                    //Task.WaitAll(tasks.ToArray());
                    //Console.WriteLine("主线程任务1");
                }

                //Task.WaitAny()阻塞主线程，有一个线程完成，就执行主线程
                {
                    //Console.WriteLine("主线程任务");
                    //Task.WaitAny(tasks.ToArray());
                    //Console.WriteLine("主线程任务1");
                }
            }

            //Task.Delay(), 不阻塞主线程，并且等待指定时间后执行任务
            {

                //Task.Delay(2000).ContinueWith(task =>
                //{
                //    Console.WriteLine("新开线程执行任务");
                //});
                //Console.WriteLine("主线程任务");
            }

            //Task.WhenAll();Task.WhenAny() 不阻塞主线程
            {
                //List<Task> tasks = new List<Task>();

                //Task task1 = Task.Run(() =>
                // {
                //     Thread.Sleep(1000);
                //     Console.WriteLine("新开线程执行任务1");
                // });

                //Task task2 = Task.Run(() =>
                //{
                //    Thread.Sleep(2000);
                //    Console.WriteLine("新开线程执行任务2");
                //});

                //Task task3 = Task.Run(() =>
                //{
                //    Thread.Sleep(3000);
                //    Console.WriteLine("新开线程执行任务3");
                //});

                //tasks.Add(task1);
                //tasks.Add(task2);
                //tasks.Add(task3);

                //Task.WhenAll不阻塞主线程，可以获取表示所有提供的任务的完成情况的任务。
                {
                    //创建一个任务，该任务将在数组中的所有 Task 对象都已完成时完成
                    //返回：表示所有提供的任务的完成情况的任务。

                    //Console.WriteLine("主线程任务");

                    //Task task = Task.WhenAll(tasks.ToArray());

                    //task.ContinueWith(t =>
                    //{
                    //    //当所有线程执行完后执行任务;
                    //    Console.WriteLine("当所有线程执行完后执行任务");
                    //});
                    //Console.WriteLine("主线程任务1");
                }

                //Task.WhenAny不阻塞主线程,可以获取表示提供的任务之一已完成的任务。 返回任务的结果是完成的任务
                {

                    //任何提供的任务已完成时，创建将完成的任务
                    //返回：表示提供的任务之一已完成的任务。 返回任务的结果是完成的任务
                    //Console.WriteLine("主线程任务");
                    //Task<Task> task = Task.WhenAny(tasks.ToArray());

                    //task.ContinueWith(t =>
                    //{
                    //    //当一个线程执行完后执行任务;
                    //    Console.WriteLine("当一个线程执行完后执行任务");
                    //});
                    //Console.WriteLine("主线程任务1");
                }
            }

            //TaskFactory.ContinueWhenAll；TaskFactory.ContinueWhenAny  不阻塞主线程
            {
                //TaskFactory taskFactory = Task.Factory;

                //List<Task> tasks = new List<Task>();

                //Task task1 = taskFactory.StartNew(() =>
                // {
                //     Thread.Sleep(1000);
                //     Console.WriteLine($"新开线程执行任务1；id={Task.CurrentId}");
                // });

                //Task task2 = taskFactory.StartNew(() =>
                //{
                //    Thread.Sleep(2000);
                //    Console.WriteLine($"新开线程执行任务2；id={Task.CurrentId}");
                //});

                //Task task3 = taskFactory.StartNew(() =>
                //{
                //    Thread.Sleep(3000);
                //    Console.WriteLine($"新开线程执行任务3；id={Task.CurrentId}");
                //});

                //tasks.Add(task1);
                //tasks.Add(task2);
                //tasks.Add(task3);

                ////ContinueWhenAll，不阻塞主线程，当所有线程执行完成时，执行任务
                //{
                //    Console.WriteLine("主线程执行");
                //    taskFactory.ContinueWhenAll(tasks.ToArray(), tasks =>
                //     {
                //         IEnumerable<int> arrId = tasks.ToList().Select(a => a.Id);
                //         string taskIds = string.Join(",", arrId);

                //         Console.Write($"所有线程执行完成后执行任务;所有执行完成的线程ID={taskIds}");
                //     });

                //    Console.WriteLine("主线程执行1");
                //}

                ////ContinueWhenAny，不阻塞主线程，当有一个线程执行完成时，执行任务
                //{
                //    Console.WriteLine("主线程执行");
                //    taskFactory.ContinueWhenAny(tasks.ToArray(), t =>
                //     {
                //         Console.Write($"只要有一个线程执行完成后执行任务；t={t.Id}就是那个执行完成的线程");
                //     });

                //    Console.WriteLine("主线程执行1");
                //}

            }


            //控制线程数量
            {
                //Console.WriteLine("主线程开始");

                ////Paraller阻塞主线程
                ////可以通过指定ParallelOptions来限制开启线程的个数
                ////可通过包一层解决线程阻塞问题
                //Task.Run(() =>
                //{
                //    ParallelOptions parallelOptions = new ParallelOptions();
                //    parallelOptions.MaxDegreeOfParallelism = 5;
                //    Parallel.For(0, 100, parallelOptions, i =>
                //    {
                //        Console.WriteLine($"新开线程执行:i={i};ID={Task.CurrentId}");
                //    });
                //});


                //Console.WriteLine("主线程结束");
            }


            //线程异常处理,直接try无法捕捉线程异常
            {
                //List<Task> tasks = new List<Task>();

                //try
                //{
                //    for (int i = 0; i < 100; i++)
                //    {
                //        int k = i;
                //        tasks.Add(Task.Run(() =>
                //        {
                //            if (k == 13)
                //            {
                //                throw new Exception($"报错了k={k}");
                //            }

                //            if (k == 18)
                //            {
                //                throw new Exception($"报错了k={k}");
                //            }

                //            if (k == 21)
                //            {
                //                throw new Exception($"报错了k={k}");
                //            }
                //            Console.WriteLine($"新开线程执行:k={k};");
                //        }));
                //    }

                //    //通过Task.WaitAll()等待加上try可以捕获异常信息
                //    Task.WaitAll(tasks.ToArray());
                //}
                ////线程异常类型
                //catch (AggregateException ex)
                //{
                //    foreach (var item in ex.InnerExceptions)
                //    {
                //        Console.WriteLine(item.Message);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
                
            }

            //线程取消
            {
                //以下线程有一个异常了，就不开启新的线程了，并且把开启的线程结束
                //List<Task> tasks = new List<Task>();

                //try
                //{
                //    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                //    for (int i = 0; i < 100; i++)
                //    {
                //        int k = i;
                //        tasks.Add(Task.Run(() =>
                //        {
                //            if (!cancellationTokenSource.IsCancellationRequested)
                //            {
                //                Console.WriteLine($"新开线程k={k}开始执行;");
                //            }
                           
                //            if (k == 13)
                //            {
                //                cancellationTokenSource.Cancel();
                //                throw new Exception($"报错了k={k}");
                //            }

                //            if (k == 18)
                //            {
                //                cancellationTokenSource.Cancel();
                //                throw new Exception($"报错了k={k}");
                //            }

                //            if (k == 21)
                //            {
                //                cancellationTokenSource.Cancel();
                //                throw new Exception($"报错了k={k}");
                //            }

                //            if (!cancellationTokenSource.IsCancellationRequested)
                //            {
                //                Console.WriteLine($"新开线程k={k}结束;");
                //            }
                //        }, cancellationTokenSource.Token));
                //    }

                //    //通过Task.WaitAll()等待加上try可以捕获异常信息
                //    Task.WaitAll(tasks.ToArray());
                //}
                ////线程异常类型
                //catch (AggregateException ex)
                //{
                //    foreach (var item in ex.InnerExceptions)
                //    {
                //        Console.WriteLine(item.Message);
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
            }

            //线程安全
            {
                {
                    //线程安全，单线程下执行结果和多线程下执行结果不一致。即线程不安全
                    //多线程操作，中间可能操作同时进行
                    //int result = 0;
                    //List<Task> tasks = new List<Task>();
                    //for (int i = 0; i < 100; i++)
                    //{
                    //    tasks.Add(Task.Run(() =>
                    //    {
                    //        result++;
                    //    }));
                    //}
                    //Task.WaitAll(tasks.ToArray());

                    //Console.WriteLine($"result={result}");
                }

                //线程安全结构
                {
                    ////list线程不安全，结果不是100
                    ////List<int> list = new List<int>();

                    ////以下都是线程安全结构

                    ////集合
                    //BlockingCollection<int> blockingList = new BlockingCollection<int>();

                    ////字典
                    //ConcurrentDictionary<int, int> keyValues = new ConcurrentDictionary<int, int>();

                    ////队列
                    //ConcurrentQueue<int> queue = new ConcurrentQueue<int>();

                    ////栈
                    //ConcurrentStack<int> stack = new ConcurrentStack<int>();

                    ////包
                    //ConcurrentBag<int> bag = new ConcurrentBag<int>();

                    //List<Task> tasks = new List<Task>();
                    //for (int i = 0; i < 100; i++)
                    //{
                    //    int k = i;
                    //    tasks.Add(Task.Run(() =>
                    //    {
                    //        //list.Add(k);


                    //        //blockingList.Add(k);

                    //        //keyValues[k] = k;

                    //        //queue.Enqueue(k);

                    //        //stack.Push(k);
                    //        bag.Add(k);
                    //    }));
                    //}
                    //Task.WaitAll(tasks.ToArray());
                    ////int result = list.Count;

                    ////int result = blockingList.Count;
                    ////int result = keyValues.Count;
                    ////int result = queue.Count;
                    ////int result = stack.Count;
                    //int result = bag.Count;

                    //Console.WriteLine(result);
                }
            }
            Console.ReadLine();
        }
    }
}
