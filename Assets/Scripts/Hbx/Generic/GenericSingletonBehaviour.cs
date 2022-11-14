//----------------------------------------------
//            Hbx: Generic
// Copyright © 2017-2018 Hogbox Studios
// GenericSingletonBehviour.cs
//----------------------------------------------

using UnityEngine;
using Hbx.Ext;

namespace Hbx.Generic
{
    public class SingletonBehaviour : MonoBehaviour
    {
        public virtual void Init() { }
    }

    public abstract class GenericSingletonBehaviour<T> : SingletonBehaviour where T : SingletonBehaviour, new()
    {
        static T _instance;
        public static T Get
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindObjectOfType<T>();
                    if(_instance == null)
                    {
                        System.Type type = typeof(T);
                        GameObject go = new GameObject(type.Name);
                        // Ensure the namespace node exists and attach this to it
                        string namespacepath = type.Namespace != null ? type.Namespace.Replace('.', '/') : "Hbx/Generic";
                        GameObject parent = GameObjectExt.GetOrCreateGameObject(namespacepath);
                        go.transform.SetParent(parent.transform, false);
                        _instance = go.AddComponent<T>();
                        _instance.Init();
                    }
                }
                return _instance;
            }
        }
    }
} // end Hbx Generic namespace
