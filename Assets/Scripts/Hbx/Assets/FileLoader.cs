using System.Linq;
using System.Threading.Tasks;

namespace Hbx.Assets
{
    /// <summary>
    /// FileLoader base convienince type for loaders, provides useful default implementations
    /// of the <c>ILoader</c> interface. See <c>ILoader</c> for details.
    /// </summary>
    public abstract class FileLoader<T> : ILoader<T> where T : ILoaderResult, new ()
    {
        public FileLoader()
        {
        }

        //
        // ILoader interface
        //

        /// <summary>
        /// Not implemented concrete type should implement
        /// </summary>
        public AssetType assetType => throw new System.NotImplementedException();

        /// <summary>
        /// Not implemented concrete type should implement
        /// </summary>
        public LoaderInputType inputTypeMask => throw new System.NotImplementedException();

        /// <summary>
        /// Not implemented concrete type should implement
        /// </summary>
        public string[] extensions => throw new System.NotImplementedException();

        // <summary>
        /// Not implemented concrete type should implement
        /// </summary>
        public string[] protocols => throw new System.NotImplementedException();

        /// <summary>
        /// No default options, conrete type should implement if needed
        /// </summary>
        public ILoaderOptions defaultOptions => null;

        /// <summary>
        /// Not implemented concrete type should implement
        /// </summary>
        /// <param name="src"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public Task<T> read(string src, ILoaderOptions options)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Default byte reader will convert the bytes to a UTF8 sting and forward
        /// to the string version of read
        /// </summary>
        /// <param name="src">ByteResult</param>
        /// <param name="options">No options supported currently</param>
        /// <returns>Task T</returns>
        public async Task<T> read(BytesResult src, ILoaderOptions options)
        {
            if (!src.valid) return new T();
            string s = System.Text.Encoding.UTF8.GetString(src.data, 0, src.data.Length);
            return await read(s, options);
        }

        /// <summary>
        /// We declare the <c>object</c> return types from the non generic ILoader
        /// This way concrete implementation won't need to implement it.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<object> ILoader.read(string src, ILoaderOptions options)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// We declare the <c>object</c> return types from the non generic ILoader
        /// This way concrete implementation won't need to implement it.
        /// </summary>
        /// <param name="src"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<object> ILoader.read(BytesResult src, ILoaderOptions options)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Default implementation checks if <c>extensions</c> contains ext
        /// or wildcard *
        /// </summary>
        /// <param name="ext">extension to check</param>
        /// <returns>true if extension is supported</returns>
        public bool supportsExtension(string ext)
        {
            return extensions.Contains(ext) || extensions.Contains("*");
        }

        /// <summary>
        /// Default implementation checks if src Starts with any value in <c>protocols</c>
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public bool supportsProtocol(string src)
        {
            foreach (string p in protocols)
            {
                if (src.StartsWith(p)) return true;
            }
            return false;
        }
    }
}
