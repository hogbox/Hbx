using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using Hbx.Assets;

public class DiskByteLoaderUnitTests
{
    [Test]
    public async Task CanLoadSimpleDiskFile()
    {
        // arrange
        string filepath = Path.Combine(Application.streamingAssetsPath, "example.json");
        DiskByteLoader diskLoader = new DiskByteLoader();

        // act
        BytesResult bytes = await diskLoader.read(filepath, null);

        // assert
        Assert.AreEqual(true, bytes.valid);
    }

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
