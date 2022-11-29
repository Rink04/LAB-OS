using System;
using static System.Int16;

namespace Lab_4
{
    internal static class Program
    {
        private static long Enter(string message)
        {
            while (true)
            {
                Console.WriteLine($"Введите {message}");
                string input = Console.ReadLine();
                if (long.TryParse(input, out long enter)) return enter;
            }
        }

        private static long F(short i, short b, short c)
        {
            if (i == 0) return 0;

            long a = 0;
            for (int index = 0; index < 100000000; index++) a += 2 * b + c - i;

            return F(i - 1, b, c) + a;
        }

        private static long F(int i, int b, int c)
        {
            if (i == 0) return 0;

            long a = 0;
            for (int index = 0; index < 100000000; index++) a += 2 * b + c - i;

            return F(i - 1, b, c) + a;
        }

        private static long F(long i, long b, long c)
        {
            if (i == 0) return 0;

            long a = 0;
            for (int index = 0; index < 100000000; index++) a += 2 * b + c - i;

            return F(i - 1, b, c) + a;
        }

        private static void Main()
        {
            DateTime start;
            long result;
            long i = Enter("Введите i");
            long b = Enter("Введите b");
            long c = Enter("Введите c");
            Console.WriteLine("Считаем...");

            if (MinValue <= i && i <= MaxValue &&
                MinValue <= b && b <= MaxValue &&
                MinValue <= c && c <= MaxValue)
            {
                start = DateTime.Now;
                result = F((short)i, (short)b, (short)c);
                Console.WriteLine($"Время: {DateTime.Now - start}");
                Console.WriteLine($"Ответ: {result}");
                return;
            }

            if (MinValue > i && i <= int.MinValue || i > MaxValue && i <= int.MaxValue &&
                MinValue > b && b <= int.MinValue || b > MaxValue && b <= int.MaxValue &&
                MinValue > c && c <= int.MinValue || c > MaxValue && c <= int.MaxValue)
            {
                start = DateTime.Now;
                result = F((int)i, (int)b, (int)c);
                Console.WriteLine($"Время: {DateTime.Now - start}");
                Console.WriteLine($"Ответ: {result}");
                return;
            }

            start = DateTime.Now;
            result = F(i, b, c);
            Console.WriteLine($"Время: {DateTime.Now - start}");
            Console.WriteLine($"Ответ: {result}");
        }
    }
}

// ответ
//  -1874597
//  21.0