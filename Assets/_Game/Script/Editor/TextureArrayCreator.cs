using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class TextureArrayCreator : EditorWindow
{
    [MenuItem("Tools/Create Texture2DArray")]
    public static void ShowWindow()
    {
        GetWindow<TextureArrayCreator>("Texture2DArray Creator");
    }

    // Danh sách texture nguồn
    List<Texture2D> textures = new List<Texture2D>();
    string saveFolder = "Assets/TextureArray";
    string fileName = "NewTextureArray";

    Vector2 scrollPos;

    void OnGUI()
    {
        EditorGUILayout.LabelField("🎨 Texture2DArray Creator", EditorStyles.boldLabel);
        // Khu vực kéo thả
        var dropArea = GUILayoutUtility.GetRect(0.0f, 100.0f, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Drag and drop same textures size in here", EditorStyles.helpBox);

        // Xử lý kéo thả
        Event evt = Event.current;
        if (evt.type == EventType.DragUpdated || evt.type == EventType.DragPerform)
        {
            if (dropArea.Contains(evt.mousePosition))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    foreach (var dragged in DragAndDrop.objectReferences)
                    {
                        if (dragged is Texture2D tex && !textures.Contains(tex))
                            textures.Add(tex);
                    }
                }
                Event.current.Use();
            }
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField($"🧩 {textures.Count} List of textures :", EditorStyles.boldLabel);

        // Hiển thị danh sách texture
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(150));
        for (int i = 0; i < textures.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            textures[i] = (Texture2D)EditorGUILayout.ObjectField(textures[i], typeof(Texture2D), false);
            if (GUILayout.Button("X", GUILayout.Width(25)))
            {
                textures.RemoveAt(i);
                break;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();

        // Nút xóa toàn bộ
        if (textures.Count > 0 && GUILayout.Button("🧹 Clear All"))
            textures.Clear();

        EditorGUILayout.Space(10);

        // Chọn thư mục lưu
        EditorGUILayout.LabelField("💾 Save Location", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.TextField(saveFolder, GUILayout.ExpandWidth(true));
        if (GUILayout.Button("📂Save Location", GUILayout.Width(120)))
        {
            string selectedPath = EditorUtility.OpenFolderPanel("Select folder", "Assets", "");
            if (!string.IsNullOrEmpty(selectedPath))
            {
                // Chuyển path tuyệt đối -> path tương đối trong Unity
                if (selectedPath.StartsWith(Application.dataPath))
                    saveFolder = "Assets" + selectedPath.Substring(Application.dataPath.Length);
                else
                    EditorUtility.DisplayDialog("Error", "Setlect folders in 'Assets'!", "OK");
            }
        }
        EditorGUILayout.EndHorizontal();

        fileName = EditorGUILayout.TextField("File name", fileName);

        EditorGUILayout.Space(10);
        GUI.enabled = textures.Count > 0;
        if (GUILayout.Button("✨ Create Texture2DArray", GUILayout.Height(30)))
        {
            CreateTextureArray();
        }
        GUI.enabled = true;
    }

    void CreateTextureArray()
    {
        if (textures == null || textures.Count == 0)
        {
            EditorUtility.DisplayDialog("Error", "Empty list texture!", "OK");
            return;
        }

        // Kiểm tra định dạng & kích thước
        var first = textures[0];
        int width = first.width;
        int height = first.height;
        TextureFormat format = first.format;
        bool mipmap = first.mipmapCount > 1;

        foreach (var tex in textures)
        {
            if (tex.width != width || tex.height != height)
            {
                EditorUtility.DisplayDialog("Error", $"All textures need same size ({width}x{height})", "OK");
                return;
            }
            if (tex.format != format)
            {
                Debug.LogWarning($"⚠ Texture '{tex.name}' have diffirent format ({tex.format}).");
            }
        }

        // Tạo array
        Texture2DArray texArray = new Texture2DArray(width, height, textures.Count, TextureFormat.RGBA32, mipmap);
        texArray.wrapMode = first.wrapMode;
        texArray.filterMode = first.filterMode;
        texArray.anisoLevel = first.anisoLevel;

        for (int i = 0; i < textures.Count; i++)
        {
            Texture2D tex = textures[i];
            tex = ConvertToReadable(tex);
            texArray.SetPixels(tex.GetPixels(0), i, 0);
        }
        texArray.Apply();

        // Tạo đường dẫn đầy đủ
        string assetPath = Path.Combine(saveFolder, fileName + ".asset").Replace("\\", "/");

        AssetDatabase.CreateAsset(texArray, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        EditorUtility.DisplayDialog("✅ Complete", $"Created Texture2DArray from {textures.Count} texture.\nSave to: {assetPath}", "OK");
    }

    Texture2D ConvertToReadable(Texture2D source)
    {
        RenderTexture rt = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGB32);
        Graphics.Blit(source, rt);
        RenderTexture.active = rt;

        Texture2D readableTex = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
        readableTex.ReadPixels(new Rect(0, 0, source.width, source.height), 0, 0);
        readableTex.Apply();

        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);
        return readableTex;
    }
}
