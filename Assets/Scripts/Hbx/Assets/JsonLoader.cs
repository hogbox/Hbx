//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// JsonLoader.cs
//----------------------------------------------

using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using System;

namespace Hbx.Assets
{
    /// <summary>
    /// Json Loader handles loading deserialising a <c>GenericResult<T></c> from a Json string
    /// </summary>
    public class JsonLoader : FileLoader
    {
        public JsonLoader()
        {
        }

        //
        // ILoader interface
        //

        /// <summary>
        /// Handles generic objects
        /// </summary>
        public override Type[] types => new Type[] { typeof(object) };

        /// <summary>
        /// Supports the .json extension
        /// </summary>
        public override string[] extensions => new string[] { ".json" };

        /// <summary>
        /// Supports inline json via the json:// protocol
        /// </summary>
        public override string[] protocols => new string[] { Protocols.JSON_PREFIX };

        /// <summary>
        /// Deserialize the src json string into GenericResult T type
        /// </summary>
        /// <param name="src">Should be a json string</param>
        /// <param name="options">Currently no supported options so can be null</param>
        /// <returns>GenericResult T</returns>
        public override async Task<ILoaderResult<T>> ReadAsync<T>(string src, ILoaderOptions options)
        {
            // check if this is an inline asset using the json:// protocol
            if (Protocols.IsPathUsingProtocol(src, Protocol.JSON))
            {
                // remove the protocol from the string and pass the data portion
                src = src.Remove(0, Protocols.JSON_PREFIX.Length);
            }

            T result = default;
            Exception exception = null;
            await Task.Run(() =>
            {
                try
                {
                    result = JsonUtility.FromJson<T>(src);
                }
                catch (Exception e)
                {
                    // TODO will relying on exception work in builds?
                    // a user could provide an invalid json string and
                    // we want to be able to provide and error, not crash
                    exception = e;
                }
            });

            if(exception != null)
            {
                return new ILoaderResult<T>();
            }

            return new ILoaderResult<T>(result);
        }
    }
}