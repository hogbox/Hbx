using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using Hbx.Assets;

public class JsonLoaderUnitTests
{
    [Test]
    public async Task CanReadJsonString()
    {
        // arrange
        string jsonstring = UnitTestData.MyJsonClassString;
        JsonLoader jsonLoader = new JsonLoader();

        // act
        ILoaderResult<MyJsonClass> jsonobj = await jsonLoader.ReadAsync<MyJsonClass>(jsonstring, null);

        // assert
        Assert.AreEqual(true, jsonobj.valid);
        Assert.AreEqual(true, jsonobj.data.Equals(UnitTestData.MyJsonClassInstance));
    }

    [Test]
    public async Task CanReadJsonProtocolString()
    {
        // arrange
        string jsonstring = UnitTestData.MyJsonClassProtocolString;
        
        JsonLoader jsonLoader = new JsonLoader();

        // act
        ILoaderResult<MyJsonClass> jsonobj = await jsonLoader.ReadAsync<MyJsonClass>(jsonstring, null);

        // assert
        Assert.AreEqual(true, jsonobj.valid);
        Assert.AreEqual(true, jsonobj.data.Equals(UnitTestData.MyJsonClassInstance));
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
}
