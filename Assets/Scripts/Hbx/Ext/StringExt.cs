//----------------------------------------------
//            Hbx: Ext
// Copyright © 2017-2018 Hogbox Studios
// StringExt.cs
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hbx.Ext
{

    public static class StringExt
    {

        /// <summary>
        /// Return all the indexes of sub string in string.
        /// </summary>
        /// <returns>An array of indexes.</returns>
        /// <param name="aString">A string.</param>
        /// <param name="aSubString">A sub string.</param>
        /// <param name="ignoreCase">If set to <c>true</c> ignore case.</param>
    
        public static int[] AllIndexesOf(this string aString, string aSubString, bool ignoreCase = false)
        {
            if (string.IsNullOrEmpty(aString) || string.IsNullOrEmpty(aSubString))
            {
                return null;
            }
        
            var indexes = new List<int>();
            int index = 0;
        
            while ((index = aString.IndexOf(aSubString, index, ignoreCase ? System.StringComparison.OrdinalIgnoreCase : System.StringComparison.Ordinal)) != -1)
            {
                indexes.Add(index++);
            }
        
            return indexes.ToArray();
        }

        /// <summary>
        /// Find the index of the first string encountered containing sub string, start search from start index
        /// </summary>
        /// <returns>The index of string containing substring, -1 if not found.</returns>
        /// <param name="aStringList">A string list.</param>
        /// <param name="aSubString">A sub string.</param>

        public static int IndexOfContaining(this string[] aStringList, string aSubString, int aStartIndex = 0)
        {
            for(int i=aStartIndex; i<aStringList.Length; i++)
            {
                if(aStringList[i].Contains(aSubString)) return i;
            }
            return -1;
        }

        public static bool AnyWordStartingWith(this string aString, string searchString)
        {
            if(aString.StartsWith(searchString)) return true;
            string[] searchWords = searchString.Split(',');
            foreach (string item in aString.Split(' '))
            {
                foreach(string searchword in searchWords)
                {
                        
                    if (item.StartsWith(searchword))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

} // end Hbx Ext namespace
