//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// ILoader.cs
//----------------------------------------------

using System;
using System.Threading.Tasks;

namespace Hbx.Assets
{

    /// <summary>
    /// Base non generic type for ILoader
    /// </summary>
    public interface ILoader
    {
        /// <summary>
        /// Returns an array of types this loader can handle
        /// </summary>
        /// <returns><c>AssetType</c> for the loader</returns>
        Type[] types { get; }

        /// <summary>
        /// Returns an array of extension strings supported by this loader
        /// including the dot
        /// extension.
        /// e,g. .png, .jpg, .obj etc
        /// </summary>
        /// <returns>Array of file extensions this loader supports</returns>
        string[] extensions { get; }

        /// <summary>
        /// Returns a array of protocols this loaders supports
        /// file://, http:// etc see Protocols.cs
        /// </summary>
        /// <returns>Array of protocals this loader supports</returns>
        string[] protocols { get; }

        /// <summary>
        /// Check is a Type is supported by this loader
        /// </summary>
        /// <param name="type">The type to check for support</param>
        /// <returns>True if the type is supported by this loader</returns>
        bool supportsType(Type type);

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
        Task<ILoaderResult<T>> ReadAsync<T>(string src, ILoaderOptions options);

        /// <summary>
        /// Read an asset from the byte array specified
        /// </summary>
        /// <returns>Asset object of type T or null if loading fails/returns>
        /// <param name="src">Byte array for the asset</param>
        /// <param name="options">Options object to control loading behavior/param>
        Task<ILoaderResult<T>> ReadAsync<T>(byte[] src, ILoaderOptions options);
    }
}
