//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// TextureFileFormatDDS.cs
//----------------------------------------------

using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;

namespace Hbx.Assets
{
    public enum DXGI_FORMAT
    {
        DXGI_FORMAT_UNKNOWN = 0,
        DXGI_FORMAT_R32G32B32A32_TYPELESS = 1,
        DXGI_FORMAT_R32G32B32A32_FLOAT = 2,
        DXGI_FORMAT_R32G32B32A32_UINT = 3,
        DXGI_FORMAT_R32G32B32A32_SINT = 4,
        DXGI_FORMAT_R32G32B32_TYPELESS = 5,
        DXGI_FORMAT_R32G32B32_FLOAT = 6,
        DXGI_FORMAT_R32G32B32_UINT = 7,
        DXGI_FORMAT_R32G32B32_SINT = 8,
        DXGI_FORMAT_R16G16B16A16_TYPELESS = 9,
        DXGI_FORMAT_R16G16B16A16_FLOAT = 10,
        DXGI_FORMAT_R16G16B16A16_UNORM = 11,
        DXGI_FORMAT_R16G16B16A16_UINT = 12,
        DXGI_FORMAT_R16G16B16A16_SNORM = 13,
        DXGI_FORMAT_R16G16B16A16_SINT = 14,
        DXGI_FORMAT_R32G32_TYPELESS = 15,
        DXGI_FORMAT_R32G32_FLOAT = 16,
        DXGI_FORMAT_R32G32_UINT = 17,
        DXGI_FORMAT_R32G32_SINT = 18,
        DXGI_FORMAT_R32G8X24_TYPELESS = 19,
        DXGI_FORMAT_D32_FLOAT_S8X24_UINT = 20,
        DXGI_FORMAT_R32_FLOAT_X8X24_TYPELESS = 21,
        DXGI_FORMAT_X32_TYPELESS_G8X24_UINT = 22,
        DXGI_FORMAT_R10G10B10A2_TYPELESS = 23,
        DXGI_FORMAT_R10G10B10A2_UNORM = 24,
        DXGI_FORMAT_R10G10B10A2_UINT = 25,
        DXGI_FORMAT_R11G11B10_FLOAT = 26,
        DXGI_FORMAT_R8G8B8A8_TYPELESS = 27,
        DXGI_FORMAT_R8G8B8A8_UNORM = 28,
        DXGI_FORMAT_R8G8B8A8_UNORM_SRGB = 29,
        DXGI_FORMAT_R8G8B8A8_UINT = 30,
        DXGI_FORMAT_R8G8B8A8_SNORM = 31,
        DXGI_FORMAT_R8G8B8A8_SINT = 32,
        DXGI_FORMAT_R16G16_TYPELESS = 33,
        DXGI_FORMAT_R16G16_FLOAT = 34,
        DXGI_FORMAT_R16G16_UNORM = 35,
        DXGI_FORMAT_R16G16_UINT = 36,
        DXGI_FORMAT_R16G16_SNORM = 37,
        DXGI_FORMAT_R16G16_SINT = 38,
        DXGI_FORMAT_R32_TYPELESS = 39,
        DXGI_FORMAT_D32_FLOAT = 40,
        DXGI_FORMAT_R32_FLOAT = 41,
        DXGI_FORMAT_R32_UINT = 42,
        DXGI_FORMAT_R32_SINT = 43,
        DXGI_FORMAT_R24G8_TYPELESS = 44,
        DXGI_FORMAT_D24_UNORM_S8_UINT = 45,
        DXGI_FORMAT_R24_UNORM_X8_TYPELESS = 46,
        DXGI_FORMAT_X24_TYPELESS_G8_UINT = 47,
        DXGI_FORMAT_R8G8_TYPELESS = 48,
        DXGI_FORMAT_R8G8_UNORM = 49,
        DXGI_FORMAT_R8G8_UINT = 50,
        DXGI_FORMAT_R8G8_SNORM = 51,
        DXGI_FORMAT_R8G8_SINT = 52,
        DXGI_FORMAT_R16_TYPELESS = 53,
        DXGI_FORMAT_R16_FLOAT = 54,
        DXGI_FORMAT_D16_UNORM = 55,
        DXGI_FORMAT_R16_UNORM = 56,
        DXGI_FORMAT_R16_UINT = 57,
        DXGI_FORMAT_R16_SNORM = 58,
        DXGI_FORMAT_R16_SINT = 59,
        DXGI_FORMAT_R8_TYPELESS = 60,
        DXGI_FORMAT_R8_UNORM = 61,
        DXGI_FORMAT_R8_UINT = 62,
        DXGI_FORMAT_R8_SNORM = 63,
        DXGI_FORMAT_R8_SINT = 64,
        DXGI_FORMAT_A8_UNORM = 65,
        DXGI_FORMAT_R1_UNORM = 66,
        DXGI_FORMAT_R9G9B9E5_SHAREDEXP = 67,
        DXGI_FORMAT_R8G8_B8G8_UNORM = 68,
        DXGI_FORMAT_G8R8_G8B8_UNORM = 69,
        DXGI_FORMAT_BC1_TYPELESS = 70,
        DXGI_FORMAT_BC1_UNORM = 71,
        DXGI_FORMAT_BC1_UNORM_SRGB = 72,
        DXGI_FORMAT_BC2_TYPELESS = 73,
        DXGI_FORMAT_BC2_UNORM = 74,
        DXGI_FORMAT_BC2_UNORM_SRGB = 75,
        DXGI_FORMAT_BC3_TYPELESS = 76,
        DXGI_FORMAT_BC3_UNORM = 77,
        DXGI_FORMAT_BC3_UNORM_SRGB = 78,
        DXGI_FORMAT_BC4_TYPELESS = 79,
        DXGI_FORMAT_BC4_UNORM = 80,
        DXGI_FORMAT_BC4_SNORM = 81,
        DXGI_FORMAT_BC5_TYPELESS = 82,
        DXGI_FORMAT_BC5_UNORM = 83,
        DXGI_FORMAT_BC5_SNORM = 84,
        DXGI_FORMAT_B5G6R5_UNORM = 85,
        DXGI_FORMAT_B5G5R5A1_UNORM = 86,
        DXGI_FORMAT_B8G8R8A8_UNORM = 87,
        DXGI_FORMAT_B8G8R8X8_UNORM = 88,
        DXGI_FORMAT_R10G10B10_XR_BIAS_A2_UNORM = 89,
        DXGI_FORMAT_B8G8R8A8_TYPELESS = 90,
        DXGI_FORMAT_B8G8R8A8_UNORM_SRGB = 91,
        DXGI_FORMAT_B8G8R8X8_TYPELESS = 92,
        DXGI_FORMAT_B8G8R8X8_UNORM_SRGB = 93,
        DXGI_FORMAT_BC6H_TYPELESS = 94,
        DXGI_FORMAT_BC6H_UF16 = 95,
        DXGI_FORMAT_BC6H_SF16 = 96,
        DXGI_FORMAT_BC7_TYPELESS = 97,
        DXGI_FORMAT_BC7_UNORM = 98,
        DXGI_FORMAT_BC7_UNORM_SRGB = 99,
        DXGI_FORMAT_AYUV = 100,
        DXGI_FORMAT_Y410 = 101,
        DXGI_FORMAT_Y416 = 102,
        DXGI_FORMAT_NV12 = 103,
        DXGI_FORMAT_P010 = 104,
        DXGI_FORMAT_P016 = 105,
        DXGI_FORMAT_420_OPAQUE = 106,
        DXGI_FORMAT_YUY2 = 107,
        DXGI_FORMAT_Y210 = 108,
        DXGI_FORMAT_Y216 = 109,
        DXGI_FORMAT_NV11 = 110,
        DXGI_FORMAT_AI44 = 111,
        DXGI_FORMAT_IA44 = 112,
        DXGI_FORMAT_P8 = 113,
        DXGI_FORMAT_A8P8 = 114,
        DXGI_FORMAT_B4G4R4A4_UNORM = 115,
        DXGI_FORMAT_P208 = 130,
        DXGI_FORMAT_V208 = 131,
        DXGI_FORMAT_V408 = 132,
        DXGI_FORMAT_SAMPLER_FEEDBACK_MIN_MIP_OPAQUE,
        DXGI_FORMAT_SAMPLER_FEEDBACK_MIP_REGION_USED_OPAQUE,
        DXGI_FORMAT_FORCE_UINT = unchecked((int)0xFFFFFFFF)
    }

