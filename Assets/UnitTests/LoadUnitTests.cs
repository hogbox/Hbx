using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;


using Hbx.Assets;

public class LoadUnitTests
{
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

    [Test]
    public async Task CanReadJsonFile()
    {
        // arrange
        string filepath = UnitTestData.MyJsonClassFilePath;

        // act
        ILoaderResult<MyJsonClass> jsonobj = await Load.Get.ReadAsync<MyJsonClass>(filepath, null);

        // assert
        Assert.AreEqual(true, jsonobj.valid);
    }

    [Test]
    public async Task CanReadJsonProtocol()
    {
        // arrange
        string jsonstring =
        @"json://{
            ""type"": ""MyJsonClass"",
            ""simplelist"": [1, 2, 3, 4]
        }";

        // act
        ILoaderResult<MyJsonClass> jsonobj = await Load.Get.ReadAsync<MyJsonClass>(jsonstring, null);

        // assert
        Assert.AreEqual(true, jsonobj.valid);
    }
}
