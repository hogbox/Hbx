//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// TextureFileFormatDDS.cs
//----------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

namespace Hbx.Assets
{
    /// <summary>
    /// Enum matching the PVR Header pixelFormat types
    /// </summary>
    public enum PVR_FORMAT
    {
        None = 9999,
        PVRTC2bppRGB = 0,
        PVRTC2bppRGBA = 1,
        PVRTC4bppRGB = 2,
        PVRTC4bppRGBA = 3,
        PVRTCII2bpp = 4,
        PVRTCII4bpp = 5,
        ETC1 = 6,
        DXT1 = 7,
        DXT2 = 8,
        DXT3 = 9,
        DXT4 = 10,
        DXT5 = 11,
        BC1 = 7,
        BC2 = 9,
        BC3 =11,
        BC4 = 12,
        BC5 = 13,
        BC6 = 14,
        BC7 = 15,
        UYVY = 16,
        YUY2 = 17,
        BW1bpp = 18,
        R9G9B9E5SharedExponent = 19,
        RGBG8888 = 20,
        GRGB8888 = 21,
        ETC2RGB = 22,
        ETC2RGBA = 23,
        ETC2RGBA1 = 24,
        EACR11 = 25,
        EACRG11 = 26,
        ASTC_4x4 = 27,
        ASTC_5x4 = 28,
        ASTC_5x5 = 29,

        ASTC_6x5 = 30,
        ASTC_6x6 = 31,
        ASTC_8x5 = 32,
        ASTC_8x6 = 33,
        ASTC_8x8 = 34,
        ASTC_10x5 = 35,
        ASTC_10x6 = 36,
        ASTC_10x8 = 37,
        ASTC_10x10 = 38,
        ASTC_12x10 = 39,
        ASTC_12x12 = 40,
        ASTC_3x3x3 = 41,
        ASTC_4x3x3 = 42,
        ASTC_4x4x3 = 43,
        ASTC_4x4x4 = 44,
        ASTC_5x4x4 = 45,
        ASTC_5x5x4 = 46,
        ASTC_5x5x5 = 47,
        ASTC_6x5x5 = 48,
        ASTC_6x6x5 = 49,
        ASTC_6x6x6 = 50
    }

    /// <summary>
    /// Enum matching the PVR Header channel type
    /// </summary>
    public enum PVR_CHANNEL_TYPE
    {
        UnsignedByteNormalised = 0,
        SignedByteNormalised = 1,
        UnsignedByte = 2,
        SignedByte = 3,
        UnsignedShortNormalised = 4,
        SignedShortNormalised = 5,
        UnsignedShort = 6,
        SignedShort = 7,
        UnsignedIntegerNormalised = 8,
        SignedIntegerNormalised = 9,
        UnsignedInteger = 10,
        SignedInteger = 11,
        Float = 12,
    }

    /// <summary>
    /// PVR header struct, used to read and store the values contained in the header of a PVR file
    /// </summary>
    public struct PVR_HEADER
    {
        public uint version;
        public uint flags;
        //public ulong pixelFormat; // 64 bit
        public PVR_FORMAT pixelFormat;
        public char[] componentOrder;
        public ushort[] componentBits;
        public uint colourSpace;
        public PVR_CHANNEL_TYPE channelType;
        public uint height;
        public uint width;
        public uint depth;
        public uint numSurfaces;
        public uint numFaces;
        public uint mipMapCount;
        public uint metaDataSize;
        public byte[] metaData;

        /// <summary>
        /// Constructor taking only values used for easy comparision to the common format types. see GetGraphicsFormat below
        /// </summary>
        /// <param name="order">Four charactar array specifiying the component order e.g. r,g,b,a</param>
        /// <param name="bits">Four values representing the bit count of each component e.g. 8,8,8,8</param>
        /// <param name="cspace">Colour space 0=linear, 1=srgb</param>
        /// <param name="ctype">PVR_CHANNEL_TYPE for the format</param>
        public PVR_HEADER(char[] order, ushort[] bits, uint cspace, PVR_CHANNEL_TYPE ctype)
        {
            version = 0;
            flags = 0;
            pixelFormat = PVR_FORMAT.None;
            componentOrder = order;
            componentBits = bits;
            colourSpace = cspace;
            channelType = ctype;
            height = 0;
            width = 0;
            depth = 0;
            numSurfaces = 0;
            numFaces = 0;
            mipMapCount = 0;
            metaDataSize = 0;
            metaData = null;
        }

