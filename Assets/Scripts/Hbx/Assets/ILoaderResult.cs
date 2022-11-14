//----------------------------------------------
//            Hbx: Assets
// Copyright © 2022 Hogbox Studios
// ILoaderResult.cs
//----------------------------------------------

/// <summary>
/// Non generic base interface for ILoaderResult to allow usage in Lists etc
/// </summary>
public interface ILoaderResult
{
    /// <summary>
    /// Is the result valid
    /// </summary>
    bool valid { get; }

    /// <summary>
    /// The data for the result
    /// </summary>
    object data { get; }
}

/// <summary>
/// Generic Interface for any type of result/type that a Loader can handle
/// </summary>
public interface ILoaderResult<T> : ILoaderResult
{
    /// <summary>
    /// The data for the result
    /// </summary>
    new T data { get; }
}
