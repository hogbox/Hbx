using System.IO;
using System.Buffers.Binary;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

namespace Hbx.Assets
{
    public struct IHDR_CHUNK
    {
        public uint size;
        public string name;
        public uint width;
        public uint height;
        public byte depth;
        public byte colorType;
        public byte compressionMethod;
        public byte filterMethod;
        public byte interlaceMethod;

        public bool Read(BinaryReader reader)
        {
            size = BinaryPrimitives.ReadUInt32BigEndian(reader.ReadBytes(4));
            name = new string(reader.ReadChars(4));
            if (name != "IHDR") return false;
            width = BinaryPrimitives.ReadUInt32BigEndian(reader.ReadBytes(4));
            height = BinaryPrimitives.ReadUInt32BigEndian(reader.ReadBytes(4));
            depth = reader.ReadByte();
            colorType = reader.ReadByte();
            compressionMethod = reader.ReadByte();
            filterMethod = reader.ReadByte();
            interlaceMethod = reader.ReadByte();
            return true;
        }
    }

    /// <summary>
    /// TextureFormat implementation for PNG texture files, this won't decompress the texels
    /// instead it will confirm the headermagic and extract the dimensions
    /// http://www.libpng.org/pub/png/spec/1.2/png-1.2.pdf
    /// </summary>
    public class TextureFileFormatPNG : TextureFileFormat
    {
        /// <summary>
        /// Header bytes for PNG files
        /// </summary>
        public override byte[] headerMagic => new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 };

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
            IHDR_CHUNK header = new IHDR_CHUNK();
            if (!header.Read(reader)) return texparams;

            texparams.width = (int)header.width;
            texparams.height = (int)header.height;
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