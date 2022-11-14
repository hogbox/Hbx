//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// TextResult.cs
//----------------------------------------------

namespace Hbx.Assets
{
    public class TextResult : ILoaderResult<string>
    {
        public string _data = null;

        public TextResult()
        {
            _data = null;
        }

        public TextResult(string data)
        {
            _data = data;
        }

        public bool valid => !string.IsNullOrEmpty(_data);

        public string data => _data;

        object ILoaderResult.data => throw new System.NotImplementedException();
    }
}

