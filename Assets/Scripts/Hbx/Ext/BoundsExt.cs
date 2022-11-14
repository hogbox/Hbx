//----------------------------------------------
//            Hbx: Ext
// Copyright © 2017-2018 Hogbox Studios
// BoundsExt.cs
//----------------------------------------------

using UnityEngine;

namespace Hbx.Ext
{

    public static class BoundsExt
    {
        public static Bounds tempBounds = new Bounds();
        public static Vector3[] tempCorners = new Vector3[8];
        public static Rect tempRect = new Rect();
        public static float xmin = 0f;
        public static float xmax = 0f;
        public static float ymin = 0f;
        public static float ymax = 0f;
        public static float zmin = 0f;
        public static float zmax = 0f;
    
        public static void Set(ref Bounds aToBounds, Bounds aFromBounds)
        {
            aToBounds.center = aFromBounds.center;
            aToBounds.extents = aFromBounds.extents;
        }

        public static void GetCorners(ref Bounds aBounds, ref Vector3[] refCorners)
        {
            GetCorners(ref aBounds, ref refCorners, 0);
        }

        public static void GetCorners(ref Bounds aBounds, ref Vector3[] refCorners, int aStart)
        {
            xmin = aBounds.center.x + -aBounds.extents.x;
            xmax = aBounds.center.x + aBounds.extents.x;
            ymin = aBounds.center.y + -aBounds.extents.y;
            ymax = aBounds.center.y + aBounds.extents.y;
            zmin = aBounds.center.z + -aBounds.extents.z;
            zmax = aBounds.center.z + aBounds.extents.z;
                
            refCorners[aStart].Set(xmin, ymin, zmin);
            refCorners[aStart+1].Set(xmax, ymin, zmin);
            refCorners[aStart+2].Set(xmax, ymax, zmin);
            refCorners[aStart+3].Set(xmin, ymax, zmin);
    
            refCorners[aStart+4].Set(xmin, ymin, zmax);
            refCorners[aStart+5].Set(xmax, ymin, zmax);
            refCorners[aStart+6].Set(xmax, ymax, zmax);
            refCorners[aStart+7].Set(xmin, ymin, zmax);
        }

        public static void GetScreenSpaceCorners(ref Bounds aBounds, Camera aCamera, ref Vector3[] refCorners)
        {
            GetScreenSpaceCorners(ref aBounds, aCamera, ref refCorners, 0);
        }

        public static void GetScreenSpaceCorners(ref Bounds aBounds, Camera aCamera, ref Vector3[] refCorners, int aStart)
        {
            GetCorners(ref aBounds, ref refCorners, aStart);
            Vector3Ext.WorldToScreenSpace(ref refCorners, aCamera, aStart, 8);
        }

        public static void GetViewSpaceCorners(ref Bounds aBounds, Camera aCamera, ref Vector3[] refCorners)
        {
            GetViewSpaceCorners(ref aBounds, aCamera, ref refCorners, 0);
        }

        public static void GetViewSpaceCorners(ref Bounds aBounds, Camera aCamera, ref Vector3[] refCorners, int aStart)
        {
            GetCorners(ref aBounds, ref refCorners, aStart);
            for(int i=aStart; i<aStart+8; i++)
                refCorners[i] = aCamera.WorldToViewportPoint(refCorners[i]);
        }

        public static void ComputeScreenSpaceRect(ref Bounds bounds, Camera aCamera, ref Rect refRect)
        {
            refRect.Set(0f,0f,0f,0f);
            GetScreenSpaceCorners(ref bounds, aCamera, ref tempCorners);
            RectExt.EncapsulatePoints(ref refRect, ref tempCorners);
            refRect.Set(Mathf.Floor(refRect.xMin), Mathf.Floor(refRect.yMin), Mathf.Floor(refRect.width), Mathf.Floor(refRect.height));
        }

        public static void ComputeViewSpaceRect(ref Bounds bounds, Camera aCamera, ref Rect refRect)
        {
            refRect.Set(0f,0f,0f,0f);
            GetViewSpaceCorners(ref bounds, aCamera, ref tempCorners);
            RectExt.EncapsulatePoints(ref refRect, ref tempCorners);
            refRect.Set(refRect.xMin,refRect.yMax, refRect.width, refRect.height);
        }

        public static void ComputeScreenSpaceRectCorners(ref Bounds aBounds, Camera aCamera, ref Vector3[] refCorners)
        {
            ComputeScreenSpaceRectCorners(ref aBounds, aCamera, ref refCorners, 0);
        }

        public static void ComputeScreenSpaceRectCorners(ref Bounds aBounds, Camera aCamera, ref Vector3[] refCorners, int aStart)
        {
            Vector3 centerInCamera = aCamera.transform.InverseTransformPoint(aBounds.center);
            ComputeScreenSpaceRect(ref aBounds, aCamera, ref tempRect);
            RectExt.GetCorners(ref tempRect, ref refCorners, aStart);
            //fix z
            for(int i=aStart; i<aStart+4; i++)
                refCorners[i].z = centerInCamera.z;//aBounds.center.z;//?
        }

        public static void ComputeWorldSpaceScreenRectCorners(ref Bounds aBounds, Camera aCamera, ref Vector3[] refCorners)
        {
            ComputeWorldSpaceScreenRectCorners(ref aBounds, aCamera, ref refCorners, 0);
        }

        public static void ComputeWorldSpaceScreenRectCorners(ref Bounds aBounds, Camera aCamera, ref Vector3[] refCorners, int aStart)
        {
            ComputeScreenSpaceRectCorners(ref aBounds, aCamera, ref refCorners, aStart);
            for(int i=aStart; i<aStart+4; i++)
                refCorners[i] = aCamera.ScreenToWorldPoint(refCorners[i]);
        }
    }

} // end Hbx Ext namespace
