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
public class ILoaderResult<T> : ILoaderResult
{
    T _data = default;

    public ILoaderResult()
    {
        _data = default;
    }

    public ILoaderResult(T data)
    {
        _data = data;
    }

    public static implicit operator T(ILoaderResult<T> d) => d.data;

    public bool valid => _data != null;

    public T data => _data;

    object ILoaderResult.data => throw new System.NotImplementedException();
}
