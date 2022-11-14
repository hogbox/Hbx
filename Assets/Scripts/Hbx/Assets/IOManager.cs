//----------------------------------------------
//            Hbx: Assets
// Copyright © 2017-2018 Hogbox Studios
// IOManager.cs
//----------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Hbx.Generic;

namespace Hbx.Assets
{

    /// <summary>
    /// Class for reading and writing data to various IO sources (disk, web etc)
    /// </summary>

    public abstract class GenericIOManager<T> : GenericSingletonBehaviour<T> where T : GenericIOManager<T>, new()
    {
        /// <summary>
        /// Reads a file from a http:// or file:// path returning the bytes
        /// </summary>
        /// <param name="aPath">A path.</param>
        /// <param name="aCompleteCallback">A complete callback, returns success bool and byte array if succesful.</param>

        public void ReadWebData(string aPath, Action<bool, byte[]> aCompleteCallback)
        {
            StartCoroutine(_ReadWebData(aPath, aCompleteCallback));
        }

        /// <summary>
        /// Reads a file from a http:// or file:// path returning the bytes as UTF8 string
        /// </summary>
        /// <param name="aPath">A path.</param>
        /// <param name="aCompleteCallback">A complete callback, returns success bool and UTF8 string if succesful.</param>

        public void ReadWebText(string aPath, Action<bool, string> aCompleteCallback)
        {
            StartCoroutine(_ReadWebData(aPath, (bool success, byte[] data) =>
            {
                string readstr = success ? System.Text.Encoding.UTF8.GetString(data) : "";
                aCompleteCallback(success, readstr);
            }));
        }

        /// <summary>
        /// Coroutine function to perform the actual download of data using UnityWebRequest
        /// </summary>
        /// <param name="aPath">A path.</param>
        /// <param name="aCompleteCallback">A complete callback, returns success bool and byte array if succesful.</param>

