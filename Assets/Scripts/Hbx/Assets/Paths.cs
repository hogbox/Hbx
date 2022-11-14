//----------------------------------------------
//            Hbx: Assets
// Copyright © 2017-2018 Hogbox Studios
// Paths.cs
//----------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hbx.Assets
{
    /// <summary>
    /// Tools for working with paths in Hbx Unity apps
    /// </summary>

    public static class Paths
    {
        public const string kFileProtocolString = "file://localhost/";
        public const string kHttpProtocolString = "http://";
        public const string kHttpsProtocolString = "https://";


#if UNITY_WEBGL && !UNITY_EDITOR
        public static string localFileProtocol
        {
            get
            {
                return ProtocolForPath(absoluteURL);
            }
        }
#else
        public static string localFileProtocol { get { return kFileProtocolString; } }
#endif

        public static readonly string[] kProtocolsList = new string[] { kFileProtocolString, kHttpProtocolString, kHttpsProtocolString };


        public static string dataPath
        {
            get
            {
                return TrimUrlPageTag(Application.dataPath);
            }
        }

        static string _streamingAssetsPath = Application.streamingAssetsPath;

        public static string streamingAssetsPath
        {
            get
            {
                return TrimUrlPageTag(_streamingAssetsPath);
            }
            set
            {
                _streamingAssetsPath = value;
            }
        }

        public static string absoluteURL
        {
            get
            {
                return TrimUrlPageTag(Application.absoluteURL);
            }
        }

        public static string persistentDataPath
        {
            get
            {
                return Application.persistentDataPath;
            }
        }

        public static string temporaryCachePath
        {
            get
            {
                return Application.temporaryCachePath;
            }
        }


        /// <summary>
        /// Removes any character after and including the # pagetag
        /// </summary>
        /// <returns>The url without any page tag.</returns>
        /// <param name="aUrl">A URL.</param>

        public static string TrimUrlPageTag(string aUrl)
        {
            int indexofhash = aUrl.LastIndexOf('#');
            return indexofhash == -1 ? aUrl : aUrl.Substring(0, indexofhash - 1);
        }

        public static string ResolvePath(string aPath, bool isDiskPath = true)
        {
            string resolved = aPath;

            // is it already a full path with protocol
            string protocol = ProtocolForPath(resolved);
            if(!string.IsNullOrEmpty(protocol))
            {
                if(isDiskPath && protocol == kFileProtocolString)
                {
                    resolved = StripProtocol(resolved);
                    return "/" + resolved;
                }
                return System.Uri.EscapeUriString(resolved);
            }

            // it's a relative path use streaming assets
            if(!Path.IsPathRooted(resolved))
            {
                resolved = Path.Combine(Paths.streamingAssetsPath, resolved);
            }

            // again check if it's now a fully resolved path
            protocol = ProtocolForPath(resolved);
            if(!string.IsNullOrEmpty(protocol))
            {
                return System.Uri.EscapeUriString(resolved);
            }

            // rooted disk path, strip the leading / characters then peg on the file protocol
            resolved = resolved.TrimStart(new char[] { '/' });

            if(isDiskPath)
            {
                if(Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
                {
                    return resolved;
                }
                else
                {
                    return "/" + resolved;
                }
            }
            else
            {
                return System.Uri.EscapeUriString(localFileProtocol + resolved);
            } 
        }

        public static string ProtocolForPath(string aPath)
        {
            foreach(string p in kProtocolsList)
            {
                if(aPath.StartsWith(p, StringComparison.CurrentCulture)) return p;
            }
            return string.Empty;
        }

        public static string StripProtocol(string aPath)
        {
            string protocol = ProtocolForPath(aPath);
            if(!string.IsNullOrEmpty(protocol))
            {
                return aPath.Remove(0, protocol.Length);
            }
            return aPath;
        }

        public static void PrintPaths()
        {
            string pathsstr = "--- Start Path Report ---\n";
            System.Action<string, string> printPath = (string title, string path) =>
            {
                pathsstr += "    " + title + " = " + path + "\n";
            };

            printPath("protocol", localFileProtocol);
            printPath("data", Application.dataPath);
            printPath("streamingAssets", Application.streamingAssetsPath);
            printPath("persistant", Application.persistentDataPath);
            printPath("temporaryCache", Application.temporaryCachePath);
            printPath("absoluteURL", Application.absoluteURL);

            pathsstr += "--- End Path Report ---";

            Debug.Log(pathsstr);
        }
    }
}
