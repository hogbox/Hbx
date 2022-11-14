//----------------------------------------------
//            Hbx: Assets
// Copyright © 2017-2018 Hogbox Studios
// TexIOManger.cs
//----------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using Hbx.Ext;

namespace Hbx.Assets
{
    /// <summary>
    /// IO Manager for reading and writing texture data
    /// </summary>

    public class TexIOManger : GenericIOManager<TexIOManger>
    {
        /// <summary>
        /// Reads a texture file from a http:// or file:// path returning the bytes as a texture.
        /// </summary>
        /// <param name="aPath">A path.</param>
        /// <param name="aTexture">An exisitng texture, if null a new texture is created.</param>
        /// <param name="aCompleteCallback">A complete callback, returns success bool and texture2D if succesful.</param>

        public void ReadWebTexture(string aPath, bool mipmaps, Texture2D anExistingTexture, int id, Action<bool, Texture2D, int> aCompleteCallback)
        {
            if(aPath.StartsWith("$")) 
            {
                //CreateFromTexGenString(aPath, anExistingTexture, id, aCompleteCallback);
                return;
            }

            StartCoroutine(_ReadWebData(aPath, (bool success, byte[] data) =>
            {
                if (!success)
                {
                    Debug.LogWarning("ReadWebTexture: Failed to read texture bytes at path '" + aPath + "'. No texture will be loaded");
                    aCompleteCallback(false, null, id);
                    return;
                }
                Texture2D texture = anExistingTexture;

                if (texture == null)
                {
                    // determine format
                    TextureExt.Format format = TextureExt.FormatForFile(aPath);
                    // if it's compressed allocate a texture of correct dimensions
                    if (format == TextureExt.Format.DDS)
                    {
                        TextureExt.TextureParams texparams = TextureExt.ParamsFromTextureBytes(data, format);
                        texture = new Texture2D(texparams.width, texparams.height, texparams.format, mipmaps);
                    }
                    else
                    {
                        texture = new Texture2D(2, 2, TextureFormat.RGB24, mipmaps);
                    }
                }

                bool loaded = texture.LoadImage(data, aPath);
                aCompleteCallback(loaded, texture, id);
            }));
        }

        /// <summary>
        /// Reads a texture file from a disk path returning the bytes as a texture
        /// </summary>
        /// <param name="aPath">A path.</param>
        /// <param name="aCompleteCallback">A complete callback.</param>

        public void ReadDiskTexture(string aPath, bool mipmaps, Texture2D anExistingTexture, int id, Action<bool, Texture2D, int> aCompleteCallback)
        {
            if (aPath.StartsWith("$"))
            {
                //CreateFromTexGenString(aPath, anExistingTexture, id, aCompleteCallback);
                return;
            }

            StartCoroutine(_ReadDiskData(aPath, (bool success, byte[] data) =>
            {
                Texture2D texture = anExistingTexture;

                if (texture == null)
                {
                    // determine format
                    TextureExt.Format format = TextureExt.FormatForFile(aPath);
                    // if it's compressed allocate a texture of correct dimensions
                    if (format == TextureExt.Format.DDS)
                    {
                        TextureExt.TextureParams texparams = TextureExt.ParamsFromTextureBytes(data, format);
                        texture = new Texture2D(texparams.width, texparams.height, texparams.format, mipmaps);
                    }
                    else
                    {
                        texture = new Texture2D(2, 2, TextureFormat.RGB24, mipmaps);
                    }
                }

                bool loaded = texture.LoadImage(data, aPath);
                aCompleteCallback(loaded, texture, id);
            }));
        }

        /// <summary>
        /// Write the Texture2D to disk in the format specified by aPaths file extension
        /// </summary>
        /// <param name="aPath">A path.</param>
        /// <param name="aTexture">A texture.</param>
        /// <param name="aCompleteCallback">A complete callback.</param>

        public void WriteDiskTexture(string aPath, Texture2D aTexture, Action<bool> aCompleteCallback)
        {
            TextureExt.Format format = TextureExt.FormatForFile(aPath);
            if (format == TextureExt.Format.DDS || format == TextureExt.Format.UNKOWN)
            {
                Debug.LogWarning("Error: Writing texture file format " + format.ToString() + " is not supported.");
                aCompleteCallback(false);
                return;
            }
            byte[] writebytes = aTexture.EncodeTextureToFileBytes(format);
            StartCoroutine(_WriteDiskData(aPath, writebytes, aCompleteCallback));
        }