    [Flags]
    public enum DDS_PIXELFORMAT_DWFLAGS
    {
        DDPF_ALPHAPIXELS = 0x1, // Texture contains alpha data; dwRGBAlphaBitMask contains valid data.
        DDPF_ALPHA = 0x2, // Used in some older DDS files for alpha channel only uncompressed data (dwRGBBitCount contains the alpha channel bitcount; dwABitMask contains valid data)
        DDPF_FOURCC = 0x4, // Texture contains compressed RGB data; dwFourCC contains valid data.
        DDPF_RGB = 0x40, // Texture contains uncompressed RGB data; dwRGBBitCount and the RGB masks(dwRBitMask, dwGBitMask, dwBBitMask) contain valid data.
        DDPF_YUV = 0x200, // Used in some older DDS files for YUV uncompressed data(dwRGBBitCount contains the YUV bit count; dwRBitMask contains the Y mask, dwGBitMask contains the U mask, dwBBitMask contains the V mask)
        DDPF_LUMINANCE = 0x20000, // Used in some older DDS files for single channel color uncompressed data(dwRGBBitCount contains the luminance channel bit count; dwRBitMask contains the channel mask). Can be combined with DDPF_ALPHAPIXELS for a two channel DDS file.

        DDS_FOURCC = DDPF_FOURCC,  // DDPF_FOURCC
        DDS_RGB = DDPF_RGB,  // DDPF_RGB
        DDS_RGBA = DDPF_RGB | DDPF_ALPHAPIXELS, // DDPF_RGB | DDPF_ALPHAPIXELS
        DDS_LUMINANCE = DDPF_LUMINANCE,  // DDPF_LUMINANCE
        DDS_LUMINANCEA = DDPF_LUMINANCE | DDPF_ALPHAPIXELS, // DDPF_LUMINANCE | DDPF_ALPHAPIXELS
        DDS_ALPHAPIXELS = DDPF_ALPHAPIXELS, // DDPF_ALPHAPIXELS
        DDS_ALPHA = DDPF_ALPHA, // DDPF_ALPHA
        DDS_PAL8 = 0x00000020,  // DDPF_PALETTEINDEXED8
        DDS_PAL8A = 0x00000021,  // DDPF_PALETTEINDEXED8 | DDPF_ALPHAPIXELS
        DDS_BUMPDUDV = 0x00080000  // DDPF_BUMPDUDV
    }

