using System;
using System.Linq;
using System.Threading.Tasks;

namespace Hbx.Assets
{
    /// <summary>
    /// FileLoader base convienince type for loaders, provides useful default implementations
    /// of the <c>ILoader</c> interface. See <c>ILoader</c> for details.
    /// </summary>
    public abstract class FileLoader : ILoader
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
        public abstract Type[] types { get; }

        /// <summary>
        /// Not implemented concrete type should implement
        /// </summary>
        public abstract string[] extensions { get; }

        // <summary>
        /// Not implemented concrete type should implement
        /// </summary>
        public abstract string[] protocols { get; }

        /// <summary>
        /// Return a new LoaderOptions instance
        /// </summary>
        public virtual ILoaderOptions defaultOptions => new LoaderOptions();

        /// <summary>
        /// Not implemented concrete type should implement
        /// </summary>
        /// <param name="src"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual Task<ILoaderResult<T>> ReadAsync<T>(string src, ILoaderOptions options)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Default byte reader will convert the bytes to a UTF8 sting and forward
        /// to the string version of read
        /// </summary>
        /// <param name="src">byte array with data for loading</param>
        /// <param name="options">No options currently supported</param>
        /// <returns>Task T</returns>
        public virtual async Task<ILoaderResult<T>> ReadAsync<T>(byte[] src, ILoaderOptions options)
        {
            if (src == null || src.Length == 0) return new ILoaderResult<T>();
            string s = System.Text.Encoding.UTF8.GetString(src, 0, src.Length);
            return await ReadAsync<T>(s, options);
        }

        /// <summary>
        /// Default implementation checks if type matches or inheirts
        /// from any of <c>types</c> 
        /// </summary>
        /// <param name="type">The type to check for support</param>
        /// <returns>True if the type is supported by this loader</returns>
        public virtual bool supportsType(Type type)
        {
            foreach(Type t in types)
            {
                if (t == type || type.IsSubclassOf(t))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Default implementation checks if <c>extensions</c> contains ext
        /// or wildcard *
        /// </summary>
        /// <param name="ext">extension to check</param>
        /// <returns>true if extension is supported</returns>
        public virtual bool supportsExtension(string ext)
        {
            return extensions.Contains(ext);// || extensions.Contains("*");
        }

        /// <summary>
        /// Default implementation checks if protocol is equal to any value in <c>protocols</c>
        /// </summary>
        /// <param name="protocol">Protocol prefix string e.g. file://, http:// etc</param>
        /// <returns>True if the protocol at the start of src is supported by this loader</returns>
        public virtual bool supportsProtocol(string protocol)
        {
            foreach (string p in protocols)
            {
                if (protocol == p) return true;
            }
            return false;
        }
    }
}
