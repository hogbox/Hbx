//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// DiskTextLoader.cs
//----------------------------------------------

using System.IO;
using System.Threading.Tasks;

namespace Hbx.Assets
{

    /// <summary>
    /// DiskTextLoader handles reading text as string from local disk file system
    /// </summary>
    public class DiskTextLoader : FileLoader<TextResult>
    {
        public DiskTextLoader()
        {
        }

        //
        // ILoader interface
        //

        /// <summary>
        /// Handles raw asset type Text
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
        /// Read file from disk returns text in <c>TextResult</c>
        /// </summary>
        /// <param name="src">The path to the file on the local disk file system</param>
        /// <param name="options">Currently no supported options so can be null</param>
        /// <returns><c>TextResult</c> containing files text as string if successful other wise invalid <c>TextResult</c></returns>
        public new async Task<TextResult> read(string src, ILoaderOptions options)
        {
            if (!File.Exists(src)) return new TextResult(null);
            string resultstring = await File.ReadAllTextAsync(src);
            return new TextResult(resultstring);
        }
    }
}
