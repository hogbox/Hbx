//----------------------------------------------
//            Hbx: Ext
// Copyright © 2017-2018 Hogbox Studios
// Matrix.cs
//----------------------------------------------

using UnityEngine;

namespace Hbx.Ext
{

    public static class MatrixExt
    {
        public static Matrix4x4 translationMatrix = Matrix4x4.identity;
        public static Vector3 translation = Vector3.zero;
        public static Matrix4x4 scalingMatrix = Matrix4x4.identity;
        public static Vector3 scale = Vector3.one;
        public static Matrix4x4 tempMatrix = new Matrix4x4();
    
        public static void Translate(ref Matrix4x4 m, Vector3 position)
        {
            translationMatrix[12] = position.x;
            translationMatrix[13] = position.y;
            translationMatrix[14] = position.z;
            m *= translationMatrix;
        }
        public static void Scale(ref Matrix4x4 m, Vector3 scale)
        {
            scalingMatrix[0] = scale.x;
            scalingMatrix[5] = scale.y;
            scalingMatrix[10] = scale.z;
            m *= scalingMatrix;
        }
        public static void CropMatrix(ref Matrix4x4 m, Rect r, Rect cameraPixelRect)
        {
            CropMatrix(ref m, r.x, r.y, r.width, r.height, cameraPixelRect.x, cameraPixelRect.y, cameraPixelRect.width, cameraPixelRect.height);
        }

        public static void CropMatrix(ref Matrix4x4 m, float rx, float ry, float rwidth, float rheight, float cameraPixelRectX, float cameraPixelRectY, float cameraPixelRectWidth, float cameraPixelRectHeight)
        {
            rx += rwidth * 0.5f;
            ry += rheight * 0.5f;
    
            if (rwidth <= 0 || rheight <= 0)
             return;// m;
                
             m[0] = cameraPixelRectWidth / rwidth;//scale.x
             m[5] = cameraPixelRectHeight / rheight;//scale.y
             m[10] = 1f;//scale.z
                
             m[12] = ((cameraPixelRectWidth - 2f * (rx - cameraPixelRectX)) / rwidth);//*m[0];//pos.x
             m[13] = ((cameraPixelRectHeight - 2f * (ry - cameraPixelRectY)) / rheight);//*m[5];//pos.y
             m[14] = 0f;//pos.z
    
             m[1] = 0f;
             m[2] = 0f;
             m[3] = 0f;
             m[4] = 0f;
             m[6] = 0f;
             m[7] = 0f;
             m[8] = 0f;
             m[9] = 0f;
             m[11] = 0f;
             m[15] = 1f;
        }

        /// <summary>
        /// Extract translation from transform matrix.
        /// </summary>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>
        /// <returns>
        /// Translation offset.
        /// </returns>

        public static Vector3 GetTranslation(this Matrix4x4 matrix) {
            translation.x = matrix.m03;
            translation.y = matrix.m13;
            translation.z = matrix.m23;
            return translation;
        }
         
        /// <summary>
        /// Extract rotation quaternion from transform matrix.
        /// </summary>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>
        /// <returns>
        /// Quaternion representation of rotation transform.
        /// </returns>

        public static Quaternion GetRotation(this Matrix4x4 matrix) {
            /*
            Vector3 forward;
            forward.x = matrix.m02;
            forward.y = matrix.m12;
            forward.z = matrix.m22;
         
            Vector3 upwards;
            upwards.x = matrix.m01;
            upwards.y = matrix.m11;
            upwards.z = matrix.m21;
         
            Quaternion.LookRotation(forward, upwards);
            */

            return matrix.ValidTRS() ? matrix.rotation : Quaternion.identity; 
        }
         
        /// <summary>
        /// Extract scale from transform matrix.
        /// </summary>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>
        /// <returns>
        /// Scale vector.
        /// </returns>

        public static Vector3 GetScale(this Matrix4x4 matrix) {
            Vector3 scale;
            scale.x = new Vector4(matrix.m00, matrix.m10, matrix.m20, matrix.m30).magnitude;
            scale.y = new Vector4(matrix.m01, matrix.m11, matrix.m21, matrix.m31).magnitude;
            scale.z = new Vector4(matrix.m02, matrix.m12, matrix.m22, matrix.m32).magnitude;
            return scale;
        }
         
        /// <summary>
        /// Extract position, rotation and scale from TRS matrix.
        /// </summary>
        /// <param name="matrix">Transform matrix. This parameter is passed by reference
        /// to improve performance; no changes will be made to it.</param>
        /// <param name="localPosition">Output position.</param>
        /// <param name="localRotation">Output rotation.</param>
        /// <param name="localScale">Output scale.</param>

        public static void DecomposeMatrix(this Matrix4x4 matrix, out Vector3 localPosition, out Quaternion localRotation, out Vector3 localScale) {
            localPosition = matrix.GetTranslation();
            localRotation = matrix.GetRotation();
            localScale = matrix.GetScale();
        }
         
         
        // EXTRAS!
         
        /// <summary>
        /// Identity quaternion.
        /// </summary>
        /// <remarks>
        /// <para>It is faster to access this variation than <c>Quaternion.identity</c>.</para>
        /// </remarks>

        public static readonly Quaternion IdentityQuaternion = Quaternion.identity;
        /// <summary>
        /// Identity matrix.
        /// </summary>
        /// <remarks>
        /// <para>It is faster to access this variation than <c>Matrix4x4.identity</c>.</para>
        /// </remarks>

        public static readonly Matrix4x4 IdentityMatrix = Matrix4x4.identity;
         
    }

} // end Hbx Ext namespace