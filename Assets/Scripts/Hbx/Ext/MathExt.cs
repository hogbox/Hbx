//----------------------------------------------
//            Hbx: Ext
// Copyright © 2017-2018 Hogbox Studios
// Math.cs
//----------------------------------------------

using UnityEngine;

namespace Hbx.Ext
{

    public static class MathExt
    {
        public static bool Approximately(ref Vector2 aVec, ref Vector2 bVec)
        {
            return Mathf.Approximately(aVec.x, bVec.x) && Mathf.Approximately(aVec.y, bVec.y);
        }
    }

}
