using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;


using Hbx.Assets;

public class DiskLoaderUnitTests
{
    [Test]
    public async Task CanReadDiskFileAsBytes()
    {
        // arrange
        string filepath = UnitTestData.MyHelloTextFilePath;
        DiskLoader diskLoader = new DiskLoader();

        // act
        ILoaderResult<byte[]> byteresult = await diskLoader.ReadAsync<byte[]>(filepath, null);

        // assert
        Assert.AreEqual(true, byteresult.valid);
        Assert.AreEqual(true, byteresult.data.SequenceEqual(UnitTestData.MyHelloTextBytes));
    }

    [Test]
    public async Task CanReadDiskFileAsText()
    {
        // arrange
        string filepath = UnitTestData.MyHelloTextFilePath;
        DiskLoader diskLoader = new DiskLoader();

        // act
        ILoaderResult<string> strresult = await diskLoader.ReadAsync<string>(filepath, null);

        // assert
        Assert.AreEqual(true, strresult.valid);
        Assert.AreEqual(UnitTestData.MyHelloTextString, strresult.data);
    }

    [Test]
    public async Task ReturnsNullForMissingBytesFile()
    {
        // arrange
        string filepath = Path.Combine(Application.streamingAssetsPath, "nonexistantfile.json");
        DiskLoader diskLoader = new DiskLoader();

        // act
        ILoaderResult<byte[]> byteresult = await diskLoader.ReadAsync<byte[]>(filepath, null);

        // assert
        Assert.AreEqual(false, byteresult.valid);
        Assert.AreEqual(null, byteresult.data);
    }

    [Test]
    public async Task ReturnsNullForMissingTextFile()
    {
        // arrange
        string filepath = Path.Combine(Application.streamingAssetsPath, "nonexistantfile.json");
        DiskLoader diskLoader = new DiskLoader();

        // act
        ILoaderResult<string> strresult = await diskLoader.ReadAsync<string>(filepath, null);

        // assert
        Assert.AreEqual(false, strresult.valid);
        Assert.AreEqual(null, strresult.data);
    }

    [Test]
    public async Task CanHandleInvalidReadType()
    {
        // arrange
        string filepath = UnitTestData.MyHelloTextFilePath;
        DiskLoader diskLoader = new DiskLoader();

        // act
        ILoaderResult<int> intresult = await diskLoader.ReadAsync<int>(filepath, null);

        // assert
        Assert.AreEqual(false, intresult.valid);
        Assert.AreEqual(true, intresult.data == 0);
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
