using System;
using System.Runtime.InteropServices;

namespace MutexAndOSHandleTest
{
    public class OSHandle : IDisposable
    {
        [DllImport("Kernel32.dll")]
        private static extern bool CloseHandle(IntPtr handle);

        private bool disposed = false;

        public IntPtr Handle { get; }

        public OSHandle(IntPtr handle)
        {
            Handle = handle;
        }

        public void Close()
        {
            Dispose();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed || Handle.Equals(IntPtr.Zero)) return;
            CloseHandle(Handle);
            disposed = true;
        }
        
        ~OSHandle()
        {
            Dispose (false);
        }
    }
}