    public struct DDS_PIXELFORMAT
    {
        public int dwSize;
        public DDS_PIXELFORMAT_DWFLAGS dwFlags;
        public string dwFourCCString;
        //public int dwFourCC;
        public uint dwRGBBitCount;
        public uint dwRBitMask;
        public uint dwGBitMask;
        public uint dwBBitMask;
        public uint dwABitMask;

        public DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS flags, uint bitcount, uint rmask, uint gmask, uint bmask, uint amask)
        {
            dwSize = 0;
            dwFlags = flags;
            dwFourCCString = "";
            dwRGBBitCount = bitcount;
            dwRBitMask = rmask;
            dwGBitMask = gmask;
            dwBBitMask = bmask;
            dwABitMask = amask;
        }

        public DDS_PIXELFORMAT(BinaryReader reader)
        {
            dwSize = reader.ReadInt32();
            dwFlags = (DDS_PIXELFORMAT_DWFLAGS)reader.ReadUInt32();
            char[] fourcc = reader.ReadChars(4);
            dwFourCCString = new string(fourcc);
            //dwFourCC = Convert.ToInt32(dwFourCCString);
            dwRGBBitCount = reader.ReadUInt32();
            dwRBitMask = reader.ReadUInt32();
            dwGBitMask = reader.ReadUInt32();
            dwBBitMask = reader.ReadUInt32();
            dwABitMask = reader.ReadUInt32();
        }

