// 1. = 1115dd800feaacefdf481f1f9070374a2a81e27880f187396db67958b207cbad
// 2. = 3a7bd3e2360a3d29eea436fcfb7e44c735d117c42d1c1835420b6b9942dd4f1b
// 3. = 74e1bb62f8dabb8125a58852b63bdf6eaef667cb56ac7f7cdba6d7305c50a22f




using System;
using System.Text;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading.Tasks;

namespace Lab_2
{

     internal static class Hash
    {
        internal static string GetStringSha256Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            using (var sha = new SHA256Managed())
            {
                byte[] textData = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", string.Empty);
            }
        }
    }

        internal static class SingleThread

        {
            private static readonly char[] Dictionary =
            {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
            'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r',
            's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};

    //internal static class Menu
    //{
    //    internal static void ThreadChoiceShow()
    //    {

    //        Console.Clear();
    //        Console.WriteLine("Выберете:");
    //        Console.WriteLine("1. Однопоточный");
    //        Console.WriteLine("2. Многопоточный");
    //        string choice = Console.ReadLine();

    //        Console.Clear();
    //        switch (choice)
    //        {
    //            case "1":
                
        internal static void BruteHash(string hash)
        {
                DateTime start = DateTime.Now;
                int length = Dictionary.Length;
                for (int ch1 = 0; ch1 < length; ch1++)
                {
                    string a = Convert.ToString(Dictionary[ch1]);
                    for (int ch2 = 0; ch2 < length; ch2++)
                    {
                        string b = Convert.ToString(Dictionary[ch2]);
                        for (int ch3 = 0; ch3 < length; ch3++)
                        {
                            string c = Convert.ToString(Dictionary[ch3]);
                            for (int ch4 = 0; ch4 < length; ch4++)
                            {
                                string d = Convert.ToString(Dictionary[ch4]);
                                for (int ch5 = 0; ch5 < length; ch5++)
                                {
                                    string e = Convert.ToString(Dictionary[ch5]);
                                    string password = a + b + c + d + e;
                                    string hashed = Hash.GetStringSha256Hash(password);
                                    if (hash == hashed)
                                    {
                                        Console.WriteLine($"Найден пароль {password}, hash {hashed}");
                                        Console.WriteLine(DateTime.Now - start);
                                        ch1 = ch2 = ch3 = ch4 = ch5 = length;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            internal static void BruteHashFromFile(string path)
            {
                DateTime start = DateTime.Now;
                int length = Dictionary.Length;
                int count = 0;
                for (int ch1 = 0; ch1 < length; ch1++)
                {
                    string a = Convert.ToString(Dictionary[ch1]);
                    for (int ch2 = 0; ch2 < length; ch2++)
                    {
                        string b = Convert.ToString(Dictionary[ch2]);
                        for (int ch3 = 0; ch3 < length; ch3++)
                        {
                            string c = Convert.ToString(Dictionary[ch3]);
                            for (int ch4 = 0; ch4 < length; ch4++)
                            {
                                string d = Convert.ToString(Dictionary[ch4]);
                                for (int ch5 = 0; ch5 < length; ch5++)
                                {
                                    string e = Convert.ToString(Dictionary[ch5]);
                                    string password = a + b + c + d + e;
                                    string hash = Hash.GetStringSha256Hash(password);
                                    foreach (string line in File.ReadLines(path, Encoding.Default))
                                    {
                                        if (!line.ToUpper().Contains(hash)) continue;

                                        Console.WriteLine($"Найден пароль {password}, hash {hash}");
                                        Console.WriteLine(DateTime.Now - start);
                                        count++;
                                        break;
                                    }

                                    if (count == File.ReadAllLines(path).Length)
                                    {
                                        ch1 = ch2 = ch3 = ch4 = ch5 = length;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            //  break;
            
            //case '2':

    internal static class MultiThreading
            {
                internal static void BruteHash(string hash)
                {
                    DateTime start = DateTime.Now;
                    bool flag = false;
                    Parallel.For(0, 26, a =>
                    {
                        byte[] password = new byte[5];
                        password[0] = (byte)(97 + a);
                        for (password[1] = 97; password[1] < 123; password[1]++)
                        {
                            for (password[2] = 97; password[2] < 123; password[2]++)
                            {
                                for (password[3] = 97; password[3] < 123; password[3]++)
                                {
                                    for (password[4] = 97; password[4] < 123; password[4]++)
                                    {
                                        string passwordString = Encoding.ASCII.GetString(password);
                                        string hashed = Hash.GetStringSha256Hash(passwordString);
                                        if (hash != hashed) continue;

                                        Console.WriteLine($"Найден пароль {passwordString}, hash {hashed}");
                                        Console.WriteLine(DateTime.Now - start);
                                        flag = true;
                                        break;
                                    }

                                    if (flag) break;
                                }

                                if (flag) break;
                            }

                            if (flag) break;
                        }
                    });
                }

                internal static void BruteHashFromFile(string path)
                {
                    bool flag = false;
                    DateTime start = DateTime.Now;
                    Parallel.For(0, 26, a =>
                    {
                        byte[] password = new byte[5];
                        int count = 0;
                        password[0] = (byte)(97 + a);
                        for (password[1] = 97; password[1] < 123; password[1]++)
                        {
                            for (password[2] = 97; password[2] < 123; password[2]++)
                            {
                                for (password[3] = 97; password[3] < 123; password[3]++)
                                {
                                    for (password[4] = 97; password[4] < 123; password[4]++)
                                    {
                                        string passwordString = Encoding.ASCII.GetString(password);
                                        string hash = Hash.GetStringSha256Hash(passwordString);
                                        foreach (string line in File.ReadLines(path, Encoding.Default))
                                        {
                                            if (!line.ToUpper().Contains(hash)) continue;

                                            Console.WriteLine($"Найден пароль {passwordString}, hash {hash}");
                                            Console.WriteLine(DateTime.Now - start);
                                            count++;
                                            if (count == File.ReadAllLines(path).Length) flag = true;
                                            break;
                                        }

                                        if (flag) break;
                                    }

                                    if (flag) break;
                                }

                                if (flag) break;
                            }

                            if (flag) break;
                        }
                    });
                }
            }
        }
        //break;
        //}
}