using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace MutexAndOSHandleTest
{
    public class OSHandle : IDisposable
    {
        [DllImport("Kernel32.dll")]
        private static extern bool CloseHandle(IntPtr handle);

        private bool disposed = false;

        public IntPtr Handle { get; private set; }

        public OSHandle(IntPtr handle)
        {
            Handle = handle;
        }

        public bool Close()
        {
            var result = CloseHandle(Handle);
            Dispose();
            return result;
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
            Handle = IntPtr.Zero;
            disposed = true;
        }
        
        ~OSHandle()
        {
            Dispose (false);
        }
        
        
    }
}