using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class IconGenerator : MonoBehaviour
{
    [SerializeField] private string prefix;
    [SerializeField] private string pathFolder;

    private Camera cam;

    [ContextMenu("Take Screenshot")]
    public void Screenshot()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Assets", pathFolder, prefix);
        path = Path.ChangeExtension(path, "png");
        TakeScreenshot(path);
    }

    private void Awake()
    {
        cam = Camera.main;
    }

    void TakeScreenshot(string path)
    {
        var renderTexture = new RenderTexture(256, 256, 24);
        cam.targetTexture = renderTexture;

        var screenshot = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        cam.Render();
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, 256, 256), 0, 0);

        cam.targetTexture = null;
        RenderTexture.active = null;

        if (Application.isEditor)
        {
            DestroyImmediate(renderTexture);
        }
        else
        {
            Destroy(renderTexture);
        }

        var bytes = screenshot.EncodeToPNG();
        File.WriteAllBytes(path, bytes);

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
