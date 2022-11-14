//----------------------------------------------
//            Hbx: Ext
// Copyright © 2017-2018 Hogbox Studios
// PerspectiveExt.cs
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hbx.Ext
{
    public static class PerspectiveExt
    {
        public static Matrix4x4 CreateFromPerpectivePoints(ref Vector2[] sourcePoints, ref Vector2[] distortPoints)
        {
            Matrix4x4 aMatrix = new Matrix4x4();

            List<float[]> a = new List<float[]>();
            List<float> b = new List<float>();
            for (int i = 0; i < sourcePoints.Length; ++i) {
                Vector2 s = sourcePoints[i], t = distortPoints[i];
                a.Add(new float[]{s.x, s.y, 1f, 0f, 0f, 0f, -s.x * t.x, -s.y * t.x}); b.Add(t.x);
                a.Add(new float[]{0f, 0f, 0f, s.x, s.y, 1f, -s.x * t.y, -s.y * t.y}); b.Add(t.y);
            }
            
            List<float> X = solve(a, b, true);;
            aMatrix.SetColumn(0, new Vector4(X[0], X[3], 0f, X[6]));
            aMatrix.SetColumn(1, new Vector4(X[1], X[4], 0,  X[7]));
            aMatrix.SetColumn(2, new Vector4(0f,   0f,   1f, 0f));
            aMatrix.SetColumn(3, new Vector4(X[2], X[5], 0f, 1f));

            return aMatrix;
        }
        
        // Given a 4x4 perspective transformation matrix, and a 2D point (a 2x1 vector),
        // applies the transformation matrix by converting the point to homogeneous
        // coordinates at z=0, post-multiplying, and then applying a perspective divide.

        private static Vector2 project(Matrix4x4 matrix, Vector2 point)
        {
            Vector4 p4 = matrix.MultiplyPoint( new Vector4(point.x, point.y, 0f, 1f) );
            return new Vector2(p4.x / p4.w, p4.y / p4.w);
        }
    
                
        private struct LUP
        {
            public List<float[]> LU;
            public List<int> P;
        }
    
        private static LUP LU(List<float[]> Aa, bool fast)
        {
            fast = fast || false;
            
            int i, j, k, Pk;
            float[] Ak, Ai;
            float absAjk, Akk,max;
            int n = Aa.Count;
            int n1 = n - 1;
            List<int> P = new List<int>(n);
            for(int e=0; e<n; e++)P.Add(0);
            
            List<float[]> A = new List<float[]>(Aa);
            
            for (k = 0; k < n; ++k)
            {
                Pk = k;
                Ak = A[k];
                max = Mathf.Abs(Ak[k]);
                for (j = k + 1; j < n; ++j) 
                {
                    absAjk = Mathf.Abs(A[j][k]);
                    if (max < absAjk)
                    {
                        max = absAjk;
                        Pk = j;
                    }
                }
    
                P[k] = Pk;
                
                if (Pk != k)
                {
                    A[k] = A[Pk];
                    A[Pk] = Ak;
                    Ak = A[k];
                }
                
                Akk = Ak[k];
                
                for (i = k + 1; i < n; ++i)
                {
                    A[i][k] /= Akk;
                }
                
                for (i = k + 1; i < n; ++i)
                {
                    Ai = A[i];
                    for (j = k + 1; j < n1; ++j)
                    {
                        Ai[j] -= Ai[k] * Ak[j];
                        ++j;
                        Ai[j] -= Ai[k] * Ak[j];
                    }
                    if (j==n1) Ai[j] -= Ai[k] * Ak[j];
                }
            }
            
            LUP lup;
            lup.LU = A;
            lup.P = P;
            return lup;
        }
        
        private static List<float> LUsolve(LUP lup, List<float> b)
        {
            int i, j;
            List<float[]> LU = lup.LU;
            int n = LU.Count;
    
            List<float> x = new List<float>(b);
            List<int> P = lup.P;
            int Pi;
            float[] LUi;
            float tmp;
            
            for (i = n - 1; i != -1; --i) x[i] = b[i];
            for (i = 0; i < n; ++i)
            {
                Pi = P[i];
                if (P[i] != i)
                {
                    tmp = x[i]; x[i] = x[Pi]; x[Pi] = tmp;
                }
                LUi = LU[i];
                for (j = 0; j < i; ++j)
                {
                    x[i] -= x[j] * LUi[j];
                }
            }
            
            for (i = n - 1; i >= 0; --i)
            {
                LUi = LU[i];
                for (j = i + 1; j < n; ++j)
                {
                    x[i] -= x[j] * LUi[j];
                }
                x[i] /= LUi[i];
            }
            
            return x;
        }
            
        private static List<float> solve(List<float[]> A, List<float> b, bool fast)
        {
            return LUsolve(LU(A, fast), b);
        }
    }
} // end Hbx Ext namespace
