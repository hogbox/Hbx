//----------------------------------------------
//            Hbx: Assets
// Copyright © 2017-2018 Hogbox Studios
// GenericSingletonJsonScriptableObject.cs
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Hbx.Assets
{
    public class SingletonJsonScriptableObject<T> : JsonScriptableObject<T> where T : new()
    {
        public virtual void Init()
        {
            Data = new T();
        }
    }

    public abstract class GenericSingletonJsonScriptableObject<T, D> : SingletonJsonScriptableObject<D> where T : SingletonJsonScriptableObject<D>, new()
                                                                                                        where D : new()
    {
        static T _instance;
        public static T Get
        {
            get
            {
                if (_instance == null)
                {
                    _instance = ScriptableObject.CreateInstance<T>();
                    if (_instance != null)
                    {
                        _instance.Init();
                    }
                }
                return _instance;
            }
        }
    }
} // end Hbx Assets namespace
