//----------------------------------------------
//            Hbx: Assets
// Copyright © 2017-2018 Hogbox Studios
// Threading.cs
//----------------------------------------------

#if !UNITY_WEBGL || UNITY_EDITOR
#define HBX_USE_THREADS
#endif

using System.Threading;
using System.IO;

namespace Hbx.Assets
{
    /// <summary>
    /// Base class for threaded operations
    /// </summary>

    public abstract class ThreadOp
    {
        Thread _thread = null;
        bool _isRunning = false;
        public bool IsRunning { get { return _isRunning; } }

        /// <summary>
        /// Start running the thread to perfrom our operation
        /// </summary>
        public void Start()
        {
            _isRunning = true;
#if HBX_USE_THREADS
            _thread = new Thread(Run);
            _thread.Start();
#else
            PerformOp();
            _isRunning = false;
#endif
        }

        /// <summary>
        /// Abstract function implemented by contrete type, the content of the function is performed on the new thread
        /// </summary>

        protected abstract void PerformOp();

        /// <summary>
        /// Funtion passed to Thread to be run, this calls our PerformOp and alters the isRunning value once complete
        /// </summary>

        void Run()
        {
            PerformOp();
            _isRunning = false;
        }
    }

    /// <summary>
    /// Thread operation for performing file reads from the disk
    /// </summary>

    public class ReadDiskBytesThreadOp : ThreadOp
    {
        string _path = "";
        byte[] _bytes = null;
        public byte[] Bytes { get { return _bytes; } }

        public ReadDiskBytesThreadOp(string aPath)
        {
            _path = aPath;
        }

        protected override void PerformOp()
        {
            _bytes = File.ReadAllBytes(Paths.ResolvePath(_path, true));
        }
    }

    /// <summary>
    /// Thread operation for performing file writes from the disk
    /// </summary>

    public class WriteDiskBytesThreadOp : ThreadOp
    {
        string _path = "";
        byte[] _bytes = null;

        public WriteDiskBytesThreadOp(string aPath, byte[] aBytesResult)
        {
            _path = aPath;
            _bytes = new byte[aBytesResult.Length];
             System.Array.Copy(aBytesResult, _bytes, aBytesResult.Length);
        }

        protected override void PerformOp()
        {
            File.WriteAllBytes(Paths.ResolvePath(_path, true), _bytes);
        }
    }

} // end Hbx Assets namespace
