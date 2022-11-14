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
            JPG,
            DDS
        }

        public static readonly Dictionary<Format, string[]> kFormatExtensions = new Dictionary<Format, string[]>()
        {
            { TextureExt.Format.PNG, new string[] { ".png" } },
            { TextureExt.Format.JPG, new string[] { ".jpg", ".jpeg" } },
            { TextureExt.Format.DDS, new string[] { ".dds" } },
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

        /// <summary>
        /// Texture parameters for allocating textures with correct dimensions and format
        /// </summary>

        public struct TextureParams
        {
            public int width;
            public int height;
            public TextureFormat format;
        }

        /// <summary>
        /// Determine the TextureParams from a files bytes depending on the file format
        /// </summary>
        /// <returns>The from texture bytes.</returns>
        /// <param name="texBytes">Tex bytes.</param>
        /// <param name="aFormat">A format.</param>

        public static TextureParams ParamsFromTextureBytes(byte[] texBytes, Format aFormat)
        {
            if(aFormat == Format.DDS)
            {
                return ParamsFromDDSBytes(texBytes);
            }
            return new TextureParams();
        }

        public static bool LoadImage(this Texture2D aTexture, byte[] texBytes, string aFilePath)
        {
            return aTexture.LoadImage(texBytes, FormatForFile(aFilePath));
        }

        public static bool LoadImage(this Texture2D aTexture, byte[] texBytes, Format aFormat)
        {
            if (aFormat == Format.JPG || aFormat == Format.PNG)
            {
                return aTexture.LoadImage(texBytes, true);

            }
            else if (aFormat == Format.DDS)
            {
                return aTexture.LoadImageDDS(texBytes);
            }
            return false;
        }

        /// <summary>
        /// When calling Load Image DDS the texture must have already been allocated with the correct dimensions and format
        /// </summary>
        /// <returns><c>true</c>, if image dds was loaded, <c>false</c> otherwise.</returns>
        /// <param name="aTexture">A texture.</param>
        /// <param name="ddsBytes">Dds file bytes.</param>

        public static bool LoadImageDDS(this Texture2D aTexture, byte[] ddsBytes)
        {
            // check it's a DDS header
            int startbyte = 4;
            byte ddsSizeCheck = ddsBytes[startbyte];
            if (ddsSizeCheck != 124)
            {
                Debug.LogWarning("Error: LoadTextureDXT: Invalid DDS texture, dds file header invalid.");
                return false;
            }

            // copy the pixel bytes
            int DDS_HEADER_SIZE = 128;
            byte[] dxtBytes = new byte[ddsBytes.Length - DDS_HEADER_SIZE];
            System.Buffer.BlockCopy(ddsBytes, DDS_HEADER_SIZE, dxtBytes, 0, ddsBytes.Length - DDS_HEADER_SIZE);
            ddsBytes = null;

            //Texture2D texture = new Texture2D(width, height, format, false);
            //aTexture.Resize(width, height, format, false);

            // load the pixel bytes into the texture
            aTexture.LoadRawTextureData(dxtBytes);
            aTexture.Apply(false, true);

            return true;
        }

        public static TextureParams ParamsFromDDSBytes(byte[] ddsBytes)
        {
            // check it's a DDS header
            int startbyte = 4;
            byte ddsSizeCheck = ddsBytes[startbyte];
            if (ddsSizeCheck != 124)
            {
                Debug.LogWarning("Error: LoadTextureDXT: Invalid DDS texture, dds file header invalid.");
                return new TextureParams();
            }

            // format
            int startformatbyte = 76;// startbyte + (8 * 4) + 1;
            byte formatSizeCheck = ddsBytes[startformatbyte];
            if (formatSizeCheck != (byte)32)
            {
                Debug.LogWarning("Error: LoadTextureDXT: Invalid DDS texture, dds file format data not found.");
                return new TextureParams();
            }

            byte[] fourccbytes = new byte[4];
            System.Buffer.BlockCopy(ddsBytes, 84, fourccbytes, 0, 4);
            string fourccstr = System.Text.Encoding.ASCII.GetString(fourccbytes);

            if (fourccstr != "DXT1" && fourccstr != "DXT5")
            {
                Debug.LogWarning("Error: LoadTextureDXT: Invalid DDS texture, dds file format data not found.");
                return new TextureParams();
            }

            TextureParams texparams = new TextureParams();
            texparams.format = fourccstr == "DXT1" ? TextureFormat.DXT1 :
                                   fourccstr == "DXT5" ? TextureFormat.DXT5 :
                                   TextureFormat.ARGB32;

            // dimensions
            texparams.height = ddsBytes[13] * 256 + ddsBytes[12];
            texparams.width = ddsBytes[17] * 256 + ddsBytes[16];

            return texparams;
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
