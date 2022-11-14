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
    public async Task CanReadSimpleJsonString()
    {
        // arrange
        string jsonstring =
        @"{
            ""type"": ""MyJsonClass"",
            ""simplelist"": [1, 2, 3, 4]
        }";
        JsonLoader<MyJsonClass> jsonLoader = new JsonLoader<MyJsonClass>();

        // act
        GenericResult<MyJsonClass> jsonobj = await jsonLoader.read(jsonstring, null);

        // assert
        Assert.AreEqual(true, jsonobj.valid);
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
