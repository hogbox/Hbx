//----------------------------------------------
//            Hbx: Assets
// Copyright © 2017-2018 Hogbox Studios
// JsonScriptableObject.cs
//----------------------------------------------

using System;
using System.IO;
using UnityEngine;

namespace Hbx.Assets
{
    /// <summary>
    /// Json scriptable object, a type of ScriptableObject that holds a single generic Data object that can be serialized to json.
    /// </summary>

    public abstract class JsonScriptableObject<T> : ScriptableObject, ISerializationCallbackReceiver where T : new()
    {
        [SerializeField]
        T _data;
        public T Data { get { return _data; } set { _data = value; } }

        /// <summary>
        /// Convert the data object to a json string
        /// </summary>
        /// <returns>The json.</returns>
        /// <param name="prettyPrint">If set to <c>true</c> pretty print.</param>

        public string ToJson(bool prettyPrint = true)
        {
            OnBeforeSerialize();
            return JsonUtils.SerializeObject(Data, prettyPrint);
        }

        /// <summary>
        /// Apply the json to the current Data instance
        /// </summary>
        /// <param name="aJsonString">A json string.</param>

        public void ApplyJson(string aJsonString)
        {
            JsonUtils.DeserializeObject<T>(aJsonString, Data);
            OnAfterDeserialize();
        }

        /// <summary>
        /// Load the json into a new Data instance
        /// </summary>
        /// <param name="aJsonString">A json string.</param>

        public void FromJson(string aJsonString)
        {
            Data = JsonUtils.DeserializeObject<T>(aJsonString, default(T));
            OnAfterDeserialize();
        }

        /// <summary>
        /// Deserialise the Data object either from a passed json string or by loading a json string from a provided file path.
        /// If it's not a json string (isJson=false) and aPathOrJson is empty a default file path is created in the persistant data folder.
        /// </summary>
        /// <param name="aPathOrJson">A path or json.</param>
        /// <param name="isJson">If set to <c>true</c> is json.</param>
        /// <param name="onCompletion">On completion.</param>

        public void SyncFrom(string aPathOrJson, bool isJson, Action<bool> onCompletion = null)
        {
            // if path is empty create a default path in persistant data folder
            if (!isJson && string.IsNullOrEmpty(aPathOrJson))
            {
                aPathOrJson = GetDefaultPath();
            }
            if (!isJson)
            {
                string diskpath = Paths.ResolvePath(aPathOrJson, true);

                if(!File.Exists(diskpath))
                {
                    Debug.LogWarning("JsonScriptableObject: Failed to Sync json from '" + diskpath + "', the path does not exist.");
                    if (onCompletion != null) onCompletion(false);
                }

                IOManager.Get.ReadDiskText(diskpath, (bool success, string readstr) =>
                {
                    if (success) ApplyJson(readstr);
                    if (onCompletion != null) onCompletion(success);
                });
                return;
            }
            else
            {
                ApplyJson(aPathOrJson);
                if (onCompletion != null) onCompletion(true);
            }
        }

        /// <summary>
        /// Serialise the Data object, either by returning a json string or writing the json string to the provided file path.
        /// If we want to save to the path (saveToPath=true) and aPathOrJson is empty a default file path is created in the persistant data folder.
        /// </summary>
        /// <param name="saveToPath">If set to <c>true</c> save to path.</param>
        /// <param name="aPath">A path if saving.</param>
        /// <param name="onCompletion">On completion.</param>

        public void SyncTo(bool saveToPath, string aPath, Action<bool, string> onCompletion = null)
        {
            // if path is empty create a default path in persistant data folder
            if (saveToPath && string.IsNullOrEmpty(aPath))
            {
                aPath = GetDefaultPath();
            }

            // serialse
            string jsonstr = ToJson();

            if (saveToPath)
            {
                string diskpath = Paths.ResolvePath(aPath, true);
                IOManager.Get.WriteDiskText(diskpath, jsonstr, (bool success) =>
                {
                    if (onCompletion != null) onCompletion(success, jsonstr);
                });
                return;
            }

            if (onCompletion != null) onCompletion(true, jsonstr);
        }

        string GetDefaultPath()
        {
            return Path.Combine(Paths.persistentDataPath, typeof(T).Name + ".json");
        }

        /// <summary>
        /// ISerializationCallbackReceiver function
        /// </summary>

        public void OnBeforeSerialize()
        {
        }

        /// <summary>
        /// ISerializationCallbackReceiver function
        /// </summary>
        /// 
        public void OnAfterDeserialize()
        {
        }
    }
} // end Hbx Data namespace
