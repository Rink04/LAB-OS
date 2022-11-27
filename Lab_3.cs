//Требования к языку программирования: любой язык программирования

//Разработать программу, имитирующую работу склада (конвейера).

//Дано 3 производителя и 2 потребителя, все разные потоки и работают все одновременно.

//Есть очередь с 200 элементами. Производители добавляют случайное число от 1…100, а потребители берут эти числа.

//Если в очереди элементов >= 100 производители спят, если нет элементов в очереди - потребители спят.

//Если элементов стало <= 80 производители просыпаются.

//Все это работает до тех пор пока пользователь не нажал на кнопку “q”, после чего производители останавливаются, а потребители берут все элементы, только потом программа завершается.


using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Lab_3
{
    class Program
    {
        static public bool flag = true;
        static public int count = 0;

        static void printMenu()
        {

            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Lab_3, Паршина А Д ");
                Console.WriteLine("Выберите: ");
                Console.WriteLine("1. Выполние ");
                Console.WriteLine("2. Очистка ");
                Console.WriteLine("3. Выход ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        // общий канал
                        Channel<int> channel = Channel.CreateBounded<int>(200);
                        //создал токен отмены
                        var cts = new CancellationTokenSource();
                        //производители и потребители
                        Task[] streams = new Task[5];
                        for (int i = 0; i < 5; i++)
                        {
                            if (i < 3)
                            {
                                streams[i] = Task.Run(() => { new Producer(channel.Writer, cts.Token); }, cts.Token);
                            }
                            else
                            {
                                streams[i] = Task.Run(() => { new Consumer(channel.Reader, cts.Token); }, cts.Token);
                            }
                        }
                        // поток проверки клавиши
                        new Thread(() =>
                        {
                            for (; ; )
                            {
                                if (Console.ReadKey(true).Key == ConsoleKey.Q)
                                {
                                    cts.Cancel();
                                }
                            }
                        })
                        { IsBackground = true }.Start();
                        Task.WaitAll(streams);

                        break;
                    case 2:
                        Console.Clear();
                        break;
                    case 3:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("\tЕще раз");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            printMenu();
        }
    }
    class Producer
    {
        private ChannelWriter<int> Writer;
        public Producer(ChannelWriter<int> _writer, CancellationToken tok)
        {
            Writer = _writer;
            Task.WaitAll(Run(tok));
        }

        private async Task Run(CancellationToken tok)
        {
            var r = new Random();
            // жде свободного место для записи
            while (await Writer.WaitToWriteAsync())
            {
                if (tok.IsCancellationRequested)
                {
                    Console.WriteLine("\t  Производители спят");
                    return;
                }
                if (Program.flag && Program.count <= 100)
                {
                    var item = r.Next(1, 101);
                    await Writer.WriteAsync(item);
                    Program.count += 1;
                    Console.WriteLine($"\tПроизводители дают: {item}");
                }
            }
        }
    }

    class Consumer
    {
        private ChannelReader<int> Reader;

        public Consumer(ChannelReader<int> _reader, CancellationToken tok)
        {
            Reader = _reader;
            Task.WaitAll(Run(tok));
        }

        private async Task Run(CancellationToken tok)
        {
            // джет свободное место для чтения
            while (await Reader.WaitToReadAsync())
            {
                if (Reader.Count != 0)
                {
                    var item = await Reader.ReadAsync();
                    Program.count -= 1;
                    Console.WriteLine($"\tПотребители берут: {item}");
                }
                if (Reader.Count >= 100)
                {
                    Program.flag = false;
                }
                else if (Reader.Count <= 80)
                {
                    Console.WriteLine($"\t  Производители проснулись");
                    Program.flag = true;
                }
                //проверка токена
                if (tok.IsCancellationRequested)
                {
                    if (Reader.Count == 0)
                    {
                        Console.WriteLine("\tПотребители спят");
                        return;
                    }
                }
            }
        }
    }

   
}