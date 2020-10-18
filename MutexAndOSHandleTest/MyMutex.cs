using System.Threading;

namespace MutexAndOSHandleTest
{
    public class MyMutex
    {
        private Thread currentThread = null;

        public void Lock()
        {
            while (Interlocked.CompareExchange(ref currentThread,Thread.CurrentThread , null) != null)
            {
                Thread.Sleep(200);
            }
        }

        public void Unlock()
        {
            Interlocked.CompareExchange(ref currentThread, null, Thread.CurrentThread);
        }
    }
}