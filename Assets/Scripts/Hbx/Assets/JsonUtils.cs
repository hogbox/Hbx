//----------------------------------------------
//            Hbx: Assets
// Copyright © 2017-2018 Hogbox Studios
// JsonUtils.cs
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hbx.Assets
{
    /// <summary>
    /// Tools for reading and writing object to json string format
    /// </summary>

    public static class JsonUtils
    {
        public static string SerializeObject(object anObject, bool prettyPrint = true)
        {
            ISerializationCallbackReceiver serializationCallbackReceiver = anObject as ISerializationCallbackReceiver;
            if (serializationCallbackReceiver != null) serializationCallbackReceiver.OnBeforeSerialize();
            return JsonUtility.ToJson(anObject, prettyPrint);
        }

        public static T DeserializeObject<T>(string aJsonString, T anExistingObject) where T : new()
        {
            ISerializationCallbackReceiver serializationCallbackReceiver = anExistingObject as ISerializationCallbackReceiver;
            if (anExistingObject == null)
            {
                T obj = JsonUtility.FromJson<T>(aJsonString);
                serializationCallbackReceiver = obj as ISerializationCallbackReceiver;
                if (serializationCallbackReceiver != null) serializationCallbackReceiver.OnAfterDeserialize();
                return obj;
            }
            JsonUtility.FromJsonOverwrite(aJsonString, anExistingObject);
            if (serializationCallbackReceiver != null) serializationCallbackReceiver.OnAfterDeserialize();
            return anExistingObject;
        }
        public static string SerializeScriptableObject(ScriptableObject anObject, bool prettyPrint = true)
        {
            return JsonUtility.ToJson(anObject, prettyPrint);
        }

        public static T DeserializeScriptableObject<T>(string aJsonString, ScriptableObject anExistingObject = null) where T : ScriptableObject
        {
            T obj = anExistingObject != null ? anExistingObject as T : ScriptableObject.CreateInstance<T>();
            JsonUtility.FromJsonOverwrite(aJsonString, obj);
            return obj;
        }
    }
}
