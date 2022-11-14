//----------------------------------------------
//            Hbx: Ext
// Copyright © 2017-2018 Hogbox Studios
// TransformExt.cs
//----------------------------------------------

using UnityEngine;

namespace Hbx.Ext
{
    public static class TransformExt
    {
        public static Transform GetOrCreateChild(this Transform aTransform, string aPath)
        {
            // split the path
            string[] els = aPath.Split('/');

            Transform current = aTransform;

            // check if each level of the path exists
            for(int i=0; i<els.Length; i++)
            {
                Transform t = current.Find(els[i]);
                if(t == null)
                {
                    GameObject go = new GameObject(els[i]);
                    go.transform.SetParent(current, false);
                    t = go.transform;
                }

                current = t;
            }
            return current;
        }

        /// <summary>
        /// Set transform component from TRS matrix.
        /// </summary>
        /// <param name="transform">Transform component.</param>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>

        public static void SetTransformFromMatrix(this Transform transform, ref Matrix4x4 matrix)
        {
            transform.localPosition = matrix.GetTranslation();
            transform.localRotation = matrix.GetRotation();
            transform.localScale = matrix.GetScale();
        }
    }
} // end Hbx Ext namespace
