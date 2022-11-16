using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Protocol
{
    FILE,
    HTTP,
    HTTPS,
    JSON,
    RESOURCES,
    ADDRESSABLE
}

public static class Protocols
{
    public static readonly string FILE_PREFIX = "file://";
    public static readonly string HTTP_PREFIX = "http://";
    public static readonly string HTTPS_PREFIX = "https://";
    public static readonly string JSON_PREFIX = "json://";
    public static readonly string RESOURCES_PREFIX = "resources://";
    public static readonly string ADDRESSABLE_PREFIX = "address://";

    public static Protocol DefaultProtocolForPlatform ()
    {
#if UNITY_WEBGL
        return ProtocolForPath(Application.absoluteURL);
#else
        return Protocol.FILE;
#endif
    }

    public static string PrefixForProtocol(Protocol p)
    {
        switch (p)
        {
            case Protocol.FILE: return FILE_PREFIX;
            case Protocol.HTTP: return HTTP_PREFIX;
            case Protocol.HTTPS: return HTTPS_PREFIX;
            case Protocol.JSON: return JSON_PREFIX;
            case Protocol.RESOURCES: return RESOURCES_PREFIX;
            case Protocol.ADDRESSABLE: return ADDRESSABLE_PREFIX;
            default: return null;
        }
    }

    public static bool IsPathUsingProtocol(string path, Protocol p)
    {
        return path.StartsWith(PrefixForProtocol(p));
    }

    public static Protocol ProtocolForPath(string path)
    {
        if (IsPathUsingProtocol(path, Protocol.FILE)) return Protocol.FILE;
        if (IsPathUsingProtocol(path, Protocol.HTTP)) return Protocol.HTTP;
        if (IsPathUsingProtocol(path, Protocol.HTTPS)) return Protocol.HTTPS;
        if (IsPathUsingProtocol(path, Protocol.JSON)) return Protocol.JSON;
        if (IsPathUsingProtocol(path, Protocol.RESOURCES)) return Protocol.RESOURCES;
        if (IsPathUsingProtocol(path, Protocol.ADDRESSABLE)) return Protocol.ADDRESSABLE;
        return DefaultProtocolForPlatform();
    }

    public static bool ProtocolRequiresFetch(Protocol protocol)
    {
        if(protocol == Protocol.FILE ||
            protocol == Protocol.HTTP ||
            protocol == Protocol.HTTPS)
        {
            return true;
        }
        return false;
    }
}
