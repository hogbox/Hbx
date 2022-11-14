//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// GenericResult.cs
//----------------------------------------------

namespace Hbx.Assets
{
    /// <summary>
    /// GenericResult allows for easy creation of new ILoaderResult types
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericResult<T> : ILoaderResult<T> where T : new()
    {
        T _data = default;

        public GenericResult()
        {
            _data = default;
        }

        public GenericResult(T data)
        {
            _data = data;
        }

        public T data => _data;

        public bool valid => _data != null;

        object ILoaderResult.data => throw new System.NotImplementedException();
    }
}
