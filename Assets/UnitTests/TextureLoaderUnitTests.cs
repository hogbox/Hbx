using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


using Hbx.Assets;

public class TextureLoaderUnitTests
{
    //
    // Local File System
    //

    [Test]
    public async Task CanReadDDSTextureBytes()
    {
        // arrange
        byte[] filebytes = await File.ReadAllBytesAsync(UnitTestData.HbxTinyDdsPath);
        TextureLoader texloader = new TextureLoader();

        // act
        ILoaderResult<Texture2D> texresult = await texloader.ReadAsync<Texture2D>(filebytes, null);

        // assert
        Assert.AreEqual(true, texresult.valid);
        Assert.AreEqual(64, texresult.data.width);
    }

    [Test]
    public async Task CanReadAllDDSTextures()
    {
        // arrange
        string[] ddsfiles = Directory.GetFiles(UnitTestData.UnitTestDDSPath, "*.dds");

        int validcount = 0;
        foreach (string ddsfile in ddsfiles)
        {
            byte[] filebytes = await File.ReadAllBytesAsync(ddsfile);
            TextureLoader texloader = new TextureLoader();

            // act
            ILoaderResult<Texture2D> texresult = await texloader.ReadAsync<Texture2D>(filebytes, null);

            // assert
            Assert.AreEqual(true, texresult.valid || texresult.error != null);
            if (texresult.valid) Assert.AreEqual(64, texresult.data.width);
            Debug.Log(Path.GetFileName(ddsfile) + " - " + (texresult.valid ? "success" : (texresult.error != null ? texresult.error : "unkown error")));
        }
        Debug.Log(validcount + " of " + ddsfiles.Length + " loaded successfully.");
    }


    [Test]
    public async Task CanReadPvrTextureBytes()
    {
        // arrange
        byte[] filebytes = await File.ReadAllBytesAsync(UnitTestData.HbxTinyPvrPath);
        TextureLoader texloader = new TextureLoader();

        // act
        ILoaderResult<Texture2D> texresult = await texloader.ReadAsync<Texture2D>(filebytes, null);

        // assert
        Assert.AreEqual(true, texresult.valid);
        Assert.AreEqual(64, texresult.data.width);
    }

    [Test]
    public async Task CanReadPngTextureBytes()
    {
        // arrange
        byte[] filebytes = await File.ReadAllBytesAsync(UnitTestData.HbxTinyPngPath);
        TextureLoader texloader = new TextureLoader();

        // act
        ILoaderResult<Texture2D> texresult = await texloader.ReadAsync<Texture2D>(filebytes, null);

        // assert
        Assert.AreEqual(true, texresult.valid);
        Assert.AreEqual(64, texresult.data.width);
    }

    [Test]
    public async Task CanReadJpegTextureBytes()
    {
        // arrange
        byte[] filebytes = await File.ReadAllBytesAsync(UnitTestData.HbxTinyJpegPath);
        TextureLoader texloader = new TextureLoader();

        // act
        ILoaderResult<Texture2D> texresult = await texloader.ReadAsync<Texture2D>(filebytes, null);

        // assert
        Assert.AreEqual(true, texresult.valid);
        Assert.AreEqual(64, texresult.data.width);
    }

    [UnityTest]
    public IEnumerator CanDrawLoadedDDS()
    {
        // arrange
        Task<byte[]> filebytesTask = File.ReadAllBytesAsync(UnitTestData.HbxTinyDdsPath);
        yield return new WaitUntil(() => filebytesTask.IsCompleted);
        byte[] filebytes = filebytesTask.Result;



        TextureLoader texloader = new TextureLoader();

        // act
        Task<ILoaderResult<Texture2D>> texTask = texloader.ReadAsync<Texture2D>(filebytes, null);
        yield return new WaitUntil(() => texTask.IsCompleted);
        ILoaderResult<Texture2D> texresult = texTask.Result;

        RenderTexture rt = null;
        Graphics.Blit(texresult.data, rt);

        yield return new WaitForSeconds(1.0f);

        // assert
        Assert.AreEqual(true, texresult.valid);
        Assert.AreEqual(64, texresult.data.width);
        yield break;
    }
}
