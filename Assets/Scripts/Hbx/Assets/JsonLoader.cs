//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// JsonLoader.cs
//----------------------------------------------

using UnityEngine;
using System.IO;
using System.Threading.Tasks;

namespace Hbx.Assets
{
    /// <summary>
    /// Json Loader handles loading deserialising a <c>GenericResult<T></c> from a Json string
    /// </summary>
    public class JsonLoader<T> : FileLoader<GenericResult<T>> where T : new()
    {
        public JsonLoader()
        {
        }

        //
        // ILoader interface
        //

        /// <summary>
        /// Handles raw asset type Text
        /// </summary>
        public new AssetType assetType => AssetType.Object;

        /// <summary>
        /// Can handle Path inputs
        /// </summary>
        public new LoaderInputType inputTypeMask => LoaderInputType.Text;

        /// <summary>
        /// Supports any extension as only deals with loading raw bytes
        /// </summary>
        public new string[] extensions => new string[] { "json" };

        /// <summary>
        /// Deserialize the src json string into GenericResult T type
        /// </summary>
        /// <param name="src">Should be a json string</param>
        /// <param name="options">Currently no supported options so can be null</param>
        /// <returns>GenericResult T</returns>
        public new async Task<GenericResult<T>> read(string src, ILoaderOptions options)
        {
            T resultstring = JsonUtility.FromJson<T>(src);
            return new GenericResult<T>(resultstring);
        }
    }
}