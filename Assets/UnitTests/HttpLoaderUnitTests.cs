using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;


using Hbx.Assets;

public class HttpLoaderUnitTests
{
    [Test]
    public async Task CanReadHttpFileAsBytes()
    {
        // arrange
        string url = UnitTestData.MyHelloTextFileUrl;
        HttpLoader httpLoader = new HttpLoader();

        // act
        ILoaderResult<byte[]> byteresult = await httpLoader.ReadAsync<byte[]>(url, null);

        // assert
        Assert.AreEqual(true, byteresult.valid);
        Assert.AreEqual(true, byteresult.data.SequenceEqual(UnitTestData.MyHelloTextBytes));
    }

    [Test]
    public async Task CanReadHttpFileAsText()
    {
        // arrange
        string url = UnitTestData.MyHelloTextFileUrl;
        HttpLoader httpLoader = new HttpLoader();

        // act
        ILoaderResult<string> strresult = await httpLoader.ReadAsync<string>(url, null);

        // assert
        Assert.AreEqual(true, strresult.valid);
        Assert.AreEqual(UnitTestData.MyHelloTextString, strresult.data);
    }

    [Test]
    public async Task CanReadHttpFileAsTexture2D()
    {
        // arrange
        string url = UnitTestData.HbxTinyJpegPathUrl;
        HttpLoader httpLoader = new HttpLoader();

        // act
        ILoaderResult<Texture2D> texresult = await httpLoader.ReadAsync<Texture2D>(url, null);

        // assert
        Assert.AreEqual(true, texresult.valid);
        Assert.AreEqual(64, texresult.data.width);
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
