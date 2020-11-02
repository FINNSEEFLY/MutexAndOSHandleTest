using System;
using System.Threading;
using System.Globalization;

namespace MutexAndOSHandleTest
{
    class Program
    {
        private const int NUM_OF_THREADS = 5;
        private const int MAX_NUMBER = 5;

        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("________________________________" + Environment.NewLine +
                                  "Какой класс будем тестировать?" + Environment.NewLine +
                                  "1) Mutex" + Environment.NewLine +
                                  "2) OSHandle" + Environment.NewLine +
                                  "0) Выход " + Environment.NewLine +
                                  "________________________________");
                var answer = ParseAnswer(Console.ReadLine());
                switch (answer)
                {
                    case 1:
                        MutexTest();
                        break;
                    case 2:
                        OSHandleTest();
                        break;
                    case 0:
                        return;
                }
            } while (true);
        }

        private static int ParseAnswer(string text)
        {
            try
            {
                return int.Parse(text.Trim());
            }
            catch
            {
                return -1;
            }
        }

        private static void MutexTest()
        {
            var threads = new Thread[NUM_OF_THREADS];
            Console.WriteLine("Mutex Test");
            var mutex = new MyMutex();
            for (var i = 1; i <= NUM_OF_THREADS; i++)
            {
                var thread = new Thread(ThreadAction) {Name = $"Поток {i}"};
                threads[i - 1] = thread;
                thread.Start(mutex);
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }
        }

        private static void ThreadAction(object mutexObj)
        {
            var mutex = mutexObj as MyMutex;
            mutex.Lock();
            for (var i = 1; i <= MAX_NUMBER; i++)
            {
                Console.WriteLine($"{Thread.CurrentThread.Name}: {i}");
            }

            mutex.Unlock();
        }

        static void OSHandleTest()
        {
            int handleNumber;
            Console.WriteLine("Введите номер дескриптора ОС (формат 1b3): ");
            while (!int.TryParse(Console.ReadLine(), NumberStyles.HexNumber, CultureInfo.InvariantCulture,
                out handleNumber))
                Console.WriteLine("Ошибка ввода, введите номер дескриптора ОС (формат 1b3): ");
            var osHandle = new OSHandle(new IntPtr(handleNumber));
            Console.WriteLine(osHandle.Close() ? "Дескриптор удалось закрыть" : "Дескриптор не удалось закрыть");
            osHandle.Dispose();
        }
    }
}