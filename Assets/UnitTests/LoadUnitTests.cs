using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;


using Hbx.Assets;

public class LoadUnitTests
{
    //
    // Local File System
    //

    [Test]
    public async Task CanReadBytesFromFile()
    {
        // arrange
        string filepath = UnitTestData.MyHelloTextFilePath;

        // act
        ILoaderResult<byte[]> bytesresult = await Load.Get.ReadAsync<byte[]>(filepath, null);

        // assert
        Assert.AreEqual(true, bytesresult.valid);
        Assert.AreEqual(true, bytesresult.data.SequenceEqual(UnitTestData.MyHelloTextBytes));
    }

    [Test]
    public async Task CanReadTextFromFile()
    {
        // arrange
        string filepath = UnitTestData.MyHelloTextFilePath;

        // act
        ILoaderResult<string> strresult = await Load.Get.ReadAsync<string>(filepath, null);

        // assert
        Assert.AreEqual(true, strresult.valid);
        Assert.AreEqual(UnitTestData.MyHelloTextString, strresult.data);
    }

    //
    // Json
    //

    [Test]
    public async Task CanReadJsonFile()
    {
        // arrange
        string filepath = UnitTestData.MyJsonClassFilePath;

        // act
        ILoaderResult<MyJsonClass> jsonobj = await Load.Get.ReadAsync<MyJsonClass>(filepath, null);

        // assert
        Assert.AreEqual(true, jsonobj.valid);
        Assert.AreEqual(true, jsonobj.data.Equals(UnitTestData.MyJsonClassInstance));
    }

    [Test]
    public async Task CanReadJsonProtocol()
    {
        // arrange
        string jsonstring = UnitTestData.MyJsonClassProtocolString;

        // act
        ILoaderResult<MyJsonClass> jsonobj = await Load.Get.ReadAsync<MyJsonClass>(jsonstring, null);

        // assert
        Assert.AreEqual(true, jsonobj.valid);
        Assert.AreEqual(true, jsonobj.data.Equals(UnitTestData.MyJsonClassInstance));
    }

    //
    // Http
    //

    [Test]
    public async Task CanReadHttpFileAsBytes()
    {
        // arrange
        string url = UnitTestData.MyHelloTextFileUrl;

        // act
        ILoaderResult<byte[]> byteresult = await Load.Get.ReadAsync<byte[]>(url, null);

        // assert
        Assert.AreEqual(true, byteresult.valid);
        Assert.AreEqual(true, byteresult.data.SequenceEqual(UnitTestData.MyHelloTextBytes));
    }

    [Test]
    public async Task CanReadHttpFileAsText()
    {
        // arrange
        string url = UnitTestData.MyHelloTextFileUrl;

        // act
        ILoaderResult<string> strresult = await Load.Get.ReadAsync<string>(url, null);

        // assert
        Assert.AreEqual(true, strresult.valid);
        Assert.AreEqual(UnitTestData.MyHelloTextString, strresult.data);
    }

    [Test]
    public async Task CanReadHttpJsonFile()
    {
        // arrange
        string filepath = UnitTestData.MyJsonClassFileUrl;

        // act
        ILoaderResult<MyJsonClass> jsonobj = await Load.Get.ReadAsync<MyJsonClass>(filepath, null);

        // assert
        Assert.AreEqual(true, jsonobj.valid);
        Assert.AreEqual(true, jsonobj.data.Equals(UnitTestData.MyJsonClassInstance));
    }

    [Test]
    public async Task CanReadHttpTexture2D()
    {
        // arrange
        string url = UnitTestData.HbxTinyJpegPathUrl;

        // act
        ILoaderResult<Texture2D> texresult = await Load.Get.ReadAsync<Texture2D>(url, null);

        // assert
        Assert.AreEqual(true, texresult.valid);
        Assert.AreEqual(64, texresult.data.width);
    }

    //
    // Resource
    //

    [Test]
    public async Task CanLoadResourcesTexture2D()
    {
        // arrange
        // act
        ILoaderResult<Texture2D> texresult = await Load.Get.ReadAsync<Texture2D>("resources://unittest/hbx-tiny", null);

        // assert
        Assert.AreEqual(true, texresult.valid);
        Assert.AreEqual(64, texresult.data.width);
    }

    [Test]
    public async Task CanLoadResourcesTextAsset()
    {
        // arrange
        // act
        ILoaderResult<TextAsset> textresult = await Load.Get.ReadAsync<TextAsset>("resources://unittest/hello", null);

        // assert
        Assert.AreEqual(true, textresult.valid);
        Assert.AreEqual(UnitTestData.MyHelloTextString, textresult.data.text);
    }

    [Test]
    public async Task CanLoadResourcesTextAssetAsString()
    {
        // arrange
        // act
        ILoaderResult<string> textresult = await Load.Get.ReadAsync<string>("resources://unittest/hello", null);

        // assert
        Assert.AreEqual(true, textresult.valid);
        Assert.AreEqual(UnitTestData.MyHelloTextString, textresult.data);
    }

    [Test]
    public async Task CanLoadResourcesTextAssetAsBytes()
    {
        // arrange
        // act
        ILoaderResult<byte[]> textresult = await Load.Get.ReadAsync<byte[]>("resources://unittest/hello", null);

        // assert
        Assert.AreEqual(true, textresult.valid);
        Assert.AreEqual(true, textresult.data.SequenceEqual(UnitTestData.MyHelloTextBytes));
    }
}
