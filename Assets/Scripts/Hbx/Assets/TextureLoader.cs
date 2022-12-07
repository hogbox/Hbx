//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// TextureLoader.cs
//----------------------------------------------

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

using Hbx.Ext;

namespace Hbx.Assets
{
    /// <summary>
    /// Resources loader handles loading Textures from byte data
    /// Typically the bytes will have been read from disk or http with
    /// one of the other loaders before passing to this
    /// </summary>
    public class TextureLoader : FileLoader
    {
        /// <summary>
        /// The list of formats this texture loader can handle
        /// </summary>
        List<ITextureFileFormat> _formats = new List<ITextureFileFormat>();

        public TextureLoader()
        {
            _formats.Add(new TextureFileFormatDDS());
            _formats.Add(new TextureFileFormatPVR());
            _formats.Add(new TextureFileFormatPNG());
            _formats.Add(new TextureFileFormatJPG());
        }

        //
        // ILoader interface
        //

        /// <summary>
        /// Handles Textures
        /// </summary>
        public override Type[] types => new Type[] { typeof(Texture2D)  };

        /// <summary>
        /// Supports any extension as only deals with loading raw bytes
        /// </summary>
        public override string[] extensions => new string[] { ".png", ".jpg", ".jpeg", ".dds", ".pvr" };

        /// <summary>
        /// Supports the resources:// protocol
        /// </summary>
        public override string[] protocols => new string[] { };

        /// <summary>
        /// Read a Texture file from bytes
        /// </summary>
        /// <param name="src">Bytes of the file</param>
        /// <param name="options">Currently no supported options so can be null</param>
        /// <returns>Unity Texuture containing texels of the file</returns>
        public override async Task<ILoaderResult<T>> ReadAsync<T>(byte[] src, ILoaderOptions options)
        {
            if (options == null) options = defaultOptions;

            ITextureFileFormat fileFormat = FindTextureFileFormatForBytes(src);
            if (fileFormat == null) return new ILoaderResult<T>();

            TextureParams texparams = fileFormat.GetTextureParams(src);
            if (!texparams.valid) return new ILoaderResult<T>();

            if(!SystemInfo.IsFormatSupported(texparams.format, FormatUsage.Sample))
            {
#if UNITY_EDITOR
                Debug.Log("Failed to load texture " + options.originalSrc + "\nFormat " + System.Enum.GetName(typeof(GraphicsFormat), texparams.format) + " is not supported");
#endif
                return new ILoaderResult<T>("Format not supported");
            }

            Texture2D texture = new Texture2D(texparams.width, texparams.height, texparams.format, texparams.mipmaps, TextureCreationFlags.None);
            if (texparams.rawTexels)
            {
                texture.LoadRawTextureData(texparams.texels);
            }
            else
            {
                texture.LoadImage(texparams.texels);
            }
            texture.Apply();
            //texture.SetPixelData()

            // return the asset
            return new ILoaderResult<T>((T)(object)texture);
        }

        ITextureFileFormat FindTextureFileFormatForBytes(byte[] src)
        {
            foreach(ITextureFileFormat f in _formats)
            {
                if (f.usingHeaderMagic(src)) return f;
            }
            return null;
        }
    }
}

