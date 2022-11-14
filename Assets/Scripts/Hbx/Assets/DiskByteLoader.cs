//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// DiskByteLoader.cs
//----------------------------------------------

using System.IO;
using System.Threading.Tasks;

namespace Hbx.Assets
{
    /// <summary>
    /// DiskByteLoader handles reading bytes as BytesResult from local disk file system
    /// </summary>
    public class DiskByteLoader : FileLoader<BytesResult>
    {
        public DiskByteLoader()
        {
        }

        //
        // ILoader interface
        //

        /// <summary>
        /// Handles raw asset type btye array
        /// </summary>
        public new AssetType assetType => AssetType.Raw;

        /// <summary>
        /// Can handle Path inputs
        /// </summary>
        public new LoaderInputType inputTypeMask => LoaderInputType.Path;

        /// <summary>
        /// Supports any extension as only deals with loading raw bytes
        /// </summary>
        public new string[] extensions => new string[] { "*" };

        /// <summary>
        /// Read file from disk returns bytes in <c>BytesResult</c>
        /// </summary>
        /// <param name="src">The path to the file on the local disk file system</param>
        /// <param name="options">Currently no supported options so can be null</param>
        /// <returns><c>BytesResult</c> containing files bytes if successful other wise invalid <c>BytesResult</c></returns>
        public new async Task<BytesResult> read(string src, ILoaderOptions options)
        {
            if (!File.Exists(src)) return new BytesResult(null);
            byte[] resultbyes = await File.ReadAllBytesAsync(src);
            return new BytesResult(resultbyes);
        }
    }
}
