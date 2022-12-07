

namespace Hbx.Assets
{
    public interface ITextureFileFormat
    {
        /// <summary>
        /// The header bytes that identify this format
        /// </summary>
        byte[] headerMagic { get; }

        /// <summary>
        /// Check if the passed bytes start with the headerMagic of this format
        /// </summary>
        /// <param name="bytes">Array of bytes to check</param>
        /// <returns>True if bytes start with headerId</returns>
        bool usingHeaderMagic(byte[] bytes);

        /// <summary>
        /// Populate TextureParams for the bytes representing a file in this format
        /// </summary>
        /// <param name="bytes">The bytes of a file in this format</param>
        /// <returns>TextureParams with decoded data for this format</returns>
        TextureParams GetTextureParams(byte[] bytes);
    }

    /// <summary>
    /// Abstract implementation of ITextureFormat interface
    /// Mainly provides generic implementations of some functionality
    /// e.g. usingHeaderId
    /// </summary>
    public abstract class TextureFileFormat : ITextureFileFormat
    {
        /// <summary>
        /// Abstract, should be implemented by concrete type
        /// </summary>
        public abstract byte[] headerMagic { get; }

        /// <summary>
        /// Default implementation does straight check that first elements of
        /// bytes matches whole of headerId
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public virtual bool usingHeaderMagic(byte[] bytes)
        {
            return headerMagicCompare(bytes, headerMagic);
        }

        /// <summary>
        /// Abstract, should be implemented by concrete type
        /// </summary>
        /// <param name="bytes">The bytes of a file in this format</param>
        /// <returns>TextureParams with decoded data for this format</returns>
        public abstract TextureParams GetTextureParams(byte[] bytes);

        /// <summary>
        /// Compare headerMagic length bytes between headerMagic and bytes
        /// </summary>
        /// <param name="bytes">Bytes from a file</param>
        /// <param name="headerMagic">The header magic to check for</param>
        public static bool headerMagicCompare(byte[] bytes, byte[] headerMagic)
        {
            if (bytes.Length < headerMagic.Length) return false;
            for(var i = 0; i < headerMagic.Length; i++)
            {
                if (bytes[i] != headerMagic[i]) return false;
            }
            return true;
        }
    }
}
