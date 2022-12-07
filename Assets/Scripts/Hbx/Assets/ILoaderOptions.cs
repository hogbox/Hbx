//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// ILoaderOptions.cs
//----------------------------------------------

public interface ILoaderOptions
{
    /// <summary>
    /// The original path or src string used in the load operation
    /// i.e. if we load a file from disk then pass the bytes to another
    /// loader this can tell us what the original file was
    /// </summary>
    string originalSrc { get; set; }

    /// <summary>
    /// Reference to any bytes loaded up to this point.
    /// This can be useful if say the http loader fails
    /// to load a Texture2D but has downloaed the bytes
    /// and we want to pass them to another loader.
    /// </summary>
    byte[] loadedBytes { get; set; }

    /// <summary>
    /// Sets the original source only if it hasn't already been set
    /// </summary>
    /// <param name="src"></param>
    void setOriginalSrcIfEmpty(string src);
}

public class LoaderOptions : ILoaderOptions
{
    public string originalSrc { get; set; }

    public byte[] loadedBytes { get; set; }

    public LoaderOptions()
    {
    }

    public void setOriginalSrcIfEmpty(string src)
    {
        if(string.IsNullOrEmpty(originalSrc))
        {
            originalSrc = src;
        }
    }
}