        protected IEnumerator _ReadWebData(string aPath, Action<bool, byte[]> aCompleteCallback)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(Paths.ResolvePath(aPath, false)))
            {
                UnityWebRequestAsyncOperation op = www.SendWebRequest();
                yield return op;
    
                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.LogWarning("_ReadWebData: For url '" + aPath + "' returned error - '" + www.error + "'.");
                    aCompleteCallback(false, null);
                }
                else
                {
                    // Or retrieve results as binary data
                    //byte[] results = www.downloadHandler.data;
                    aCompleteCallback(true, www.downloadHandler.data);
                }
            }
        }

        public void PostWebText(string aPath, string postData, Action<bool, string> aCompleteCallback)
        {
            StartCoroutine(_PostWebData(aPath, postData, (bool success, byte[] data) =>
            {
                string readstr = success ? System.Text.Encoding.UTF8.GetString(data) : "";
                aCompleteCallback(success, readstr);
            }));
        }

        protected IEnumerator _PostWebData(string aPath, string postData, Action<bool, byte[]> aCompleteCallback)
        {
            UnityWebRequest www = new UnityWebRequest(Paths.ResolvePath(aPath, false));
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(postData));
            www.downloadHandler = new DownloadHandlerBuffer();
            www.method = UnityWebRequest.kHttpVerbPOST;
            www.SetRequestHeader("Content-Type", "application/json");

            UnityWebRequestAsyncOperation op = www.SendWebRequest();
            yield return op;

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.LogWarning("_PostWebData: For url '" + aPath + "' returned error - '" + www.error + "'.");
                aCompleteCallback(false, null);
            }
            else
            {
                // success, return any response
                aCompleteCallback(true, www.downloadHandler.data);
            }

            /*using (UnityWebRequest www = UnityWebRequest.Post(Paths.ResolvePath(aPath, false), postData))
            {
                //www.uploadHandler.contentType = "application/json; utf-8";
                www.SetRequestHeader("Content-Type", "application/json");

                UnityWebRequestAsyncOperation op = www.SendWebRequest();
                yield return op;

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.LogWarning("_PostWebData: For url '" + aPath + "' returned error - '" + www.error + "'.");
                    aCompleteCallback(false, null);
                }
                else
                {
                    // success, return any response
                    aCompleteCallback(true, www.downloadHandler.data);
                }
            }*/
        }

        /// <summary>
        /// Reads a file from a disk path returning the bytes
        /// </summary>
        /// <param name="aPath">A path.</param>
        /// <param name="aCompleteCallback">A complete callback, returns success bool and byte array if succesful.</param>

        public void ReadDiskData(string aPath, Action<bool, byte[]> aCompleteCallback)
        {
            StartCoroutine(_ReadDiskData(aPath, aCompleteCallback));
        }

        /// <summary>
        /// Reads a file from a disk path returning the bytes as UTF8 string
        /// </summary>
        /// <param name="aPath">A path.</param>
        /// <param name="aCompleteCallback">A complete callback, returns success bool and UTF8 string if succesful.</param>

        public void ReadDiskText(string aPath, Action<bool, string> aCompleteCallback)
        {
            StartCoroutine(_ReadDiskData(aPath, (bool success, byte[] data) =>
            {
                string readstr = success ? System.Text.Encoding.UTF8.GetString(data) : "";
                aCompleteCallback(success, readstr);
            }));
        }

        /// <summary>
        /// Coroutine function to perform the actual data read on another thread if supported
        /// </summary>
        /// <param name="aPath">A path.</param>
        /// <param name="aCompleteCallback">A complete callback, returns success bool and byte array if succesful.</param>

        protected IEnumerator _ReadDiskData(string aPath, Action<bool, byte[]> aCompleteCallback)
        {
            if(!File.Exists(aPath))
            {
                aCompleteCallback(false, null);
                yield break;
            }

            ReadDiskBytesThreadOp readOp = new ReadDiskBytesThreadOp(aPath);
            readOp.Start();

            while(readOp.IsRunning)
            {
                yield return null;
            }
            aCompleteCallback(readOp.Bytes != null, readOp.Bytes);
        }

        //
        // Write

        /// <summary>
        /// Writes bytes to a file on disk at the specified path
        /// </summary>
        /// <param name="aPath">A file path.</param>
        /// <param name="aBytesResult">Bytes to write.</param>
        /// <param name="aCompleteCallback">A complete callback, returns success bool.</param>

        public void WriteDiskData(string aPath, byte[] aBytesResult, Action<bool> aCompleteCallback)
        {
            StartCoroutine(_WriteDiskData(aPath, aBytesResult, aCompleteCallback));
        }

        /// <summary>
        /// Writes text to a file on disk at the specified path
        /// </summary>
        /// <param name="aPath">A file path.</param>
        /// <param name="aString">String to write.</param>
        /// <param name="aCompleteCallback">A complete callback, returns success bool.</param>

        public void WriteDiskText(string aPath, string aString, Action<bool> aCompleteCallback)
        {
            byte[] writebytes = System.Text.Encoding.UTF8.GetBytes(aString);
            StartCoroutine(_WriteDiskData(aPath, writebytes, aCompleteCallback));
        }

        /// <summary>
        /// Coroutine function to perform the actual data write on another thread if supported
        /// </summary>
        /// <param name="aPath">A path.</param>
        /// <param name="aString">String to write.</param>
        /// <param name="aCompleteCallback">A complete callback, returns success bool.</param>

        protected IEnumerator _WriteDiskData(string aPath, byte[] aBytesResult, Action<bool> aCompleteCallback)
        {
            WriteDiskBytesThreadOp writeOp = new WriteDiskBytesThreadOp(aPath, aBytesResult);
            writeOp.Start();
            while(writeOp.IsRunning)
            {
                yield return null;
            }
            if(aCompleteCallback != null) aCompleteCallback(true);
        }
    }

    /// <summary>
    /// Conrete type to be used for basic IO
    /// </summary>

    public class IOManager : GenericIOManager<IOManager>
    {
    }

} // end Hbx Asset namespace