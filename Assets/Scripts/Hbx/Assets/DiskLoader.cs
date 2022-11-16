//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// DiskLoader.cs
//----------------------------------------------

using System;
using System.IO;
using System.Threading.Tasks;

namespace Hbx.Assets
{
    /// <summary>
    /// DiskLoader handles reading bytes or string from a file on the local file system
    /// </summary>
    public class DiskLoader : FileLoader
    {
        public DiskLoader()
        {
        }

        //
        // ILoader interface
        //

        /// <summary>
        /// Handles bytes and strings
        /// </summary>
        public override Type[] types => new Type[] { typeof(byte[]), typeof(string) };

        /// <summary>
        /// Supports any extension as only deals with loading raw bytes
        /// </summary>
        public override string[] extensions => new string[] { "*" };

        /// <summary>
        /// Supports the file:// protocol
        /// </summary>
        public override string[] protocols => new string[] { Protocols.FILE_PREFIX };

        /// <summary>
        /// Read file from disk returns bytes in a byte array or string
        /// </summary>
        /// <param name="src">The path to the file on the local disk file system</param>
        /// <param name="options">Currently no supported options so can be null</param>
        /// <returns>Depending on T can return btye array or string</returns>
        public override async Task<ILoaderResult<T>> ReadAsync<T>(string src, ILoaderOptions options)
        {
            if (!File.Exists(src)) return new ILoaderResult<T>();
            T result = default;

            if (typeof(T) == typeof(byte[]))
            {
                byte[] bytes = await File.ReadAllBytesAsync(src);
                result = (T)(object)bytes;
            }
            else if(typeof(T) == typeof(string))
            {
                string str = await File.ReadAllTextAsync(src);
                result = (T)(object)str;
            }

            return new ILoaderResult<T>(result);
        }
    }
}