        public bool Compare(DDS_PIXELFORMAT b)
        {
            return dwFlags == b.dwFlags && dwRGBBitCount == b.dwRGBBitCount && dwRBitMask == b.dwRBitMask && dwGBitMask == b.dwGBitMask && dwBBitMask == b.dwBBitMask && dwABitMask == b.dwABitMask;
        }
    };

    [Flags]
    public enum DDS_HEADER_DWFLAGS
    {
        DDSD_CAPS = 0x1, // Required in every .dds file.
        DDSD_HEIGHT = 0x2, // Required in every .dds file.
        DDSD_WIDTH = 0x4, // Required in every .dds file.
        DDSD_PITCH = 0x8, // Required when pitch is provided for an uncompressed texture.
        DDSD_PIXELFORMAT = 0x1000, // Required in every .dds file.
        DDSD_MIPMAPCOUNT = 0x20000, // Required in a mipmapped texture.
        DDSD_LINEARSIZE = 0x80000, // Required when pitch is provided for a compressed texture.
        DDSD_DEPTH = 0x800000 // Required in a depth texture.
    }

    [Flags]
    public enum DDS_HEADER_DWCAPS
    {
        DDSCAPS_COMPLEX = 0x8, // Optional; must be used on any file that contains more than one surface (a mipmap, a cubic environment map, or mipmapped volume texture).
        DDSCAPS_MIPMAP = 0x400000, // Optional; should be used for a mipmap.
        DDSCAPS_TEXTURE = 0x1000, // Required
        DDS_SURFACE_FLAGS_TEXTURE = DDSCAPS_TEXTURE,
        DDS_SURFACE_FLAGS_MIPMAP = DDSCAPS_COMPLEX | DDSCAPS_MIPMAP,
        DDS_SURFACE_FLAGS_CUBEMAP = DDSCAPS_COMPLEX
    }

    [Flags]
    public enum DDS_HEADER_DWCAPS2
    {
        DDSCAPS2_CUBEMAP = 0x200, // Required for a cube map.
        DDSCAPS2_CUBEMAP_POSITIVEX = 0x400, // Required when these surfaces are stored in a cube map.
        DDSCAPS2_CUBEMAP_NEGATIVEX = 0x800, // Required when these surfaces are stored in a cube map.
        DDSCAPS2_CUBEMAP_POSITIVEY = 0x1000, // Required when these surfaces are stored in a cube map.
        DDSCAPS2_CUBEMAP_NEGATIVEY = 0x2000, // Required when these surfaces are stored in a cube map.
        DDSCAPS2_CUBEMAP_POSITIVEZ  = 0x4000, // Required when these surfaces are stored in a cube map.
        DDSCAPS2_CUBEMAP_NEGATIVEZ  = 0x8000, // Required when these surfaces are stored in a cube map.
        DDSCAPS2_VOLUME = 0x200000, // Required for a volume texture.
        DDS_CUBEMAP_POSITIVEX = DDSCAPS2_CUBEMAP | DDSCAPS2_CUBEMAP_POSITIVEX,
        DDS_CUBEMAP_NEGATIVEX = DDSCAPS2_CUBEMAP | DDSCAPS2_CUBEMAP_NEGATIVEX,
        DDS_CUBEMAP_POSITIVEY = DDSCAPS2_CUBEMAP | DDSCAPS2_CUBEMAP_POSITIVEY,
        DDS_CUBEMAP_NEGATIVEY = DDSCAPS2_CUBEMAP | DDSCAPS2_CUBEMAP_NEGATIVEY,
        DDS_CUBEMAP_POSITIVEZ = DDSCAPS2_CUBEMAP | DDSCAPS2_CUBEMAP_POSITIVEZ,
        DDS_CUBEMAP_NEGATIVEZ = DDSCAPS2_CUBEMAP | DDSCAPS2_CUBEMAP_NEGATIVEZ,
        DDS_CUBEMAP_ALLFACES = DDS_CUBEMAP_POSITIVEX | DDS_CUBEMAP_NEGATIVEX | DDS_CUBEMAP_POSITIVEY | DDS_CUBEMAP_NEGATIVEY | DDS_CUBEMAP_POSITIVEZ | DDS_CUBEMAP_NEGATIVEZ,
        DDS_FLAGS_VOLUME = DDSCAPS2_VOLUME
    }

    public enum D3D10_RESOURCE_DIMENSION
    {
        D3D10_RESOURCE_DIMENSION_UNKNOWN = 0,
        D3D10_RESOURCE_DIMENSION_BUFFER = 1,
        D3D10_RESOURCE_DIMENSION_TEXTURE1D = 2,
        D3D10_RESOURCE_DIMENSION_TEXTURE2D = 3,
        D3D10_RESOURCE_DIMENSION_TEXTURE3D = 4
    };

    public struct DDS_HEADER_DXT10
    {
        public DXGI_FORMAT dxgiFormat;
        public D3D10_RESOURCE_DIMENSION resourceDimension;
        public uint miscFlag;
        public uint arraySize;
        public uint miscFlags2;

        public bool Read(BinaryReader reader)
        {
            dxgiFormat = (DXGI_FORMAT)reader.ReadInt32();
            resourceDimension = (D3D10_RESOURCE_DIMENSION)reader.ReadInt32();
            miscFlag = reader.ReadUInt32();
            arraySize = reader.ReadUInt32();
            miscFlags2 = reader.ReadUInt32();
            return true;
        }
    }

    public struct DDS_HEADER
    {
        public uint dwSize;
        public DDS_HEADER_DWFLAGS dwFlags;
        public uint dwHeight;
        public uint dwWidth;
        public uint dwPitchOrLinearSize;
        public uint dwDepth;
        public uint dwMipMapCount;
        public uint[] dwReserved1;
        public DDS_PIXELFORMAT ddspf;
        public DDS_HEADER_DWCAPS dwCaps;
        public DDS_HEADER_DWCAPS2 dwCaps2;
        uint dwCaps3;
        uint dwCaps4;
        uint dwReserved2;

        public bool Read(BinaryReader reader)
        {
            uint headermagic = reader.ReadUInt32(); // skip the header magic
            dwSize = reader.ReadUInt32();
            if (dwSize != 124) return false;
            dwFlags = (DDS_HEADER_DWFLAGS)reader.ReadInt32();
            dwHeight = reader.ReadUInt32();
            dwWidth = reader.ReadUInt32();
            dwPitchOrLinearSize = reader.ReadUInt32();
            dwDepth = reader.ReadUInt32();
            dwMipMapCount = reader.ReadUInt32();
            dwReserved1 = new uint[11];
            for (int i = 0; i < dwReserved1.Length; i++) dwReserved1[i] = reader.ReadUInt32();
            ddspf = new DDS_PIXELFORMAT(reader);
            dwCaps = (DDS_HEADER_DWCAPS)reader.ReadUInt32();
            dwCaps2 = (DDS_HEADER_DWCAPS2)reader.ReadUInt32();
            dwCaps3 = reader.ReadUInt32();
            dwCaps4 = reader.ReadUInt32();
            dwReserved2 = reader.ReadUInt32();

            return true;
        }
    }


    /// <summary>
    /// TextureFormat implementation for DDS texture files
    /// </summary>
    public class TextureFileFormatDDS : TextureFileFormat
    {
        /// <summary>
        /// Abstract, should be implemented by concrete type
        /// </summary>
        public override byte[] headerMagic => new byte[] { 68, 68, 83, 32 }; // 0x20534444

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
            DDS_HEADER header = new DDS_HEADER();
            if (!header.Read(reader)) return texparams;

            bool isfourcc = header.ddspf.dwFlags.HasFlag(DDS_PIXELFORMAT_DWFLAGS.DDPF_FOURCC);
            bool isdx10 = false;
            // if it's dx10 format we also read the dxt10 header
            DDS_HEADER_DXT10 dxt10header = new DDS_HEADER_DXT10();
            if(isfourcc && header.ddspf.dwFourCCString == "DX10") isdx10 = dxt10header.Read(reader);

            var stream = reader.BaseStream;
            int byteoffset = (int)stream.Position;

            texparams.width = (int)header.dwWidth;
            texparams.height = (int)header.dwHeight;
            texparams.mipmaps = (int)header.dwMipMapCount;

            texparams.rawTexels = true;
            texparams.texels = new byte[bytes.Length - byteoffset];
            System.Buffer.BlockCopy(bytes, byteoffset, texparams.texels, 0, texparams.texels.Length);

            //texparams.texelStartOffset = (int)byteoffset;

            texparams.format = isdx10 ? GetFormat(dxt10header.dxgiFormat) : GetFormat(header.ddspf);
            Debug.Log("Format: " + texparams.format);
            // check type of texture
            if (header.dwCaps == DDS_HEADER_DWCAPS.DDS_SURFACE_FLAGS_TEXTURE)
            {
                texparams.dimension = TextureDimension.Tex2D;

            }

            texparams.valid = true;
            return texparams;
        }


        // https://learn.microsoft.com/en-us/windows/win32/direct3ddds/dx-graphics-dds-pguide#dds-variants

        protected GraphicsFormat GetFormat(DDS_PIXELFORMAT format)
        {
            // is it a compressed data
            if (format.dwFlags.HasFlag(DDS_PIXELFORMAT_DWFLAGS.DDPF_FOURCC))
            {

                if (format.dwFourCCString == "DXT1") return GraphicsFormat.RGBA_DXT1_UNorm;
                if (format.dwFourCCString == "DXT3") return GraphicsFormat.RGBA_DXT3_UNorm;
                if (format.dwFourCCString == "DXT5") return GraphicsFormat.RGBA_DXT5_UNorm;
                if (format.dwFourCCString == "BC4U") return GraphicsFormat.R_BC4_UNorm;
                if (format.dwFourCCString == "BC4S") return GraphicsFormat.R_BC4_SNorm;
                if (format.dwFourCCString == "ATI2") return GraphicsFormat.RG_BC5_UNorm;
                if (format.dwFourCCString == "BC5S") return GraphicsFormat.RG_BC5_SNorm;
            }
            else
            {

                Dictionary<GraphicsFormat, DDS_PIXELFORMAT> kCommonFormats = new Dictionary<GraphicsFormat, DDS_PIXELFORMAT>()
                {
                    { GraphicsFormat.R8G8B8A8_UNorm, new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGBA, 32, 0xff, 0xff00, 0xff0000, 0xff000000) }, // DXGI_FORMAT_R8G8B8A8_UNORM
                    //{ GraphicsFormat.R16G16_UNorm, new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGBA, 32, 0xffff, 0xffff0000, 0, 0) }, // DXGI_FORMAT_R16G16_UNORM
                    //{ GraphicsFormat.A2R10G10B10_UNormPack32, new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGBA, 32, 0x3ff, 0xffc00, 0x3ff00000, 0) }, // DXGI_FORMAT_R10G10B10A2_UNORM !!
                    { GraphicsFormat.R16G16_UNorm, new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGB, 32, 0xffff, 0xffff0000, 0, 0) }, // DXGI_FORMAT_R16G16_UNORM
                    { GraphicsFormat.B5G5R5A1_UNormPack16, new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGBA, 16, 0x7c00, 0x3e0, 0x1f, 0x8000) }, // DXGI_FORMAT_B5G5R5A1_UNORM
                    { GraphicsFormatUtility.GetGraphicsFormat(RenderTextureFormat.RGB565, false), new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGB, 16, 0xf800, 0x7e0, 0x1f, 0) }, // DXGI_FORMAT_B5G6R5_UNORM
                    { GraphicsFormatUtility.GetGraphicsFormat(TextureFormat.Alpha8, false), new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_ALPHA, 8, 0, 0, 0, 0xff) }, // DXGI_A8_UNORM
                    { GraphicsFormatUtility.GetGraphicsFormat(TextureFormat.ARGB32, false), new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGBA, 32, 0xff0000, 0xff00, 0xff, 0xff000000) }, // D3DFMT_A8R8G8B8
                    //{ GraphicsFormatUtility.GetGraphicsFormat(TextureFormat.ARGB32, false), new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGB, 32, 0xff0000, 0xff00, 0xff, 0) }, //D3DFMT_X8R8G8B8
                    { GraphicsFormat.B8G8R8A8_UNorm, new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGB, 32, 0xff, 0xff00, 0xff0000, 0) }, // D3DFMT_X8B8G8R8
                    { GraphicsFormat.A2R10G10B10_UNormPack32, new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGBA, 32, 0x3ff00000, 0xffc00, 0x3ff, 0xc0000000) }, // D3DFMT_A2R10G10B10
                    { GraphicsFormat.R8G8B8_UNorm, new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGB, 24, 0xff0000, 0xff00, 0xff, 0) }, // D3DFMT_R8G8B8
                    { GraphicsFormat.R5G5B5A1_UNormPack16, new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGB, 16, 0x7c00, 0x3e0, 0x1f , 0) }, // D3DFMT_X1R5G5B5 !!
                    { GraphicsFormatUtility.GetGraphicsFormat(TextureFormat.ARGB4444, false), new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGBA, 16, 0xf00, 0xf0, 0xf, 0xf000) }, // D3DFMT_A4R4G4B4
                    //{ GraphicsFormatUtility.GetGraphicsFormat(TextureFormat.ARGB4444, false), new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_RGB, 16, 0xf00, 0xf0, 0xf, 0) }, // D3DFMT_X4R4G4B4 !!
                    // D3DFMT_A8R3G3B2 DDS_RGBA    16 0xe0 0x1c 0x3 0xff00 // not supported
                    { GraphicsFormat.R8G8_UNorm, new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_LUMINANCE, 16, 0xff, 0, 0, 0xff00) }, // D3DFMT_A8L8
                    { GraphicsFormat.R16_UNorm, new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_LUMINANCE, 16, 0xffff, 0, 0, 0) }, // D3DFMT_L16
                    { GraphicsFormat.R8_UNorm, new DDS_PIXELFORMAT(DDS_PIXELFORMAT_DWFLAGS.DDS_LUMINANCE, 16, 0xff, 0, 0, 0) } // D3DFMT_L8
                    // D3DFMT_A4L4 DDS_LUMINANCE   8 0xf   0xf0 // not supported
                };

                foreach (GraphicsFormat gf in kCommonFormats.Keys)
                {
                    if (kCommonFormats[gf].Compare(format)) return gf;
                }
            }

            return GraphicsFormat.None;
        }

        protected GraphicsFormat GetFormat(DXGI_FORMAT format)
        {
            switch(format)
            {
                case DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_TYPELESS: return GraphicsFormat.R32G32B32A32_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_FLOAT: return GraphicsFormat.R32G32B32A32_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_UINT: return GraphicsFormat.R32G32B32A32_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_R32G32B32A32_SINT: return GraphicsFormat.R32G32B32A32_SInt;
                case DXGI_FORMAT.DXGI_FORMAT_R32G32B32_TYPELESS: return GraphicsFormat.R32G32B32_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R32G32B32_FLOAT: return GraphicsFormat.R32G32B32_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R32G32B32_UINT: return GraphicsFormat.R32G32B32_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R32G32B32_SINT: return GraphicsFormat.R32G32B32_SInt;
                case DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_TYPELESS: return GraphicsFormat.R16G16B16A16_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_FLOAT: return GraphicsFormat.R16G16B16A16_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_UNORM: return GraphicsFormat.R16G16B16A16_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_UINT: return GraphicsFormat.R16G16B16A16_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_SNORM: return GraphicsFormat.R16G16B16A16_SNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R16G16B16A16_SINT: return GraphicsFormat.R16G16B16A16_SInt;
                case DXGI_FORMAT.DXGI_FORMAT_R32G32_TYPELESS: return GraphicsFormat.R32G32_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R32G32_FLOAT: return GraphicsFormat.R32G32_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R32G32_UINT: return GraphicsFormat.R32G32_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_R32G32_SINT: return GraphicsFormat.R32G32_SInt;
                case DXGI_FORMAT.DXGI_FORMAT_R32G8X24_TYPELESS: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_D32_FLOAT_S8X24_UINT: return GraphicsFormat.D32_SFloat_S8_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_R32_FLOAT_X8X24_TYPELESS: return GraphicsFormat.D32_SFloat_S8_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_X32_TYPELESS_G8X24_UINT: return GraphicsFormat.D32_SFloat_S8_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_R10G10B10A2_TYPELESS: return GraphicsFormat.A2R10G10B10_UNormPack32; // is this right?
                case DXGI_FORMAT.DXGI_FORMAT_R10G10B10A2_UNORM: return GraphicsFormat.A2R10G10B10_UNormPack32; // is this right?
                case DXGI_FORMAT.DXGI_FORMAT_R10G10B10A2_UINT: return GraphicsFormat.A2R10G10B10_UIntPack32; // is this right?
                case DXGI_FORMAT.DXGI_FORMAT_R11G11B10_FLOAT: return GraphicsFormat.B10G11R11_UFloatPack32; // is this right?
                case DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_TYPELESS: return GraphicsFormat.R8G8B8A8_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM: return GraphicsFormat.R8G8B8A8_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UNORM_SRGB: return GraphicsFormat.R8G8B8A8_SRGB;
                case DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_UINT: return GraphicsFormat.R8G8B8A8_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_SNORM: return GraphicsFormat.R8G8B8A8_SNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R8G8B8A8_SINT: return GraphicsFormat.R8G8B8A8_SInt;
                case DXGI_FORMAT.DXGI_FORMAT_R16G16_TYPELESS: return GraphicsFormat.R16G16_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R16G16_FLOAT: return GraphicsFormat.R16G16_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R16G16_UNORM: return GraphicsFormat.R16G16_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R16G16_UINT: return GraphicsFormat.R16G16_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_R16G16_SNORM: return GraphicsFormat.R16G16_SNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R16G16_SINT: return GraphicsFormat.R16G16_SInt;
                case DXGI_FORMAT.DXGI_FORMAT_R32_TYPELESS: return GraphicsFormat.R32_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_D32_FLOAT: return GraphicsFormat.D32_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R32_FLOAT: return GraphicsFormat.R32_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R32_UINT: return GraphicsFormat.R32_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_R32_SINT: return GraphicsFormat.R32_SInt;
                case DXGI_FORMAT.DXGI_FORMAT_R24G8_TYPELESS: return GraphicsFormat.D24_UNorm_S8_UInt; // is this right ?
                case DXGI_FORMAT.DXGI_FORMAT_D24_UNORM_S8_UINT: return GraphicsFormat.D24_UNorm_S8_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_R24_UNORM_X8_TYPELESS: return GraphicsFormat.D24_UNorm_S8_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_X24_TYPELESS_G8_UINT: return GraphicsFormat.D24_UNorm_S8_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_R8G8_TYPELESS: return GraphicsFormat.R8G8_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R8G8_UNORM: return GraphicsFormat.R8G8_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R8G8_UINT: return GraphicsFormat.R8G8_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_R8G8_SNORM: return GraphicsFormat.R8G8_SNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R8G8_SINT: return GraphicsFormat.R8G8_SInt;
                case DXGI_FORMAT.DXGI_FORMAT_R16_TYPELESS: return GraphicsFormat.R16_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_R16_FLOAT: return GraphicsFormat.R16_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_D16_UNORM: return GraphicsFormat.D16_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R16_UNORM: return GraphicsFormat.R16_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R16_UINT: return GraphicsFormat.R16_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_R16_SNORM: return GraphicsFormat.R16_SNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R16_SINT: return GraphicsFormat.R16_SInt;
                case DXGI_FORMAT.DXGI_FORMAT_R8_TYPELESS: return GraphicsFormat.R8_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R8_UNORM: return GraphicsFormat.R8_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R8_UINT: return GraphicsFormat.R8_UInt;
                case DXGI_FORMAT.DXGI_FORMAT_R8_SNORM: return GraphicsFormat.R8_SNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R8_SINT: return GraphicsFormat.R8_SInt;
                case DXGI_FORMAT.DXGI_FORMAT_A8_UNORM: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_R1_UNORM: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_R9G9B9E5_SHAREDEXP: return GraphicsFormat.E5B9G9R9_UFloatPack32; // is this right?
                case DXGI_FORMAT.DXGI_FORMAT_R8G8_B8G8_UNORM: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_G8R8_G8B8_UNORM: return GraphicsFormat.None;


                case DXGI_FORMAT.DXGI_FORMAT_BC1_TYPELESS: return GraphicsFormat.RGBA_DXT1_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM: return GraphicsFormat.RGBA_DXT1_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_BC1_UNORM_SRGB: return GraphicsFormat.RGBA_DXT1_SRGB;
                case DXGI_FORMAT.DXGI_FORMAT_BC2_TYPELESS: return GraphicsFormat.RGBA_DXT3_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM: return GraphicsFormat.RGBA_DXT3_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_BC2_UNORM_SRGB: return GraphicsFormat.RGBA_DXT3_SRGB;
                case DXGI_FORMAT.DXGI_FORMAT_BC3_TYPELESS: return GraphicsFormat.RGBA_DXT5_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM: return GraphicsFormat.RGBA_DXT5_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_BC3_UNORM_SRGB: return GraphicsFormat.RGBA_DXT5_SRGB;
                case DXGI_FORMAT.DXGI_FORMAT_BC4_TYPELESS: return GraphicsFormat.R_BC4_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_BC4_UNORM: return GraphicsFormat.R_BC4_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_BC4_SNORM: return GraphicsFormat.R_BC4_SNorm;
                case DXGI_FORMAT.DXGI_FORMAT_BC5_TYPELESS: return GraphicsFormat.RG_BC5_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_BC5_UNORM: return GraphicsFormat.RG_BC5_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_BC5_SNORM: return GraphicsFormat.RG_BC5_SNorm;
                case DXGI_FORMAT.DXGI_FORMAT_B5G6R5_UNORM: return GraphicsFormat.B5G6R5_UNormPack16;
                case DXGI_FORMAT.DXGI_FORMAT_B5G5R5A1_UNORM: return GraphicsFormat.B5G5R5A1_UNormPack16;
                case DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM: return GraphicsFormat.B8G8R8A8_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_B8G8R8X8_UNORM: return GraphicsFormat.B8G8R8A8_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_R10G10B10_XR_BIAS_A2_UNORM: return GraphicsFormat.A2R10G10B10_XRUNormPack32;
                case DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_TYPELESS: return GraphicsFormat.B8G8R8A8_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_B8G8R8A8_UNORM_SRGB: return GraphicsFormat.B8G8R8A8_SRGB;
                case DXGI_FORMAT.DXGI_FORMAT_B8G8R8X8_TYPELESS: return GraphicsFormat.B8G8R8A8_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_B8G8R8X8_UNORM_SRGB: return GraphicsFormat.B8G8R8A8_SRGB;
                case DXGI_FORMAT.DXGI_FORMAT_BC6H_TYPELESS: return GraphicsFormat.RGB_BC6H_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_BC6H_UF16: return GraphicsFormat.RGB_BC6H_UFloat;
                case DXGI_FORMAT.DXGI_FORMAT_BC6H_SF16: return GraphicsFormat.RGB_BC6H_SFloat;
                case DXGI_FORMAT.DXGI_FORMAT_BC7_TYPELESS: return GraphicsFormat.RGBA_BC7_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM: return GraphicsFormat.RGBA_BC7_UNorm;
                case DXGI_FORMAT.DXGI_FORMAT_BC7_UNORM_SRGB: return GraphicsFormat.RGBA_BC7_SRGB;


                case DXGI_FORMAT.DXGI_FORMAT_AYUV: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_Y410: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_Y416: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_NV12: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_P010: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_P016: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_420_OPAQUE: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_YUY2: return GraphicsFormat.YUV2;
                case DXGI_FORMAT.DXGI_FORMAT_Y210: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_Y216: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_NV11: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_AI44: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_IA44: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_P8: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_A8P8: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_B4G4R4A4_UNORM: return GraphicsFormat.B4G4R4A4_UNormPack16;
                case DXGI_FORMAT.DXGI_FORMAT_P208: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_V208: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_V408: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_SAMPLER_FEEDBACK_MIN_MIP_OPAQUE: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_SAMPLER_FEEDBACK_MIP_REGION_USED_OPAQUE: return GraphicsFormat.None;
                case DXGI_FORMAT.DXGI_FORMAT_FORCE_UINT: return GraphicsFormat.None;

                default: return GraphicsFormat.None;

            }
        }
    }
}
