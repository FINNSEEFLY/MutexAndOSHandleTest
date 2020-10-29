﻿using System;
using System.Threading;
using System.Diagnostics;

namespace MutexAndOSHandleTest
{
    class Program
    {
        private const int NUM_OF_THREADS = 5;
        private const int MAX_NUMBER = 5;
        static void Main(string[] args)
        {
            int answer;
            do
            {
                Console.WriteLine("________________________________");
                Console.WriteLine("Какой класс будем тестировать?");
                Console.WriteLine("1) Mutex");
                Console.WriteLine("2) OSHandle");
                Console.WriteLine("0) Выход");
                Console.WriteLine("________________________________");
                answer = ParseAnswer(Console.ReadLine());
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
                answer = -1;
            } while (answer<1);

        }

        private static int ParseAnswer(string text)
        {
            try
            {
                return Int32.Parse(text.Trim());
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
            Console.WriteLine("Введите номер дескриптора ОС: ");
            while (!int.TryParse(Console.ReadLine(), out handleNumber))
                Console.WriteLine("Ошибка ввода, введите номер дескриптора ОС: ");
            //TODO: Получение дескриптора
            //Process.GetProcessById(handleNumber);
            //var osHandle = new OSHandle(new IntPtr(handleNumber));
            //osHandle.Close();
        }
    }
}