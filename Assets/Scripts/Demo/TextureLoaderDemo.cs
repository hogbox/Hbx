using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

using Hbx.Assets;

public class TextureViewQuad
{
    public Material material;
    public TMPro.TextMeshPro label;

    public TextureViewQuad(Material m, TMPro.TextMeshPro t)
    {
        material = m;
        label = t;
    }
}

public class TextureLoaderDemo : MonoBehaviour
{

    public static string DataFolderName = "unittest";
    public static string DataPath = Path.Join(Application.streamingAssetsPath, DataFolderName);
    public static string DataUrl = "https://hogbox.github.io/Hbx/Assets/StreamingAssets/" + DataFolderName;

    public static string DDSPath = Path.Join(DataPath, "dds");
    public static string PVRPath = Path.Join(DataPath, "pvr");

    public bool _loadDDS = true;

    // Start is called before the first frame update
    private IEnumerator Start()
    {
        float gridsize = 1000.0f;
        float aspect = Screen.width / Screen.height;
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = (aspect > 1.0f ? gridsize : gridsize * aspect) * 0.5f;

        string[] ddsfiles = _loadDDS ? Directory.GetFiles(DDSPath, "*.dds") : Directory.GetFiles(PVRPath, "*.pvr");

        int gridcellsx = Mathf.CeilToInt(Mathf.Sqrt((float)ddsfiles.Length));

        float cellsize = gridsize / gridcellsx;
        float quadsize = cellsize * 0.8f;

        Vector2 origin = new Vector2(-gridsize * 0.5f, -gridsize * 0.5f);
        Vector2 p = origin;
        foreach(string ddsfile in ddsfiles)
        {
            Task<ILoaderResult<Texture2D>> readtask = Load.Get.ReadAsync<Texture2D>(ddsfile, null);
            yield return new WaitUntil(() => readtask.IsCompleted);
            ILoaderResult<Texture2D> texresult = readtask.Result;

            TextureViewQuad q = CreateViewingQuad(p, quadsize);
            q.material.mainTexture = texresult.data;
            if(_loadDDS) q.material.mainTextureScale = new Vector2(1, -1);

            q.label.text = Path.GetFileNameWithoutExtension(ddsfile);// System.Enum.GetName(typeof(GraphicsFormat), texresult.data.format);

            p.x = p.x + cellsize;
            if(p.x >= gridsize * 0.5F)
            {
                p.x = origin.x;
                p.y = p.y + cellsize;
            }
        }
    }

    TextureViewQuad CreateViewingQuad(Vector2 offset, float size)
    {
        GameObject root = new GameObject("TextureQuad");
        root.transform.position = new Vector3(offset.x, offset.y, 12);

        float qsize = size * 0.8f;

        GameObject qo = GameObject.CreatePrimitive(PrimitiveType.Plane);
        qo.transform.SetParent(root.transform, false);
        qo.transform.rotation = Quaternion.Euler(90, 180, 0);
        qo.transform.localScale = new Vector3(qsize * 0.1f, 1.0f, qsize * 0.1f);
        qo.transform.localPosition = new Vector3(size * 0.5f, size * 0.5f, 0.0f);
        MeshRenderer meshRenderer = qo.GetComponent<MeshRenderer>();
        Material material = new Material(Shader.Find("Unlit/Texture"));
        meshRenderer.material = material;

        GameObject lo = new GameObject("label");
        lo.transform.SetParent(root.transform, false);
        TMPro.TextMeshPro text = lo.AddComponent<TMPro.TextMeshPro>();
        text.fontSize = 100;

        RectTransform trt = lo.transform as RectTransform;
        if(trt != null)
        {
            trt.sizeDelta = new Vector2(size, 50);
            trt.localPosition = new Vector3(size * 0.5f, 0, 0);
        }

        return new TextureViewQuad(material, text);
    }
}