        /*
        public void CreateFromTexGenString(string aPath, Texture2D anExistingTexture, int id, Action<bool, Texture2D, int> aCompleteCallback)
        {
            // check it's a texgen url
            string[] els = aPath.Split('^');
            aPath = string.Empty;

            if (els.Length < 2)
            {
                if (aCompleteCallback != null) aCompleteCallback(false, null, id);
                return;
            }

            if (els[0] == "$texgen")
            {
                Tex.TexGen.TexGenParams genParams = JsonUtils.DeserializeObject<Tex.TexGen.TexGenParams>(els[1].Replace('\'','"'), null);

                if (genParams._name == "checker")
                {
                    // do we have any colors
                    Color c1 = genParams._colors != null && genParams._colors.Length > 0 ? genParams._colors[0] : Color.white;
                    Color c2 = genParams._colors != null && genParams._colors.Length > 1 ? genParams._colors[1] : Color.black;

                    if(anExistingTexture)
                    {
                        anExistingTexture.Reinitialize(64, 64);
                        Tex.TexGen.FillCheckerTexture(anExistingTexture, c1, c2, 2, 2);
                        if (aCompleteCallback != null) aCompleteCallback(true, anExistingTexture, id);
                        return;
                    }
                    else
                    {
                        Texture2D checkerTex = Tex.TexGen.CreateCheckerTexture(c1, c2, 64, 64, 2, 2);
                        if (aCompleteCallback != null) aCompleteCallback(true, checkerTex, id);
                        return;
                    }
                }
                else if (genParams._name == "solid")
                {
                    // do we have any colors
                    Color c1 = genParams._colors != null && genParams._colors.Length > 0 ? genParams._colors[0] : Color.white;

                    if (anExistingTexture)
                    {
                        anExistingTexture.Reinitialize(64, 64);
                        Tex.TexGen.FillColorTexture(anExistingTexture, c1);
                        if (aCompleteCallback != null) aCompleteCallback(true, anExistingTexture, id);
                        return;
                    }
                    else
                    {
                        Texture2D checkerTex = Tex.TexGen.CreateColorTexture(c1, 64, 64);
                        if (aCompleteCallback != null) aCompleteCallback(true, checkerTex, id);
                        return;
                    }
                }

            }
            else if(els[0] == "$resources")
            {
                string resourcepath = els[1];
                resourcepath = resourcepath.TrimStart('/');
                Texture2D resourcetex = Resources.Load<Texture2D>(resourcepath);

                // copy the resource tex into the exisitng tex if one is provided
                if (resourcetex != null && anExistingTexture != null)
                {
                    anExistingTexture.Reinitialize(resourcetex.width, resourcetex.height);
                    Tex.TexUtils.CopyToTex2D(resourcetex, anExistingTexture);
                    //Destroy(resourcetex);
                    Resources.UnloadAsset(resourcetex);
                    resourcetex = null;
                    if (aCompleteCallback != null) aCompleteCallback(true, anExistingTexture, id);
                }
                else
                {
                    if (aCompleteCallback != null) aCompleteCallback(resourcetex != null, resourcetex, id);
                }
                return;
            }
            else if(els[0] == "$base64")
            {
                string[] headerDataPair = els[1].Split(',');
                els[1] = string.Empty;

                string header = headerDataPair[0].ToLower().Trim();

                TextureExt.Format format = TextureExt.Format.UNKOWN;

                if(header == "jpeg")
                {
                    format = TextureExt.Format.JPG;
                }
                else if (header == "png")
                {
                    format = TextureExt.Format.PNG;
                }

                if (format == TextureExt.Format.UNKOWN)
                {
                    if (aCompleteCallback != null) aCompleteCallback(false, null, id);
                    return;
                }

                // convert to bytes and load into texture
                byte[] base64Bytes = System.Convert.FromBase64String(headerDataPair[1]);

                Texture2D base64Tex = anExistingTexture ?? new Texture2D(2, 2, TextureFormat.RGB24, false);
                base64Tex.LoadImage(base64Bytes, format);
                base64Bytes = null;
                if (aCompleteCallback != null) aCompleteCallback(true, base64Tex, id);
                return;
            }

            if (aCompleteCallback != null) aCompleteCallback(false, null, id);
        }*/
    }

} // end Hbx Asset namespace
