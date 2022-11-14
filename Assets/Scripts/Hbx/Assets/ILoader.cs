//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// ILoader.cs
//----------------------------------------------

using System.Threading.Tasks;

namespace Hbx.Assets
{

    /// <summary>
    /// Base non generic type for ILoader
    /// </summary>
    public interface ILoader
    {
        /// <summary>
        /// Returns the type of Asset this loader can handle
        /// </summary>
        /// <returns><c>AssetType</c> for the loader</returns>
        AssetType assetType { get; }

        /// <summary>
        /// Returns the type of Inputs this loader can accept.
        /// Mask as may support multiple types.
        /// </summary>
        /// <returns><c>LoaderInputType</c> for the loader</returns>
        LoaderInputType inputTypeMask { get; }

        /// <summary>
        /// Returns an array of extension strings supported by this loader
        /// including the dot '.'. Additionally wildcard * indicates any
        /// extension.
        /// e,g. .png, .jpg, .obj, * etc
        /// </summary>
        /// <returns>Array of file extensions this loader supports</returns>
        string[] extensions { get; }

        /// <summary>
        /// Returns a array of protocols this loaders supports
        /// file://, http:// etc but also things like $base64 and other inline string based formats
        /// </summary>
        /// <returns>Array of protocals this loader supports</returns>
        string[] protocols { get; }

        /// <summary>
        /// Checks if this loader supports the provided extension
        /// </summary>
        /// <returns>True if the extension is supported</returns>
        /// <param name="ext">File extension</param>
        bool supportsExtension(string ext);

        /// <summary>
        /// Checks if this loader supports the provided protocol
        /// </summary>
        /// <returns>True if the inline prefix is supported</returns>
        /// <param name="src">File path/src to be checked for protocol</param>
        bool supportsProtocol(string src);

        /// <summary>
        /// Allocate a default options object for this loader
        /// </summary>
        /// <returns>Options object implementing the ILoaderOptions interface</returns>
        ILoaderOptions defaultOptions { get; }

        /// <summary>
        /// Read an asset from the src specified
        /// </summary>
        /// <returns>Asset object of type T or null if loading fails/returns>
        /// <param name="src">File path, url, text or inline defintion for the asset/param>
        /// <param name="options">Options object to control loading behavior/param>
        Task<object> read(string src, ILoaderOptions options);

        /// <summary>
        /// Read an asset from the <c>BytesResult</c> specified
        /// </summary>
        /// <returns>Asset object of type T or null if loading fails/returns>
        /// <param name="src">Byte array for the asset</param>
        /// <param name="options">Options object to control loading behavior/param>
        Task<object> read(BytesResult src, ILoaderOptions options);
    }

    /// <summary>
    /// Generic interface for ILoader types enforces use of ILoaderResult for loading
    /// </summary>
    public interface ILoader<T> : ILoader where T : ILoaderResult
    {
        /// <summary>
        /// Read an asset from the src specified
        /// </summary>
        /// <returns>Asset object of type T or null if loading fails/returns>
        /// <param name="src">File path, url, text or inline defintion for the asset/param>
        /// <param name="options">Options object to control loading behavior/param>
        new Task<T> read(string src, ILoaderOptions options);

        /// <summary>
        /// Read an asset from the <c>BytesResult</c> specified
        /// </summary>
        /// <returns>Asset object of type T or null if loading fails/returns>
        /// <param name="src">Byte array for the asset</param>
        /// <param name="options">Options object to control loading behavior/param>
        new Task<T> read(BytesResult src, ILoaderOptions options);
    }
}
