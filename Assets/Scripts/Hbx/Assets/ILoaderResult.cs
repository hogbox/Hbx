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
    /// Reason for any error
    /// </summary>
    string error { get; }

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
    string _error = null;
    bool _set = false;

    public ILoaderResult()
    {
        _data = default;
        _error = null;
        _set = false;
    }

    public ILoaderResult(T data)
    {
        _data = data;
        _error = null;
        _set = true;
    }

    public ILoaderResult(string error)
    {
        _data = default;
        _error = error;
        _set = false;
    }


    public static implicit operator T(ILoaderResult<T> d) => d.data;

    public bool valid => _data != null && _set;

    public string error => _error;

    public T data => _data;

    object ILoaderResult.data => throw new System.NotImplementedException();
}
