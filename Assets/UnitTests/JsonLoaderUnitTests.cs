using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using Hbx.Assets;

public class MyJsonClass
{
    public string type;
    public int[] simplelist;
}

public class JsonLoaderUnitTests
{
    [Test]
    public async Task CanReadJsonString()
    {
        // arrange
        string jsonstring =
        @"{
            ""type"": ""MyJsonClass"",
            ""simplelist"": [1, 2, 3, 4]
        }";
        JsonLoader jsonLoader = new JsonLoader();

        // act
        ILoaderResult<MyJsonClass> jsonobj = await jsonLoader.ReadAsync<MyJsonClass>(jsonstring, null);

        // assert
        Assert.AreEqual(true, jsonobj.valid);
    }

    [Test]
    public async Task CanReadJsonProtocolString()
    {
        // arrange
        string jsonstring =
        @"json://{
            ""type"": ""MyJsonClass"",
            ""simplelist"": [1, 2, 3, 4]
        }";
        JsonLoader jsonLoader = new JsonLoader();

        // act
        ILoaderResult<MyJsonClass> jsonobj = await jsonLoader.ReadAsync<MyJsonClass>(jsonstring, null);

        // assert
        Assert.AreEqual(true, jsonobj.valid);
    }

    [Test]
    public async Task CanHandleInvalidJsonString()
    {
        // arrange
        string jsonstring = "{,";
        JsonLoader jsonLoader = new JsonLoader();

        // act
        ILoaderResult<MyJsonClass> jsonobj = await jsonLoader.ReadAsync<MyJsonClass>(jsonstring, null);

        // assert
        Assert.AreEqual(false, jsonobj.valid);
    }

    /*
    [Test]
    public async Task ReturnsInvalidBytesForMissingFile()
    {
        // arrange
        string filepath = Path.Combine(Application.streamingAssetsPath, "nonexistantfile.json");
        DiskByteLoader diskLoader = new DiskByteLoader();

        // act
        BytesResult bytes = await diskLoader.read(filepath, null);

        // assert
        Assert.AreEqual(false, bytes.valid);
    }
    */

    /*
    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator DiskLoaderUnitTestsWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
    */
}
