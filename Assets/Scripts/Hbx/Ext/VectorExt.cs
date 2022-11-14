//----------------------------------------------
//            Hbx: Ext
// Copyright © 2017-2018 Hogbox Studios
// VectorExt.cs
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hbx.Ext
{

    public static class Vector2Ext
    {
        public static readonly Vector2 zero = new Vector2(0f,0f);
        public static readonly Vector2 one = new Vector2(1f, 1f);
        public static readonly Vector2 invalid = new Vector2(float.MinValue, float.MinValue);

        public static Vector2 temp = Vector2.zero;

        public static void FromVector3(this Vector2 aVector2, ref Vector3 aVector3, PlaneAxis aPlane = PlaneAxis.XY)
        {
            switch(aPlane)
            {
                case PlaneAxis.XY:
                {
                    aVector2.x = aVector3.x; aVector2.y = aVector3.y;
                    break;
                }
                case PlaneAxis.XZ:
                {
                    aVector2.x = aVector3.x; aVector2.y = aVector3.z;
                    break;
                }
                case PlaneAxis.ZY:
                {
                    aVector2.x = aVector3.z; aVector2.y = aVector3.y;
                    break;
                }
                default: break;
            }
        }
    
        // todo, can above just call this, we need this one to call on elements in ref arrays
        public static void FromVector3(ref Vector2 aVector2, ref Vector3 aVector3, PlaneAxis aPlane = PlaneAxis.XY)
        {
            switch(aPlane)
            {
                case PlaneAxis.XY:
                {
                    aVector2.x = aVector3.x; aVector2.y = aVector3.y;
                    break;
                }
                case PlaneAxis.XZ:
                {
                    aVector2.x = aVector3.x; aVector2.y = aVector3.z;
                    break;
                }
                case PlaneAxis.ZY:
                {
                    aVector2.x = aVector3.z; aVector2.y = aVector3.y;
                    break;
                }
                default: break;
            }
        }

        public static Vector3 ToVector3(this Vector2 aVector2, PlaneAxis aPlane = PlaneAxis.XY, float defaultZ = 0.0f)
        {
            aVector2.ToVector3(ref Vector3Ext.temp, aPlane, defaultZ);
            return Vector3Ext.temp;
        }

        public static void ToVector3(this Vector2 aVector2, ref Vector3 aVector3, PlaneAxis aPlane = PlaneAxis.XY, float defaultZ = 0.0f)
        {
            Vector3Ext.FromVector2(ref aVector3, ref aVector2, aPlane, defaultZ);
        }

        public static Vector3[] ToVector3(this Vector2[] aPointsArray, PlaneAxis aPlane = PlaneAxis.XY, float defaultZ = 0f)
        {
            Vector3[] results = new Vector3[aPointsArray.Length];
            aPointsArray.ToVector3(ref results, aPlane, defaultZ);
            return results;
        }

        public static void ToVector3(this Vector2[] aPointsArray, ref Vector3[] outVecs, PlaneAxis aPlane = PlaneAxis.XY, float defaultZ = 0f)
        {
            for(int i=0; i<aPointsArray.Length; i++)
            {
                Vector3Ext.FromVector2(ref outVecs[i], ref aPointsArray[i], aPlane, defaultZ);
            }
        }
    
        public static Vector2Int ToInt(this Vector2 aVector2)
        {
            return new Vector2Int((int)aVector2.x, (int)aVector2.y);
        }

        public static Vector2 ToFloat(this Vector2Int aVector2)
        {
            return new Vector2(aVector2.x, aVector2.y);
        }

        public static Vector2 ComputeCenter(this Vector2[] aPointsArray)
        {
            float invcount = 1.0f / (float)aPointsArray.Length;
            Vector2 accumed = Vector2.zero;
            for (int i = 0; i < aPointsArray.Length; i++)
            {
                accumed += aPointsArray[i];
            }
            return accumed * invcount;
        }

        public static Vector2[] ToCenterOrigin(this Vector2[] aPointsArray)
        {
            Vector2[] results = new Vector2[aPointsArray.Length];
            Vector2 center = aPointsArray.ComputeCenter();
            for (int i = 0; i < aPointsArray.Length; i++)
            {
                results[i] = aPointsArray[i] - center;
            }
            return results;
        }

        public static Vector2[] ToBottomLeftOrigin(this Vector2[] aPointsArray)
        {
            Vector2[] results = new Vector2[aPointsArray.Length];
            Rect bounds = new Rect();
            RectExt.EncapsulatePoints(ref bounds, ref aPointsArray);
            Vector2 offset = new Vector2(-bounds.xMin, -bounds.yMin);

            for (int i = 0; i < aPointsArray.Length; i++)
            {
                results[i] = aPointsArray[i] + offset;
            }
            return results;
        }

        public static Vector2[] ToRotated(this Vector2[] aPointsArray, float aRotationDegrees)
        {
            Quaternion quaternion = Quaternion.AngleAxis(aRotationDegrees, Vector3.forward);
            Vector2[] rotatedPoints = new Vector2[aPointsArray.Length];
            for (int i = 0; i < aPointsArray.Length; i++)
            {
                rotatedPoints[i] = quaternion * aPointsArray[i];
            }
            return rotatedPoints;
        }

        public static void Rotate(this Vector2[] aPointsArray, float aRotationDegrees)
        {
            Quaternion quaternion = Quaternion.AngleAxis(aRotationDegrees, Vector3.forward);
            for (int i = 0; i < aPointsArray.Length; i++)
            {
                aPointsArray[i] = quaternion * aPointsArray[i];
            }
        }

        public static void RotateAround(this Vector2[] aPointsArray, float aRotationDegrees, Vector2 anOrigin)
        {
            Quaternion quaternion = Quaternion.AngleAxis(aRotationDegrees, Vector3.forward);
            Vector2 delta = Vector2.zero;

            for (int i = 0; i < aPointsArray.Length; i++)
            {
                delta.x = aPointsArray[i].x - anOrigin.x;
                delta.y = aPointsArray[i].y - anOrigin.y;
                delta = quaternion * delta;
                aPointsArray[i].x = anOrigin.x + delta.x;
                aPointsArray[i].y = anOrigin.y + delta.y;
            }
        }

        public static void Offset(this Vector2[] aPointsArray, Vector2 anOffset)
        {
            for (int i = 0; i < aPointsArray.Length; i++)
            {
                aPointsArray[i] = aPointsArray[i] + anOffset;
            }
        }

        public static float Angle(Vector2 pos1, Vector2 pos2)
        {
            Vector2 from = pos2 - pos1;
            Vector2 to = new Vector2(1, 0);
     
            float result = Vector2.Angle( from, to );
            Vector3 cross = Vector3.Cross( from, to );
     
            if (cross.z > 0) {
                result = 360f - result;
            }
     
            return result;
        }

        public static int[] GetColinearIndicies(ref Vector2[] aPointsArray)
        {
            List<int> results = new List<int>();
            if(aPointsArray.Length < 3) return results.ToArray();

            temp = (aPointsArray[1] - aPointsArray[0]);
            Vector2 current = Vector2.zero;

            for(int i=1; i<aPointsArray.Length-1; i++)
            {
                current.x = aPointsArray[i+1].x - aPointsArray[i].x;
                current.y = aPointsArray[i+1].y - aPointsArray[i].y;

                float dot = Vector2.Dot(current, temp);
                if(dot == 1.0f) results.Add(i);

                temp.Set(current.x, current.y);
            }
            return results.ToArray();
        }

        static bool SegmentsIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            Vector2 a = p2 - p1;
            Vector2 b = p3 - p4;
            Vector2 c = p1 - p3;
           
            float alphaNumerator = b.y*c.x - b.x*c.y;
            float alphaDenominator = a.y*b.x - a.x*b.y;
            float betaNumerator  = a.x*c.y - a.y*c.x;
            float betaDenominator  = a.y*b.x - a.x*b.y;
           
            bool doIntersect = true;
           
            if (alphaDenominator == 0 || betaDenominator == 0) {
                doIntersect = false;
            } else {
               
                if (alphaDenominator > 0) {
                    if (alphaNumerator < 0 || alphaNumerator > alphaDenominator) {
                        doIntersect = false;
                       
                    }
                } else if (alphaNumerator > 0 || alphaNumerator < alphaDenominator) {
                    doIntersect = false;
                }
               
                if (doIntersect && betaDenominator > 0) {
                    if (betaNumerator < 0 || betaNumerator > betaDenominator) {
                        doIntersect = false;
                    }
                } else if (betaNumerator > 0 || betaNumerator < betaDenominator) {
                    doIntersect = false;
                }
            }
         
            return doIntersect;
        }

        public static bool RayRayIntersection(Vector2 s1, Vector2 d1, Vector2 s2, Vector2 d2, ref Vector2 intersection)
        {
            return SegmentSegmentIntersection(s1, s1 + (d1.normalized * 999999.0f), s2, s2 + (d2.normalized * 999999.0f), ref intersection);
        }

        public static bool SegmentSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 intersection)
        {
         
            float Ax,Bx,Cx,Ay,By,Cy,d,e,f,num/*,offset*/;
            float x1lo,x1hi,y1lo,y1hi;
         
            Ax = p2.x-p1.x;
            Bx = p3.x-p4.x;
         
            // X bound box test/
            if(Ax<0) {
                x1lo=p2.x; x1hi=p1.x;
            } else {
                x1hi=p2.x; x1lo=p1.x;
            }

            if(Bx>0) {
                if(x1hi < p4.x || p3.x < x1lo) return false;
            } else {
                if(x1hi < p3.x || p4.x < x1lo) return false;
            }

            Ay = p2.y-p1.y;         
            By = p3.y-p4.y;

            // Y bound box test//         
            if(Ay<0) {
                y1lo=p2.y; y1hi=p1.y;
            } else {
                y1hi=p2.y; y1lo=p1.y;
            }

            if(By>0) {         
                if(y1hi < p4.y || p3.y < y1lo) return false;
            } else {
                if(y1hi < p3.y || p4.y < y1lo) return false;
            }

            Cx = p1.x-p3.x;         
            Cy = p1.y-p3.y;
            d = By*Cx - Bx*Cy;  // alpha numerator//
            f = Ay*Bx - Ax*By;  // both denominator//
         
            // alpha tests//
            if(f>0) {
                if(d<0 || d>f) return false;
            } else {
                if(d>0 || d<f) return false;
            }

            e = Ax*Cy - Ay*Cx;  // beta numerator//           
         
            // beta tests //
            if(f>0) {                          
              if(e<0 || e>f) return false;
            } else {
              if(e>0 || e<f) return false;
             }

            // check if they are parallel         
            if(f==0) return false;
            // compute intersection coordinates //
            num = d*Ax; // numerator //
        //    offset = same_sign(num,f) ? f*0.5f : -f*0.5f;   // round direction //
        //    intersection.x = p1.x + (num+offset) / f;
              intersection.x = p1.x + num / f;
            num = d*Ay;

        //    offset = same_sign(num,f) ? f*0.5f : -f*0.5f;         
        //    intersection.y = p1.y + (num+offset) / f;
            intersection.y = p1.y + num / f;

            return true;         
        }

        public static bool LineLineIntersection(Vector2 alinePoint1, Vector2 alineVec1, Vector2 alinePoint2, Vector2 alineVec2, out Vector2 intersection)
        {
            Vector3 linePoint1 = alinePoint1.ToVector3();
            Vector3 lineVec1 = alineVec1.ToVector3();
            Vector3 linePoint2 = alinePoint2.ToVector3();
            Vector3 lineVec2 = alineVec2.ToVector3();

            Vector3 lineVec3 = linePoint2 - linePoint1;
            Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
            Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);
     
            float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);
     
            //is coplanar, and not parrallel
            if(Mathf.Abs(planarFactor) < 0.0001f && crossVec1and2.sqrMagnitude > 0.0001f)
            {
                float s = Vector3.Dot(crossVec3and2, crossVec1and2) / crossVec1and2.sqrMagnitude;
                intersection = (linePoint1 + (lineVec1 * s)).ToVector2(PlaneAxis.XY);
                return true;
            }
            else
            {
                intersection = Vector2.zero;
                return false;
            }
        }

        public static float DistanceToSegment(Vector2 p1, Vector2 p2, Vector2 point)
        {
            Vector2 delta = (p2 - p1);
            Vector2 direction = delta.normalized;
            Vector2 pointonline = NearestPointOnLine(p1, direction, point);
            // check it's in the segment
            Vector2 onlinedelta = (pointonline - p1);
            Vector2 onlinedir = onlinedelta.normalized;
            float deltadot = Vector2.Dot(direction, onlinedir);
            if (deltadot < 0.0f) return (point - p1).magnitude;
            float onlinedist = onlinedelta.magnitude;
            if (onlinedist > delta.magnitude) return (point - p2).magnitude;
            return (point - pointonline).magnitude;
        }

        public static Vector2 NearestPointOnLine(Vector2 linePnt, Vector2 lineDir, Vector2 pnt)
        {
            lineDir.Normalize();//this needs to be a unit vector
            Vector2 v = pnt - linePnt;
            float d = Vector2.Dot(v, lineDir);
            return linePnt + lineDir * d;
        }
 
    }


    /// <summary>
    /// Vector3 ext.
    /// </summary>

    public static class Vector3Ext
    {
        public static Vector3 temp = Vector3.zero;


        public static void FromVector2(this Vector3 aVector3, ref Vector2 aVector2, PlaneAxis aPlane = PlaneAxis.XY, float defaultZ = 0f)
        {
            switch(aPlane)
            {
                case PlaneAxis.XY:
                {
                    aVector3.x = aVector2.x; aVector3.y = aVector2.y; aVector3.z = defaultZ;
                    break;
                }
                case PlaneAxis.XZ:
                {
                    aVector3.x = aVector2.x; aVector3.z = aVector2.y; aVector3.y = defaultZ;
                    break;
                }
                case PlaneAxis.ZY:
                {
                    aVector3.z = aVector2.x; aVector3.y = aVector2.y; aVector3.x = defaultZ;
                    break;
                }
                default: break;
            }
        }

        // todo, can above just call this, we need this one to call on elements in ref arrays
        public static void FromVector2(ref Vector3 aVector3, ref Vector2 aVector2, PlaneAxis aPlane = PlaneAxis.XY, float defaultZ = 0f)
        {
            switch(aPlane)
            {
                case PlaneAxis.XY:
                {
                    aVector3.x = aVector2.x; aVector3.y = aVector2.y; aVector3.z = defaultZ;
                    break;
                }
                case PlaneAxis.XZ:
                {
                    aVector3.x = aVector2.x; aVector3.z = aVector2.y; aVector3.y = defaultZ;
                    break;
                }
                case PlaneAxis.ZY:
                {
                    aVector3.z = aVector2.x; aVector3.y = aVector2.y; aVector3.x = defaultZ;
                    break;
                }
                default: break;
            }
        }

        public static Vector2 ToVector2(this Vector3 aVector3, PlaneAxis aPlane = PlaneAxis.XY)
        {
            aVector3.ToVector2(ref Vector2Ext.temp, aPlane);
            return Vector2Ext.temp;
        }

        public static void ToVector2(this Vector3 aVector3, ref Vector2 aVector2, PlaneAxis aPlane = PlaneAxis.XY)
        {
            Vector2Ext.FromVector3(ref aVector2, ref aVector3, aPlane);
        }

        public static Vector2[] ToVector2(this Vector3[] aPointsArray, PlaneAxis aPlane = PlaneAxis.XY)
        {
            Vector2[] results = new Vector2[aPointsArray.Length];
            aPointsArray.ToVector2(ref results, aPlane);
            return results;
        }

        public static void ToVector2(this Vector3[] aPointsArray, ref Vector2[] outVecs, PlaneAxis aPlane = PlaneAxis.XY)
        {
            for(int i=0; i<aPointsArray.Length; i++)
            {
                Vector2Ext.FromVector3(ref outVecs[i], ref aPointsArray[i], aPlane);
            }
        }

        public static Vector3 ComputeRangerCenter(this Vector3[] aVec3Array, int aStart, int aCount)
        {
            Vector3 center = new Vector3();
            for(int i=aStart; i<aStart+aCount; i++)
                center += aVec3Array[i];
            center *= 1.0f/(aVec3Array.Length-aStart);
            return center;
        }

        public static Vector3 ComputeCenter(this Vector3[] aPointsArray)
        {
            float invcount = 1.0f / (float)aPointsArray.Length;
            Vector3 accumed = Vector3.zero;
            for (int i = 0; i < aPointsArray.Length; i++)
            {
                accumed += aPointsArray[i];
            }
            return accumed * invcount;
        }

        public static Vector3[] ToCenterOrigin(this Vector3[] aPointsArray)
        {
            Vector3[] results = new Vector3[aPointsArray.Length];
            Vector3 center = aPointsArray.ComputeCenter();
            for (int i = 0; i < aPointsArray.Length; i++)
            {
                results[i] = aPointsArray[i] - center;
            }
            return results;
        }

        public static Vector3[] ToRotated(this Vector3[] aPointsArray, float aRotationDegrees, PlaneAxis aPlane = PlaneAxis.XY)
        {
            return aPointsArray.ToRotated(aRotationDegrees, Axis.NormalForPlaneAxis(aPlane));
        }

        public static Vector3[] ToRotated(this Vector3[] aPointsArray, float aRotationDegrees, Vector3 anAxis)
        {
            Quaternion quaternion = Quaternion.AngleAxis(aRotationDegrees, anAxis);
            Vector3[] rotatedPoints = new Vector3[aPointsArray.Length];
            for (int i = 0; i < aPointsArray.Length; i++)
            {
                rotatedPoints[i] = quaternion * aPointsArray[i];
            }
            return rotatedPoints;
        }

        public static void Rotate(this Vector3[] aPointsArray, float aRotationDegrees, PlaneAxis aPlane = PlaneAxis.XY)
        {
            aPointsArray.Rotate(aRotationDegrees, Axis.NormalForPlaneAxis(aPlane));
        }

        public static void Rotate(this Vector3[] aPointsArray, float aRotationDegrees, Vector3 anAxis)
        {
            Quaternion quaternion = Quaternion.AngleAxis(aRotationDegrees, anAxis);
            for (int i = 0; i < aPointsArray.Length; i++)
            {
                aPointsArray[i] = quaternion * aPointsArray[i];
            }
        }

        public static void WorldToScreenSpace(ref Vector3[] refCorners, Camera aCamera, int aStart, int aCount)
        {
            for(int i=aStart; i<aStart+aCount; i++)
                refCorners[i] = aCamera.WorldToScreenPoint(refCorners[i]);
        }
            
        public static void WorldToScreenSpace(ref Vector3[] refCorners, Vector3[] worldCorners, Camera aCamera, int aStart, int aCount)
        {
            for(int i=aStart; i<aStart+aCount; i++)
                refCorners[i] = aCamera.WorldToScreenPoint(worldCorners[i-aStart]);
        }
            
       public static void ScreenToWorldSpace(ref Vector3[] refCorners, Camera aCamera, int aStart, int aCount)
        {
            for(int i=aStart; i<aStart+aCount; i++)
                refCorners[i] = aCamera.ScreenToWorldPoint(refCorners[i]);
        }
            
        public static void ScreenToWorldSpace(ref Vector3[] refCorners, Vector3[] screenCorners, Camera aCamera, int aStart, int aCount)
        {
            for(int i=aStart; i<aStart+aCount; i++)
                refCorners[i] = aCamera.ScreenToWorldPoint(screenCorners[i-aStart]);
        }

        public static Vector3 ClosestPointOnSegment(Vector3 p1, Vector3 p2, Vector3 point)
        {
            Vector3 direction = p2 - p1;
            Vector3 p1topoint = point - p1;
            return Vector3.Project(p1topoint, direction.normalized);
        }
    }

} // end Hbx Ext namespace
