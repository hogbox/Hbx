//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// BytesResult.cs
//----------------------------------------------

namespace Hbx.Assets
{
    public class BytesResult : ILoaderResult<byte[]>
    {
        public byte[] _data = null;

        public BytesResult()
        {
            _data = null;
        }

        public BytesResult(byte[] data)
        {
            _data = data;
        }

        public bool valid  => _data != null;

        public byte[] data => _data;

        object ILoaderResult.data => throw new System.NotImplementedException();
    }
}
