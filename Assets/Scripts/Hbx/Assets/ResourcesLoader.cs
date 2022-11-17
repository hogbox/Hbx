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
    /// Resources loader handles loading assets from the Unity Resources system.
    /// Uses the resources:// protocol to identify paths
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
            // check if this is an inline asset using the resources:// protocol
            if (!Protocols.IsPathUsingProtocol(src, Protocol.RESOURCES))
            {
                return new ILoaderResult<T>();
            }

            // remove the protocol from the string leaving the resources path
            src = src.Remove(0, Protocols.RESOURCES_PREFIX.Length);

            // get type and check if it's a raw asset (string or bytes)
            Type ttype = typeof(T);
            bool israw = Load.IsRawType(ttype);

            // try to load the asset, if it was raw make sure we try to load as TextAsset
            ResourceRequest request = Resources.LoadAsync(src, israw ? typeof(TextAsset) : ttype);
            await request;

            if (israw)
            {
                // if it's raw get the text asset the return the appropritate text or bytes
                TextAsset textAsset = (TextAsset)request.asset;
                if (textAsset == null) return new ILoaderResult<T>();

                T result = ttype == typeof(string) ? (T)(object)textAsset.text : (T)(object)textAsset.bytes;
                return new ILoaderResult<T>(result);
            }

            // return the asset
            return new ILoaderResult<T>((T)(object)request.asset);
        }
    }
}

