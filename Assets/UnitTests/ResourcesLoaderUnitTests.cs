using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using Hbx.Assets;

public class ResourcesLoaderUnitTests
{
    [Test]
    public async Task CanLoadResourcesTexture2D()
    {
        // arrange
        ResourcesLoader resourceLoader = new ResourcesLoader();

        // act
        ILoaderResult<Texture2D> texresult = await resourceLoader.ReadAsync<Texture2D>("resources://unittest/hbx-tiny", null);

        // assert
        Assert.AreEqual(true, texresult.valid);
        Assert.AreEqual(64, texresult.data.width);
    }

    [Test]
    public async Task CanLoadResourcesTextAsset()
    {
        // arrange
        ResourcesLoader resourceLoader = new ResourcesLoader();

        // act
        ILoaderResult<TextAsset> textresult = await resourceLoader.ReadAsync<TextAsset>("resources://unittest/hello", null);

        // assert
        Assert.AreEqual(true, textresult.valid);
        Assert.AreEqual(UnitTestData.MyHelloTextString, textresult.data.text);
    }

    [Test]
    public async Task CanLoadResourcesTextAssetAsString()
    {
        // arrange
        ResourcesLoader resourceLoader = new ResourcesLoader();

        // act
        ILoaderResult<string> textresult = await resourceLoader.ReadAsync<string>("resources://unittest/hello", null);

        // assert
        Assert.AreEqual(true, textresult.valid);
        Assert.AreEqual(UnitTestData.MyHelloTextString, textresult.data);
    }

    [Test]
    public async Task CanLoadResourcesTextAssetAsBytes()
    {
        // arrange
        ResourcesLoader resourceLoader = new ResourcesLoader();

        // act
        ILoaderResult<byte[]> textresult = await resourceLoader.ReadAsync<byte[]>("resources://unittest/hello", null);

        // assert
        Assert.AreEqual(true, textresult.valid);
        Assert.AreEqual(true, textresult.data.SequenceEqual(UnitTestData.MyHelloTextBytes));
    }
}
