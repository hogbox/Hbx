//----------------------------------------------
//            Hbx: Ext
// Copyright © 2017-2018 Hogbox Studios
// RectExt.cs
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hbx.Ext
{

    public static class RectExt
    {
        public static Vector3[] tempCorners = new Vector3[4];
        public static Rect tempRect = new Rect();
        public static Vector2 tempVec2 = new Vector2();
        public static float xmin = 0f;
        public static float xmax = 0f;
        public static float ymin = 0f;
        public static float ymax = 0f;

        public static void SetFromPoint(ref Rect aRect, ref Vector2 aPoint)
        {
            aRect.Set(aPoint.x, aPoint.y, float.Epsilon, float.Epsilon);
        }
        public static void SetFromPoint(ref Rect aRect, ref Vector3 aPoint, PlaneAxis anAxis = PlaneAxis.XY)
        {
            if (anAxis == PlaneAxis.XY)
            {
                aRect.Set(aPoint.x, aPoint.y, float.Epsilon, float.Epsilon);
            }
            else if (anAxis == PlaneAxis.XZ)
            {
                aRect.Set(aPoint.x, aPoint.z, float.Epsilon, float.Epsilon);
            }
            else if (anAxis == PlaneAxis.ZY)
            {
                aRect.Set(aPoint.z, aPoint.y, float.Epsilon, float.Epsilon);
            }
        }

        public static void Set(ref Rect aRect, ref Vector2 aVector, float aWidth, float aHeight)
        {
            aRect.Set(aVector.x, aVector.y, aWidth, aHeight);
        }

        public static void EncapsulatePoint(ref Rect aRect, ref Vector3 aPoint, PlaneAxis anAxis = PlaneAxis.XY)
        {
            if (anAxis == PlaneAxis.XY)
            {
                EncapsulatePoint(ref aRect, aPoint.x, aPoint.y);
            }
            else if(anAxis == PlaneAxis.XZ)
            {
                EncapsulatePoint(ref aRect, aPoint.x, aPoint.z);
            }
            else if (anAxis == PlaneAxis.ZY)
            {
                EncapsulatePoint(ref aRect, aPoint.z, aPoint.y);
            }
        }

        public static void EncapsulatePoint(ref Rect aRect, ref Vector2 aPoint)
        {
            //if(!aRect.Contains(aPoint, true))
            {
                EncapsulatePoint(ref aRect, aPoint.x, aPoint.y);
            }
        }

        public static void EncapsulatePoint(ref Rect aRect, float x, float y)
        {
            //if(!aRect.Contains(aPoint, true))
            {
                xmin = aRect.xMin; xmax = aRect.xMax;
                ymin = aRect.yMin; ymax = aRect.yMax;

                xmin = Mathf.Min(x, xmin);
                xmax = Mathf.Max(x, xmax);

                ymin = Mathf.Min(y, ymin);
                ymax = Mathf.Max(y, ymax);

                aRect.Set(xmin, ymin, xmax - xmin, ymax - ymin);
            }
        }
            
        public static void EncapsulatePoints(ref Rect aRect, ref Vector3[] aPointsArray, PlaneAxis anAxis = PlaneAxis.XY)
        {
            SetFromPoint(ref aRect, ref aPointsArray[0]);
            for(int i=1; i<aPointsArray.Length; i++)
                EncapsulatePoint(ref aRect, ref aPointsArray[i], anAxis);   
        }
    
        public static void EncapsulatePoints(ref Rect aRect, ref Vector2[] aPointsArray)
        {
            SetFromPoint(ref aRect, ref aPointsArray[0]);
            for(int i=1; i<aPointsArray.Length; i++)
                EncapsulatePoint(ref aRect, ref aPointsArray[i]);   
        }

        public static void EncapsulatePoints(ref Rect aRect, ref List<Vector3> aPointsArray, PlaneAxis anAxis = PlaneAxis.XY)
        {
            Vector3Ext.temp = aPointsArray[0];
            SetFromPoint(ref aRect, ref Vector3Ext.temp);
            for(int i=1; i<aPointsArray.Count; i++)
            {
                Vector3Ext.temp = aPointsArray[i];
                EncapsulatePoint(ref aRect, ref Vector3Ext.temp, anAxis);
            }
        }
    
        public static void EncapsulatePoints(ref Rect aRect, ref List<Vector2> aPointsArray)
        {
            Vector2Ext.temp = aPointsArray[0];
            SetFromPoint(ref aRect, ref Vector2Ext.temp);
            for(int i=1; i<aPointsArray.Count; i++)
            {
                Vector2Ext.temp = aPointsArray[i];
                EncapsulatePoint(ref aRect, ref Vector2Ext.temp);
            }
        }
    
        public static void EncapsulateRect(ref Rect aRect, ref Rect bRect)
        {
            xmin = aRect.xMin;xmax = aRect.xMax;
            ymin = aRect.yMin;ymax = aRect.yMax;
            
            if(bRect.xMin < xmin)
                xmin = bRect.xMin;
            else if(bRect.xMax > xmax)
                xmax = bRect.xMax;
            if(bRect.yMin < ymin)
                ymin = bRect.yMin;
            else if(bRect.yMax > ymax)
                ymax = bRect.yMax;
            aRect.Set(xmin,ymin, xmax-xmin, ymax-ymin);
        }

        public static void GetCorners(ref Rect aRect, ref Vector3[] refCorners)
        {
            GetCorners(ref aRect, ref refCorners, 0);
        }

        public static void GetCorners(ref Rect aRect, ref Vector3[] refCorners, int aStart)
        {
            xmin = aRect.xMin;xmax = aRect.xMax;
            ymin = aRect.yMin;ymax = aRect.yMax;
    
            refCorners[aStart].Set(xmin, ymin, 0f);
            refCorners[aStart+1].Set(xmax, ymin, 0f);
            refCorners[aStart+2].Set(xmax, ymax, 0f);
            refCorners[aStart+3].Set(xmin, ymax, 0f);
        }

        public static void GetCorners(ref Rect aRect, ref Vector2[] refCorners)
        {
            GetCorners(ref aRect, ref refCorners, 0);
        }

        public static void GetCorners(ref Rect aRect, ref Vector2[] refCorners, int aStart)
        {
            xmin = aRect.xMin;xmax = aRect.xMax;
            ymin = aRect.yMin;ymax = aRect.yMax;
                
            refCorners[aStart].Set(xmin, ymin);
            refCorners[aStart+1].Set(xmax, ymin);
            refCorners[aStart+2].Set(xmax, ymax);
            refCorners[aStart+3].Set(xmin, ymax);
        }

        //
        // calculate the scale factor required to get rfit to fit inside rtarget
        // maitaining the aspect of rfit
        //
        public static float ScaleToAspectFitRectInRect(Rect rfit, Rect rtarget)
        {
            // first try to match width
            float s = rtarget.width / rfit.width;
            // if we scale the height to make the widths equal, does it still fit?
            if(rfit.height * s <= rtarget.height)
            {
                return s;
            }
            // no, match height instead
            return rtarget.height / rfit.height;
        }

        /// <summary>
        /// Returns a rect at aspect ratio of rfit that fits inside rtarget.
        /// </summary>
        /// <param name="rfit">Rfit.</param>
        /// <param name="rtarget">Rtarget.</param>

        public static void AspectFitRectInRect(ref Rect rfit, Rect rtarget)
        {
            float s = ScaleToAspectFitRectInRect(rfit, rtarget);
            var w = rfit.width * s;
            var h = rfit.height * s;
            var x = rtarget.center.x - w / 2.0f;
            var y = rtarget.center.y - h / 2.0f;
            rfit.Set(x, y, w, h);
        }

        public static void ClampWidthHeight(ref Rect aRect, float aSize, bool maintainAspect)
        {
            float xscaler = aRect.height > aRect.width ? aSize / aRect.height : 1f;
            float yscaler = aRect.width > aRect.height ? aSize / aRect.width : 1f;
                
            bool clamped = false;
            if(aRect.width > aSize)
            {
                aRect.width = aSize;
                clamped=true;
            }
            if(aRect.height > aSize)
            {
                aRect.height = aSize;
                clamped=true;
             }
    
            if(maintainAspect && clamped)
            {
                aRect.width *= xscaler;
                aRect.height *= yscaler;
            }
        }
    }

} // end Hbx Ext namespaxe