        /// <summary>
        /// Read the header of a PVR file from the passed BinaryReader
        /// The reader should be pointing to the start of the file.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns>True if read is successful</returns>
        public bool Read(BinaryReader reader)
        {
            version = reader.ReadUInt32();
            flags = reader.ReadUInt32();

            byte[] pf1 = reader.ReadBytes(4);
            byte[] pf2 = reader.ReadBytes(4);
            int pf2i = BitConverter.ToInt32(pf2, 0);
            if (pf2i == 0)
            {
                pixelFormat = (PVR_FORMAT)BitConverter.ToUInt32(pf1, 0);
                componentOrder = null;
                componentBits = null;
            }
            else
            {
                pixelFormat = PVR_FORMAT.None;
                componentOrder = new char[4];
                componentBits = new ushort[4];
                for (var i = 0; i < 4; i++)
                {
                    componentOrder[i] = (char)pf1[i];
                    componentBits[i] = (ushort)pf2[i];
                }
            }

            colourSpace = reader.ReadUInt32();
            channelType = (PVR_CHANNEL_TYPE)reader.ReadUInt32();
            height = reader.ReadUInt32();
            width = reader.ReadUInt32();
            depth = reader.ReadUInt32();
            numSurfaces = reader.ReadUInt32();
            numFaces = reader.ReadUInt32();
            mipMapCount = reader.ReadUInt32();
            metaDataSize = reader.ReadUInt32();

            metaData = reader.ReadBytes((int)metaDataSize);

            return true;
        }

        public bool Compare(PVR_HEADER b)
        {
            return componentOrder != null && b.componentOrder != null && componentOrder.SequenceEqual(b.componentOrder) &&
                componentBits != null && b.componentBits != null && componentBits.SequenceEqual(b.componentBits) &&
                colourSpace == b.colourSpace && channelType == b.channelType;
        }
    }

    /// <summary>
    /// TextureFormat implementation for PVR texture files
    /// http://powervr-graphics.github.io/WebGL_SDK/WebGL_SDK/Documentation/Specifications/PVR%20File%20Format.Specification.pdf
    /// </summary>
    public class TextureFileFormatPVR : TextureFileFormat
    {
        /// <summary>
        /// Abstract, should be implemented by concrete type
        /// </summary>
        public override byte[] headerMagic => new byte[] { 80, 86, 82, 3 }; // 0x03525650, if endianess does match.  0x50565203, if endianess does not match.

        /// <summary>
        /// Abstract, should be implemented by concrete type
        /// </summary>
        /// <param name="bytes">The bytes of a file in this format</param>
        /// <returns>TextureParams with decoded data for this format</returns>
        public override TextureParams GetTextureParams(byte[] bytes)
        {
            TextureParams texparams = new TextureParams();
            if (!usingHeaderMagic(bytes)) return texparams;

            var reader = new BinaryReader(new MemoryStream(bytes));

            // read the dds header
            PVR_HEADER header = new PVR_HEADER();
            if (!header.Read(reader)) return texparams;

            var stream = reader.BaseStream;
            int byteoffset = (int)stream.Position;

            texparams.width = (int)header.width;
            texparams.height = (int)header.height;
            texparams.mipmaps = (int)header.mipMapCount;
            texparams.dimension = TextureDimension.Tex2D;

            texparams.format = header.pixelFormat == PVR_FORMAT.None ? GetGraphicsFormat(header) : GetGraphicsFormat(header.pixelFormat, header.colourSpace == 1);

            texparams.rawTexels = true;
            texparams.texels = new byte[bytes.Length - byteoffset];
            System.Buffer.BlockCopy(bytes, byteoffset, texparams.texels, 0, texparams.texels.Length);

            texparams.valid = true;
            return texparams;
        }

