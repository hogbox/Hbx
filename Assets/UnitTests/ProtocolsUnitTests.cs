using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using Hbx.Assets;

public class ProtocolsUnitTests
{
    [Test]
    public void ReturnsFilePrefixForProtocol()
    {
        // arrange
        Protocol protocol = Protocol.FILE;
        // act
        string fileprefix = Protocols.PrefixForProtocol(protocol);
        // assert
        Assert.AreEqual(Protocols.FILE_PREFIX, fileprefix);
    }

    [Test]
    public void ReturnsHttpPrefixForProtocol()
    {
        // arrange
        Protocol protocol = Protocol.HTTP;
        // act
        string httpprefix = Protocols.PrefixForProtocol(protocol);
        // assert
        Assert.AreEqual(Protocols.HTTP_PREFIX, httpprefix);
    }

    [Test]
    public void ReturnsHttpsPrefixForProtocol()
    {
        // arrange
        Protocol protocol = Protocol.HTTPS;
        // act
        string httpsprefix = Protocols.PrefixForProtocol(protocol);
        // assert
        Assert.AreEqual(Protocols.HTTPS_PREFIX, httpsprefix);
    }

    [Test]
    public void ReturnsJsonPrefixForProtocol()
    {
        // arrange
        Protocol protocol = Protocol.JSON;
        // act
        string jsonprefix = Protocols.PrefixForProtocol(protocol);
        // assert
        Assert.AreEqual(Protocols.JSON_PREFIX, jsonprefix);
    }

    [Test]
    public void ReturnsResourcesPrefixForProtocol()
    {
        // arrange
        Protocol protocol = Protocol.RESOURCES;
        // act
        string resorcesprefix = Protocols.PrefixForProtocol(protocol);
        // assert
        Assert.AreEqual(Protocols.RESOURCES_PREFIX, resorcesprefix);
    }

    [Test]
    public void ReturnsAddressablePrefixForProtocol()
    {
        // arrange
        Protocol protocol = Protocol.ADDRESSABLE;
        // act
        string addressprefix = Protocols.PrefixForProtocol(protocol);
        // assert
        Assert.AreEqual(Protocols.ADDRESSABLE_PREFIX, addressprefix);
    }

    [Test]
    public void ReturnsFileProtocolForFilePath()
    {
        // arrange
        string filepath = "file://c:/thisisafilepath/file.png";
        // act
        Protocol protocol = Protocols.ProtocolForPath(filepath);
        // assert
        Assert.AreEqual(Protocol.FILE, protocol);
    }

    [Test]
    public void ReturnsHttpProtocolForHttpPath()
    {
        // arrange
        string httppath = "http://www.thisisahttppath.co.uk/";
        // act
        Protocol protocol = Protocols.ProtocolForPath(httppath);
        // assert
        Assert.AreEqual(Protocol.HTTP, protocol);
    }

    [Test]
    public void ReturnsHttpsProtocolForHttpsPath()
    {
        // arrange
        string httpspath = "https://www.thisisahttppath.co.uk/";
        // act
        Protocol protocol = Protocols.ProtocolForPath(httpspath);
        // assert
        Assert.AreEqual(Protocol.HTTPS, protocol);
    }

    [Test]
    public void ReturnsJsonProtocolForJsonPath()
    {
        // arrange
        string jsonpath = "json://{\"prop\":\"jsonprop\"}";
        // act
        Protocol protocol = Protocols.ProtocolForPath(jsonpath);
        // assert
        Assert.AreEqual(Protocol.JSON, protocol);
    }

    [Test]
    public void ReturnsResourcesProtocolForResourcePath()
    {
        // arrange
        string resourcepath = "resources://pathtomyresource.asset";
        // act
        Protocol protocol = Protocols.ProtocolForPath(resourcepath);
        // assert
        Assert.AreEqual(Protocol.RESOURCES, protocol);
    }

    [Test]
    public void ReturnsAddressableProtocolForAddressPath()
    {
        // arrange
        string addresspath = "address://pathtomyresource.asset";
        // act
        Protocol protocol = Protocols.ProtocolForPath(addresspath);
        // assert
        Assert.AreEqual(Protocol.ADDRESSABLE, protocol);
    }
}
