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
                Console.WriteLine("Выберите пункт меню: ");
                Console.WriteLine("1. Выполнить");
                Console.WriteLine("2. Очистка консоли");
                Console.WriteLine("3. Выйти");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        //создаю общий канал данных
                        Channel<int> channel = Channel.CreateBounded<int>(200);
                        //создал токен отмены
                        var cts = new CancellationTokenSource();
                        //создаются производители и потребители
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
                        //Создается поток проверки нажатия клавиши
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
                        //Ожидает завершения выполнения всех указанных объектов Task 
                        Task.WaitAll(streams);

                        break;
                    case 2:
                        Console.Clear();
                        break;
                    case 3:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("\t Еще раз!");
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
            // ждет, когда появиться место для записи
            while (await Writer.WaitToWriteAsync())
            {
                if (tok.IsCancellationRequested)
                {
                    Console.WriteLine("\tПроизводитель спит");
                    return;
                }
                if (Program.flag && Program.count <= 100)
                {
                    var item = r.Next(1, 101);
                    await Writer.WriteAsync(item);
                    Program.count += 1;
                    Console.WriteLine($"\tПроизводитель добавил: {item}");
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
            // ожидает, когда освободиться место для чтения элемента.
            while (await Reader.WaitToReadAsync())
            {
                if (Reader.Count != 0)
                {
                    var item = await Reader.ReadAsync();
                    Program.count -= 1;
                    Console.WriteLine($"\tПользователь берет: {item}");
                }
                if (Reader.Count >= 100)
                {
                    Program.flag = false;
                }
                else if (Reader.Count <= 80)
                {
                    Console.WriteLine($"\tПроизводитель проснулся");
                    Program.flag = true;
                }
                //проверка токена
                if (tok.IsCancellationRequested)
                {
                    if (Reader.Count == 0)
                    {
                        Console.WriteLine("\tПотребитель спит");
                        return;
                    }
                }
            }
        }
    }  
}

