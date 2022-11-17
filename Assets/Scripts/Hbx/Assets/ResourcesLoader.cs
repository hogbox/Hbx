//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// ResourcesLoader.cs
//----------------------------------------------

using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

using Hbx.Ext;

namespace Hbx.Assets
{
    /// <summary>
    /// DiskLoader handles reading bytes or string from a file on the local file system
    /// </summary>
    public class ResourcesLoader : FileLoader
    {
        public ResourcesLoader()
        {
        }

        //
        // ILoader interface
        //

        /// <summary>
        /// Handles bytes and strings
        /// </summary>
        public override Type[] types => new Type[]
        {
            typeof(string),
            typeof(byte[]),
            typeof(GameObject),
            typeof(Texture2D),
            typeof(Texture2DArray),
            typeof(Texture3D),
            typeof(Cubemap),
            typeof(CubemapArray),
            typeof(VideoClip),
            typeof(TextAsset),
            typeof(AudioClip)
        };

        /// <summary>
        /// Supports any extension as only deals with loading raw bytes
        /// </summary>
        public override string[] extensions => new string[] { "*" };

        /// <summary>
        /// Supports the resources:// protocol
        /// </summary>
        public override string[] protocols => new string[] { Protocols.RESOURCES_PREFIX };

        /// <summary>
        /// Read a Unity Resource from the Resources folder
        /// </summary>
        /// <param name="src">Path to resource relative to the Resources folder</param>
        /// <param name="options">Currently no supported options so can be null</param>
        /// <returns>Unity Resource object of type T if it exists at src path</returns>
        public override async Task<ILoaderResult<T>> ReadAsync<T>(string src, ILoaderOptions options)
        {
            // check if this is an inline asset using the json:// protocol
            if (!Protocols.IsPathUsingProtocol(src, Protocol.RESOURCES))
            {
                return new ILoaderResult<T>();
            }

            // remove the protocol from the string leaving the resources path
            src = src.Remove(0, Protocols.RESOURCES_PREFIX.Length);

            Type ttype = typeof(T);
            bool israw = Load.IsRawType(ttype);

            ResourceRequest request = Resources.LoadAsync(src, israw ? typeof(TextAsset) : ttype);
            await request;

            if (israw)
            {
                TextAsset textAsset = (TextAsset)request.asset;
                if (textAsset == null) return new ILoaderResult<T>();

                T result = ttype == typeof(string) ? (T)(object)textAsset.text : (T)(object)textAsset.bytes;
                return new ILoaderResult<T>(result);
            }

            return new ILoaderResult<T>((T)(object)request.asset);
        }
    }
}

