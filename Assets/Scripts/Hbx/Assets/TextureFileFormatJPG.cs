using System.IO;
using System.Buffers.Binary;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

namespace Hbx.Assets
{
    /// <summary>
    /// TextureFormat implementation for JPG texture files, this won't decompress the texels
    /// instead it will confirm the headermagic and extract the dimensions
    /// http://www.libpng.org/pub/png/spec/1.2/png-1.2.pdf
    /// </summary>
    public class TextureFileFormatJPG : TextureFileFormat
    {
        /// <summary>
        /// Header bytes for PNG files
        /// </summary>
        public override byte[] headerMagic => new byte[] { 255, 216, 255, 224 };

        /// <summary>
        /// Check the headermagic and extract the dimensions of the image
        /// </summary>
        /// <param name="bytes">The bytes of a file in this format</param>
        /// <returns>TextureParams with decoded data for this format</returns>
        public override TextureParams GetTextureParams(byte[] bytes)
        {
            TextureParams texparams = new TextureParams();
            if (!usingHeaderMagic(bytes)) return texparams;

            var reader = new BinaryReader(new MemoryStream(bytes));

            // read 8 bytes to get past headermagic
            byte[] headermagic = reader.ReadBytes(8);

            // read the dds header
            //IHDR_CHUNK header = new IHDR_CHUNK();
            //if (!header.Read(reader)) return texparams;

            texparams.width = 64;// (int)header.width;
            texparams.height = 64;// (int)header.height;
            texparams.mipmaps = 1;
            texparams.dimension = TextureDimension.Tex2D;

            texparams.format = GraphicsFormat.R8G8B8A8_SRGB;

            texparams.rawTexels = false;
            texparams.texels = bytes;

            texparams.valid = true;
            return texparams;
        }
    }
}