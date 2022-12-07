using System.IO;
using System.Linq;
using UnityEngine;


public class MyJsonClass
{
    public string type = null;
    public int[] simplelist = null;

    public bool Equals(MyJsonClass b)
    {
        return type == b.type && simplelist.SequenceEqual(b.simplelist);
    }
}

public static class UnitTestData
{
    public static string UnitTestDataFolderName = "unittest";
    public static string UnitTestDataPath = Path.Join(Application.streamingAssetsPath, UnitTestDataFolderName);
    public static string UnitTestDataUrl = "https://hogbox.github.io/Hbx/Assets/StreamingAssets/" + UnitTestDataFolderName;

    public static string UnitTestDDSPath = Path.Join(UnitTestDataPath, "dds");
    public static string UnitTestDDSUrl = Path.Join(UnitTestDataUrl, "dds");

    public static string UnitTestPvrPath = Path.Join(UnitTestDataPath, "pvr");
    public static string UnitTestPvrUrl = Path.Join(UnitTestDataUrl, "pvr");

    public static string MyHelloTextFilePath = Path.Join(UnitTestDataPath, "hello.txt");
    public static string MyHelloTextFileUrl = Path.Join(UnitTestDataUrl, "hello.txt");
    public static string MyHelloTextString = "hello";
    public static byte[] MyHelloTextBytes = System.Text.Encoding.UTF8.GetBytes(MyHelloTextString);

    public static string HbxTinyJpegPath = Path.Join(UnitTestDataPath, "hbx-tiny.jpg");
    public static string HbxTinyJpegPathUrl = Path.Join(UnitTestDataUrl, "hbx-tiny.jpg");

    public static string HbxTinyPngPath = Path.Join(UnitTestDataPath, "hbx-tiny.png");
    public static string HbxTinyPngPathUrl = Path.Join(UnitTestDataUrl, "hbx-tiny.png");

    public static string HbxTinyDdsPath = Path.Join(UnitTestDDSPath, "hbx-tiny_bc1_rgb_nomips.dds");
    public static string HbxTinyDdsPathUrl = Path.Join(UnitTestDDSUrl, "hbx-tiny_bc1_rgb_nomips.dds");

    //public static string HbxTinyPvrPath = Path.Join(UnitTestPvrPath, "hbx-tiny_pvrtc_2bpp_rgba_unorm_srgb.pvr");
    //public static string HbxTinyPvrPath = Path.Join(UnitTestPvrPath, "hbx-tiny_pvrtc_4bpp_rgba_unorm_srgb.pvr");
    public static string HbxTinyPvrPath = Path.Join(UnitTestPvrPath, "hbx-tiny_bc1_unorm_srgb.pvr");
    public static string HbxTinyPvrPathUrl = Path.Join(UnitTestPvrUrl, "hbx-tiny_pvrtc_2bpp_rgba_unorm_srgb.pvr");

    public static string MyJsonClassFilePath = Path.Join(UnitTestDataPath, "myjsonclass.json");
    public static string MyJsonClassFileUrl = Path.Join(UnitTestDataUrl, "myjsonclass.json");

    public static string MyJsonClassString = @"{""type"":""MyJsonClass"",""simplelist"":[1, 2, 3, 4]}";
    public static string MyJsonClassProtocolString = @"json://" + MyJsonClassString;
    public static MyJsonClass MyJsonClassInstance = new MyJsonClass() { type = "MyJsonClass", simplelist = new int[] { 1, 2, 3, 4 } };
}
