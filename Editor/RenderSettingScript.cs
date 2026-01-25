using UnityEditor;
using UnityEngine;

public class RenderSyncShow : EditorWindow
{
    public string address;
    public string port;
    public bool isrunning = false;
    public Vector2 Vector2 = new Vector2(0, 0);

    [MenuItem("RenderSyncShow/Settings")]
    public static void ShowWindow()
    {
        CreateInstance<RenderSyncShow>().Show();
    }

    public void OnEnable()
    {
        address = EditorPrefs.GetString("RenderSyncShow_Address", "127.0.0.1");
        port = EditorPrefs.GetString("RenderSyncShow_Port", "8080");
    }
    public void Update()
    {
        EditorPrefs.SetString("RenderSyncShow_Address", address);
        EditorPrefs.SetString("RenderSyncShow_Port", port);
        isrunning = EditorPrefs.GetBool("RenderSyncShow Server Running", false);
    }

    public void OnGUI()
    {
        EditorGUILayout.BeginScrollView(Vector2);
        EditorGUILayout.LabelField("RenderSyncShow", EditorStyles.largeLabel);
        EditorGUILayout.LabelField("Version: 1.0.0");
        EditorGUILayout.LabelField("Made by 碳酸苏打Tasuda");
        EditorGUILayout.Space();
        if (Application.isPlaying)
        {
            if (isrunning)
            {
                EditorGUILayout.LabelField("Now Running");
            }
            else
            {
                EditorGUILayout.LabelField("Not running");
            }
        }
        address = EditorGUILayout.TextField("Address", address);
        port = EditorGUILayout.TextField("Port", port);
        EditorGUILayout.EndScrollView();
    }
}
