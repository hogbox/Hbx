//----------------------------------------------
//            Hbx: Generic
// Copyright © 2017-2018 Hogbox Studios
// GenericSingletonScriptableObject.cs
//----------------------------------------------

using UnityEngine;
using Hbx.Ext;

namespace Hbx.Generic
{
    public class SingletonScriptableObject : ScriptableObject
    {
        public virtual void Init() { }
    }

    public abstract class GenericSingletonScriptableObject<T> : SingletonScriptableObject where T : SingletonScriptableObject, new()
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
} // end Hbx Generic namespace
