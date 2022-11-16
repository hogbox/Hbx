//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// HttpLoader.cs
//----------------------------------------------

using System;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

using Hbx.Ext;

namespace Hbx.Assets
{
    /// <summary>
    /// HttpLoader handles reading bytes as BytesResult from local disk file system
    /// Additionally the HttpLoader can read Textures using Unity DownloadHandlerTexture
    /// </summary>
    public class HttpLoader : FileLoader
    {
        public HttpLoader()
        {
        }

        //
        // ILoader interface
        //

        /// <summary>
        /// Handles bytes, strings and Texture2D
        /// </summary>
        public override Type[] types => new Type[] { typeof(byte[]), typeof(string), typeof(Texture2D) };

        /// <summary>
        /// Supports any extension as only deals with loading raw bytes
        /// </summary>
        public override string[] extensions => new string[] { ".png", ".jpeg", ".jpg" };

        /// <summary>
        /// Supports the http:// and https:// protocols
        /// </summary>
        public override string[] protocols => new string[] { Protocols.HTTP_PREFIX, Protocols.HTTPS_PREFIX };

        /// <summary>
        /// Read file for http server and return either btye array or string
        /// </summary>
        /// <param name="src">Url to a file on a http server</param>
        /// <param name="options">Currently no supported options so can be null</param>
        /// <returns>Depending on T returns either byte array or string of the contents of the file</returns>
        public override async Task<ILoaderResult<T>> ReadAsync<T>(string src, ILoaderOptions options)
        {
            UnityWebRequest wr = new UnityWebRequest(); // Completely blank
            wr.url = src;
            wr.method = UnityWebRequest.kHttpVerbGET;
            wr.useHttpContinue = false;
            //wr.chunkedTransfer = false;
            wr.redirectLimit = 10;
            wr.timeout = 60;

            if (typeof(T) == typeof(Texture2D))
            {
                wr.downloadHandler = new DownloadHandlerTexture(false);
            }
            else
            {
                wr.downloadHandler = new DownloadHandlerBuffer();
            }

            await wr.SendWebRequest();

            T result = default;

            if (wr.result != UnityWebRequest.Result.Success)
            {
                //Debug.Log(wr.error);
                return new ILoaderResult<T>();
            }
            else
            {
                // do we just want raw bytes
                if (typeof(T) == typeof(byte[]))
                {
                    byte[] bytes = wr.downloadHandler.data;
                    result = (T)(object)bytes;
                }
                // do we just want raw text
                else if(typeof(T) == typeof(string))
                {
                    string str = wr.downloadHandler.text;
                    result = (T)(object)str;
                }
                // do we want a texture2d
                else if(typeof(T) == typeof(Texture2D))
                {
                    DownloadHandlerTexture texd = (DownloadHandlerTexture)wr.downloadHandler;
                    result = (T)(object)texd.texture;
                }
            }

            return new ILoaderResult<T>(result);
        }
    }
}
