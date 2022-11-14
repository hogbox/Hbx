//----------------------------------------------
//            Hbx: Ext
// Copyright © 2017-2018 Hogbox Studios
// MeshExt.cs
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hbx.Ext
{

    public static class MeshExt
    {
        public static void BuildQuad(this Mesh aMesh, Vector3[] aCornersArray, Vector2[] aUVArray)
        {
            aMesh.vertices = aCornersArray;
            aMesh.uv = aUVArray;
            aMesh.SetTriangles(new int[6]{0,3,2, 2,1,0},0);
        }

        public static void BuildMerged(this Mesh aMesh, GameObject aGameObject)
        {
            MeshFilter[] meshFilters = aGameObject.GetComponentsInChildren<MeshFilter>(true);
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];
            int i = 0;
            while (i < meshFilters.Length)
            {
                combine[i].mesh = meshFilters[i].sharedMesh;
                combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
                i++;
            }
            aMesh.CombineMeshes(combine, true, true, false);
        }
    }

} // end Hbx Ext namespace