//----------------------------------------------
//            Hbx:
// Copyright © 2017-2018 Hogbox Studios
// Axis.cs
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hbx
{

    public enum Axis3D
    {
        X = 0,
        Y,
        Z,
        X_Neg,
        Y_Neg,
        Z_Neg
    }

    public enum Dir3D
    {
        Right = 0,
        Up,
        Forward,
        Left,
        Down,
        Backward
    }

    public enum PlaneAxis
    {
        XY = 0,
        XZ,
        ZY
    }

    public static class Axis
    {
        public static Axis3D AxisForDirection(Dir3D aDir)
        {
            return (Axis3D)((int)aDir);
        }

        public static Dir3D DirectionForAxis(Axis3D anAxis)
        {
            return (Dir3D)((int)anAxis);
        }

        public static Vector3 AxisToVector3(Axis3D anAxis)
        {
            switch(anAxis)
            {
                case Axis3D.X: return x;
                case Axis3D.Y: return y;
                case Axis3D.Z: return z;
                case Axis3D.X_Neg: return xneg;
                case Axis3D.Y_Neg: return yneg;
                case Axis3D.Z_Neg: return zneg;
                default: break;
            }
            return x;
        }

        public static Vector3 DirectionToVector3(Dir3D aDir)
        {
            switch(aDir)
            {
                case Dir3D.Left: return left;
                case Dir3D.Up: return up;
                case Dir3D.Forward: return forward;
                case Dir3D.Right: return right;
                case Dir3D.Down: return down;
                case Dir3D.Backward: return backward;
                default: break;
            }
            return x;
        }

        public static Vector3 NormalForPlaneAxis(PlaneAxis aPlane)
        {
            switch (aPlane)
            {
                case PlaneAxis.XY: return Vector3.forward;
                case PlaneAxis.XZ: return Vector3.up;
                case PlaneAxis.ZY: return Vector3.right;
                default: break;
            }
            return Vector3.forward;
        }

        public static Vector3 ConvertVector(Vector2 aVec2, PlaneAxis aPlane, float defaultZ = 0f)
        {
            switch(aPlane)
            {
                case PlaneAxis.XY: return new Vector3(aVec2.x, aVec2.y, defaultZ);
                case PlaneAxis.XZ: return new Vector3(aVec2.x, defaultZ, aVec2.y);
                case PlaneAxis.ZY: return new Vector3(defaultZ, aVec2.y, aVec2.x);
                default: break;
            }
            return new Vector3(aVec2.x, aVec2.y, defaultZ);;
        }

        public static readonly Vector3 left = Vector3.left;
        public static readonly Vector3 xneg = left;
        public static readonly Vector3 right = Vector3.right;
        public static readonly Vector3 x = right;
        public static readonly Vector3 up = Vector3.up;
        public static readonly Vector3 y = up;
        public static readonly Vector3 down = Vector3.down;
        public static readonly Vector3 yneg = down;
        public static readonly Vector3 forward = Vector3.forward;
        public static readonly Vector3 z = forward;
        public static readonly Vector3 backward = Vector3.back;
        public static readonly Vector3 zneg = backward;
    }
}
