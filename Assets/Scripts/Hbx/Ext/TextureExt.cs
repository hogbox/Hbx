//----------------------------------------------
//            Hbx: Ext
// Copyright © 2017-2018 Hogbox Studios
// TextureExt.cs
//----------------------------------------------

using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hbx.Ext
{
    public static class TextureExt
    {
        public enum Format
        {
            UNKOWN,
            PNG,
            JPG
        }

        public static readonly Dictionary<Format, string[]> kFormatExtensions = new Dictionary<Format, string[]>()
        {
            { TextureExt.Format.PNG, new string[] { ".png" } },
            { TextureExt.Format.JPG, new string[] { ".jpg", ".jpeg" } }
        };

        /// <summary>
        /// Determine the Format from the files extension
        /// </summary>
        /// <returns>The Format for the passed file path, Unkown if not supported.</returns>
        /// <param name="aFilePath">A file path.</param>

        public static Format FormatForFile(string aFilePath)
        {
            string ext = Path.GetExtension(aFilePath).ToLower();
            foreach (Format f in kFormatExtensions.Keys)
            {
                foreach (string e in kFormatExtensions[f])
                {
                    if (e == ext)
                    {
                        return f;
                    }
                }
            }
            return Format.UNKOWN;
        }

        public static byte[] EncodeTextureToFileBytes(this Texture2D aTexture, string aPath)
        {
            return aTexture.EncodeTextureToFileBytes(FormatForFile(aPath));
        }

        /// <summary>
        /// Encodes the texture as a file of specified Format.
        /// </summary>
        /// <returns>The texture file bytes.</returns>
        /// <param name="aTextue">A texture.</param>
        /// <param name="aFormat">A format.</param>

        public static byte[] EncodeTextureToFileBytes(this Texture2D aTexture, Format aFormat)
        {
            byte[] texbytes = null;
            if (aFormat == Format.JPG)
            {
                texbytes = aTexture.EncodeToJPG(100);
            }
            else if (aFormat == Format.PNG)
            {
                texbytes = aTexture.EncodeToPNG();
            }
            return texbytes;
        }

        /// <summary>
        /// Encodes the textures file byte representation to base64 string.
        /// </summary>
        /// <returns>The texture file bytes as base64 string.</returns>
        /// <param name="aTexture">A texture.</param>
        /// <param name="aFormat">A format.</param>

        public static string EncodeTextureToBase64String(this Texture2D aTexture, Format aFormat)
        {
            return System.Convert.ToBase64String(aTexture.EncodeTextureToFileBytes(aFormat));
        }
    }
} // end Hbx Ext namespace
