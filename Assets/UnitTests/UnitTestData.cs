using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitTestData
{
    public static string UnitTestDataFolderName = "unittest";
    public static string UnitTestDataPath = Path.Join(Application.streamingAssetsPath, UnitTestDataFolderName);
    public static string UnitTestDataUrl = "https://hogbox.github.io/Hbx/Assets/StreamingAssets/" + UnitTestDataFolderName;

    public static string MyJsonClassFilePath = Path.Join(UnitTestDataPath, "myjsonclass.json");
    public static string MyJsonClassFileUrl = Path.Join(UnitTestDataUrl, "myjsonclass.json");

    public static string MyHelloTextFilePath = Path.Join(UnitTestDataPath, "hello.txt");
    public static string MyHelloTextFileUrl = Path.Join(UnitTestDataUrl, "hello.txt");
    public static string MyHelloTextString = "hello";
    public static byte[] MyHelloTextBytes = System.Text.Encoding.UTF8.GetBytes(MyHelloTextString);

    public static string HbxTinyJpegPath = Path.Join(UnitTestDataPath, "hbx-tiny.jpg");
    public static string HbxTinyJpegPathUrl = Path.Join(UnitTestDataUrl, "hbx-tiny.jpg");

    public static string HbxTinyPngPath = Path.Join(UnitTestDataPath, "hbx-tiny.png");
    public static string HbxTinyPngPathUrl = Path.Join(UnitTestDataUrl, "hbx-tiny.png");
}