        /// <summary>
        /// Get the Unity GraphicsFormat based on the PVR_FORMAT and color space
        /// </summary>
        /// <param name="format">PVR_FORMAT read from the header</param>
        /// <param name="srgb">Color space read from the header</param>
        /// <returns>Matching GraphicsFormat if one is found otherwise return None</returns>
        public GraphicsFormat GetGraphicsFormat(PVR_FORMAT format, bool srgb)
        {
            if (format == PVR_FORMAT.PVRTC2bppRGB) return srgb ? GraphicsFormat.RGB_PVRTC_2Bpp_SRGB : GraphicsFormat.RGB_PVRTC_2Bpp_UNorm;
            if (format == PVR_FORMAT.PVRTC2bppRGBA) return srgb ? GraphicsFormat.RGBA_PVRTC_2Bpp_SRGB : GraphicsFormat.RGBA_PVRTC_2Bpp_UNorm;
            if (format == PVR_FORMAT.PVRTC4bppRGB) return srgb ? GraphicsFormat.RGB_PVRTC_4Bpp_SRGB : GraphicsFormat.RGB_PVRTC_4Bpp_UNorm;
            if (format == PVR_FORMAT.PVRTC4bppRGBA) return srgb ? GraphicsFormat.RGBA_PVRTC_4Bpp_SRGB : GraphicsFormat.RGBA_PVRTC_4Bpp_UNorm;
            if (format == PVR_FORMAT.PVRTCII2bpp) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.PVRTCII4bpp) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ETC1) return srgb ? GraphicsFormat.RGB_ETC_UNorm : GraphicsFormat.RGB_ETC_UNorm;
            if (format == PVR_FORMAT.DXT1) return srgb ? GraphicsFormat.RGBA_DXT1_SRGB : GraphicsFormat.RGBA_DXT1_UNorm;
            if (format == PVR_FORMAT.DXT2) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.DXT3) return srgb ? GraphicsFormat.RGBA_DXT3_SRGB : GraphicsFormat.RGBA_DXT3_UNorm;
            if (format == PVR_FORMAT.DXT4) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.DXT5) return srgb ? GraphicsFormat.RGBA_DXT5_SRGB : GraphicsFormat.RGBA_DXT5_UNorm;
            if (format == PVR_FORMAT.BC1) return srgb ? GraphicsFormat.RGBA_DXT1_SRGB : GraphicsFormat.RGBA_DXT1_UNorm;
            if (format == PVR_FORMAT.BC2) return srgb ? GraphicsFormat.RGBA_DXT3_SRGB : GraphicsFormat.RGBA_DXT3_UNorm;
            if (format == PVR_FORMAT.BC3) return srgb ? GraphicsFormat.RGBA_DXT5_SRGB : GraphicsFormat.RGBA_DXT5_UNorm;
            if (format == PVR_FORMAT.BC4) return srgb ? GraphicsFormat.R_BC4_SNorm : GraphicsFormat.R_BC4_UNorm;
            if (format == PVR_FORMAT.BC5) return srgb ? GraphicsFormat.RG_BC5_SNorm : GraphicsFormat.RG_BC5_UNorm;
            if (format == PVR_FORMAT.BC6) return srgb ? GraphicsFormat.RGB_BC6H_SFloat : GraphicsFormat.RGB_BC6H_UFloat;
            if (format == PVR_FORMAT.BC7) return srgb ? GraphicsFormat.RGBA_BC7_SRGB : GraphicsFormat.RGBA_BC7_UNorm;
            if (format == PVR_FORMAT.UYVY) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.YUY2) return srgb ? GraphicsFormat.YUV2 : GraphicsFormat.YUV2;
            if (format == PVR_FORMAT.BW1bpp) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.R9G9B9E5SharedExponent) return srgb ? GraphicsFormat.E5B9G9R9_UFloatPack32 : GraphicsFormat.E5B9G9R9_UFloatPack32;
            if (format == PVR_FORMAT.RGBG8888) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.GRGB8888) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ETC2RGB) return srgb ? GraphicsFormat.RGB_ETC2_SRGB : GraphicsFormat.RGB_ETC2_UNorm;
            if (format == PVR_FORMAT.ETC2RGBA) return srgb ? GraphicsFormat.RGBA_ETC2_SRGB : GraphicsFormat.RGBA_ETC2_UNorm;
            if (format == PVR_FORMAT.ETC2RGBA1) return srgb ? GraphicsFormat.RGB_A1_ETC2_SRGB : GraphicsFormat.RGB_A1_ETC2_UNorm;
            if (format == PVR_FORMAT.EACR11) return srgb ? GraphicsFormat.R_EAC_SNorm : GraphicsFormat.R_EAC_UNorm;
            if (format == PVR_FORMAT.EACRG11) return srgb ? GraphicsFormat.RG_EAC_SNorm : GraphicsFormat.RG_EAC_UNorm;
            if (format == PVR_FORMAT.ASTC_4x4) return srgb ? GraphicsFormat.RGBA_ASTC4X4_SRGB : GraphicsFormat.RGBA_ASTC4X4_UNorm;
            if (format == PVR_FORMAT.ASTC_5x4) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_5x5) return srgb ? GraphicsFormat.RGBA_ASTC5X5_SRGB : GraphicsFormat.RGBA_ASTC5X5_UNorm;
            if (format == PVR_FORMAT.ASTC_6x5) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_6x6) return srgb ? GraphicsFormat.RGBA_ASTC6X6_SRGB : GraphicsFormat.RGBA_ASTC6X6_UNorm;
            if (format == PVR_FORMAT.ASTC_8x5) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_8x6) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_8x8) return srgb ? GraphicsFormat.RGBA_ASTC8X8_SRGB : GraphicsFormat.RGBA_ASTC8X8_UNorm;
            if (format == PVR_FORMAT.ASTC_10x5) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_10x6) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_10x8) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_10x10) return srgb ? GraphicsFormat.RGBA_ASTC10X10_SRGB : GraphicsFormat.RGBA_ASTC10X10_UNorm;
            if (format == PVR_FORMAT.ASTC_12x10) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_12x12) return srgb ? GraphicsFormat.RGBA_ASTC12X12_SRGB : GraphicsFormat.RGBA_ASTC12X12_UNorm;
            if (format == PVR_FORMAT.ASTC_3x3x3) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_4x3x3) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_4x4x3) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_4x4x4) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_5x4x4) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_5x5x4) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_5x5x5) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_6x5x5) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_6x6x5) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            if (format == PVR_FORMAT.ASTC_6x6x6) return srgb ? GraphicsFormat.None : GraphicsFormat.None;
            return GraphicsFormat.None;
        }

        /// <summary>
        /// Uses the componentOrder, componentBits, colorSpace and channelType to find a matching format from
        /// the dictionary common formats
        /// </summary>
        /// <param name="header"></param>
        /// <returns>Matching GraphicsFormat if one is found otherwise return None</returns>
        public GraphicsFormat GetGraphicsFormat(PVR_HEADER header)
        {
            Dictionary<GraphicsFormat, PVR_HEADER> kCommonFormats = new Dictionary<GraphicsFormat, PVR_HEADER>()
            {
                { GraphicsFormat.R8G8B8A8_UNorm, new PVR_HEADER(new char[] { 'r','g','b','a' }, new ushort[] { 8,8,8,8 }, 0, PVR_CHANNEL_TYPE.UnsignedByteNormalised) },
                { GraphicsFormat.R8G8B8A8_SRGB, new PVR_HEADER(new char[] { 'r','g','b','a' }, new ushort[] { 8,8,8,8 }, 1, PVR_CHANNEL_TYPE.UnsignedByteNormalised) },
                { GraphicsFormat.R8G8B8A8_UInt, new PVR_HEADER(new char[] { 'r','g','b','a' }, new ushort[] { 8,8,8,8 }, 0, PVR_CHANNEL_TYPE.UnsignedInteger) },
                { GraphicsFormat.R8G8B8A8_SInt, new PVR_HEADER(new char[] { 'r','g','b','a' }, new ushort[] { 8,8,8,8 }, 0, PVR_CHANNEL_TYPE.SignedInteger) },

                { GraphicsFormat.R16G16B16A16_UNorm, new PVR_HEADER(new char[] { 'r','g','b','a' }, new ushort[] { 16,16,16,16 }, 0, PVR_CHANNEL_TYPE.UnsignedByteNormalised) },
                { GraphicsFormat.R16G16B16A16_SNorm, new PVR_HEADER(new char[] { 'r','g','b','a' }, new ushort[] { 16,16,16,16 }, 0, PVR_CHANNEL_TYPE.SignedByteNormalised) },
                { GraphicsFormat.R16G16B16A16_UInt, new PVR_HEADER(new char[] { 'r','g','b','a' }, new ushort[] { 16,16,16,16 }, 0, PVR_CHANNEL_TYPE.UnsignedInteger) },
                { GraphicsFormat.R16G16B16A16_SInt, new PVR_HEADER(new char[] { 'r','g','b','a' }, new ushort[] { 16,16,16,16 }, 0, PVR_CHANNEL_TYPE.SignedInteger) },
                { GraphicsFormat.R16G16B16A16_SFloat, new PVR_HEADER(new char[] { 'r','g','b','a' }, new ushort[] { 16,16,16,16 }, 0, PVR_CHANNEL_TYPE.Float) },

                { GraphicsFormat.R32G32B32A32_UInt, new PVR_HEADER(new char[] { 'r','g','b','a' }, new ushort[] { 32,32,32,32 }, 0, PVR_CHANNEL_TYPE.UnsignedInteger) },
                { GraphicsFormat.R32G32B32A32_SInt, new PVR_HEADER(new char[] { 'r','g','b','a' }, new ushort[] { 32,32,32,32 }, 0, PVR_CHANNEL_TYPE.SignedInteger) },
                { GraphicsFormat.R32G32B32A32_SFloat, new PVR_HEADER(new char[] { 'r','g','b','a' }, new ushort[] { 32,32,32,32 }, 0, PVR_CHANNEL_TYPE.Float) },

                { GraphicsFormat.R8_UNorm, new PVR_HEADER(new char[] { 'r','\0','\0','\0' }, new ushort[] { 8,0,0,0 }, 0, PVR_CHANNEL_TYPE.UnsignedByteNormalised) },
                { GraphicsFormat.R8_SNorm, new PVR_HEADER(new char[] { 'r','\0','\0','\0' }, new ushort[] { 8,0,0,0 }, 0, PVR_CHANNEL_TYPE.SignedByteNormalised) },
                { GraphicsFormat.R8_UInt, new PVR_HEADER(new char[] { 'r','\0','\0','\0' }, new ushort[] { 8,0,0,0 }, 0, PVR_CHANNEL_TYPE.UnsignedInteger) },
                { GraphicsFormat.R8_SInt, new PVR_HEADER(new char[] { 'r','\0','\0','\0' }, new ushort[] { 8,0,0,0 }, 0, PVR_CHANNEL_TYPE.SignedInteger) },
                { GraphicsFormat.R8_SRGB, new PVR_HEADER(new char[] { 'r','\0','\0','\0' }, new ushort[] { 8,0,0,0 }, 1, PVR_CHANNEL_TYPE.UnsignedByteNormalised) },

                { GraphicsFormat.R16_UNorm, new PVR_HEADER(new char[] { 'r','\0','\0','\0' }, new ushort[] { 16,0,0,0 }, 1, PVR_CHANNEL_TYPE.UnsignedByteNormalised) },
                { GraphicsFormat.R16_SNorm, new PVR_HEADER(new char[] { 'r','\0','\0','\0' }, new ushort[] { 16,0,0,0 }, 1, PVR_CHANNEL_TYPE.SignedByteNormalised) },
                { GraphicsFormat.R16_UInt, new PVR_HEADER(new char[] { 'r','\0','\0','\0' }, new ushort[] { 16,0,0,0 }, 1, PVR_CHANNEL_TYPE.UnsignedInteger) },
                { GraphicsFormat.R16_SInt, new PVR_HEADER(new char[] { 'r','\0','\0','\0' }, new ushort[] { 16,0,0,0 }, 1, PVR_CHANNEL_TYPE.SignedInteger) },
                { GraphicsFormat.R16_SFloat, new PVR_HEADER(new char[] { 'r','\0','\0','\0' }, new ushort[] { 16,0,0,0 }, 1, PVR_CHANNEL_TYPE.Float) },
            };

            foreach (GraphicsFormat gf in kCommonFormats.Keys)
            {
                if (kCommonFormats[gf].Compare(header)) return gf;
            }

            return GraphicsFormat.None;
        }
    }
}