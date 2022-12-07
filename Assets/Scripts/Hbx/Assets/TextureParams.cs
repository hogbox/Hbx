using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

namespace Hbx.Assets
{
    public class TextureParams
    {
        public bool valid = false;
        public TextureDimension dimension = TextureDimension.None;
        public int width = 0;
        public int height = 0;
        public int depth = 0;
        public int mipmaps = 1;
        public GraphicsFormat format = GraphicsFormat.R8G8B8A8_UNorm;
        //public int texelStartOffset = 0;
        public byte[] texels = null;
        public bool rawTexels = false;
    }
}
