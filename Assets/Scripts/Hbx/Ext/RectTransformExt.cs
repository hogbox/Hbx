//----------------------------------------------
//            Hbx: Ext
// Copyright © 2017-2018 Hogbox Studios
// RectTransformExt.cs
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hbx.Ext
{
    public static class RectTransformExt
    {
        public static void StretchToParentSize(this RectTransform aRect, Vector2 aPadding)
        {
            aRect.anchorMin = new Vector2(0f, 0f);
            aRect.anchorMax = new Vector2(1f, 1f);
            aRect.pivot = new Vector2(0.5f, 0.5f);
            aRect.sizeDelta = aPadding;
           // aRect.anchoredPosition = aRect.parent.position;
        }
    }
}
