//----------------------------------------------
//            Hbx: Generic
// Copyright © 2017-2018 Hogbox Studios
// GenericPool.cs
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hbx.Generic
{
    public abstract class GenericPool<P, T> : GenericSingletonBehaviour<P> where P : SingletonBehaviour, new() where T : Object
    {
        /*static P _instance;
        public static P Get
        {
            get
            {
                if (_instance == null) _instance = new P();
                return _instance;
            }
        }*/

        Queue<T> _pooledObjects = new Queue<T>();

        public T GetOrCreateObject()
        {
            if(_pooledObjects.Count > 0)
            {
                T inst = _pooledObjects.Dequeue();
                EnableObject(inst);
                return inst;
            }
            return CreateObject();
        }

        protected virtual T CreateObject()
        {
            return null;
        }

        public void PoolObject(T anObject)
        {
            DisableObject(anObject);
            _pooledObjects.Enqueue(anObject);
        }

        protected virtual void EnableObject(T anObject)
        {
            
        }

        protected virtual void DisableObject(T anObject)
        {
            
        }
    }

    /// <summary>
    /// Basic gameobject pool
    /// </summary>

    public class GameObjectPool : GenericPool<GameObjectPool, GameObject>
    {
        protected override GameObject CreateObject()
        {
            GameObject go = new GameObject();
            return go;
        }

        protected override void EnableObject(GameObject anObject)
        {
            anObject.SetActive(true);
        }

        protected override void DisableObject(GameObject anObject)
        {
            anObject.SetActive(false);
        }
    }

} // end Hbx Generic namespace
