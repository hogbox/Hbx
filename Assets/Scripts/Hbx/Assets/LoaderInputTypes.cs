using System;

namespace Hbx.Assets
{
    /// <summary>
    /// Loaders can accept different types of input
    /// Can be used as a mask as loaders may support multiple types
    /// </summary>
    [Flags]
    public enum LoaderInputType
    {
        Path = 0,
        Bytes = 1,
        Text = 2
    }
}
