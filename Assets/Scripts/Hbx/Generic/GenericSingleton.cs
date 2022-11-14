//----------------------------------------------
//            Hbx: Generic
// Copyright © 2017-2018 Hogbox Studios
// GenericSingleton.cs
//----------------------------------------------

namespace Hbx.Generic
{
    public class Singleton
    {
        public virtual void Init() { }
    }

    public abstract class GenericSingleton<T> : Singleton where T : Singleton, new()
    {
        static T _instance;
        public static T Get
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                    _instance.Init();
                }
                return _instance;
            }
        }
    }
} // end Hbx Generic namespace
