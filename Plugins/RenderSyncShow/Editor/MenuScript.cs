using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RenderSyncShowMenuScript : MonoBehaviour
{
    [MenuItem("RenderSyncShow/Start &s", false, 1)]
    public static void option1()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("<color=blue>[RenderSyncShow]</color> Please enter Play Mode to use this feature.");
        }
        else
        {
            if (FindObjectOfType<GameObject>().name == "RenderSyncRunner")
            {
                Debug.LogWarning("<color=blue>[RenderSyncShow]</color> RenderSyncShowServer is already running.");
            }
            else
            {
                RenderSyncScript.StartSync();
                Debug.Log("<color=blue>[RenderSyncShow]</color> Started.");
                EditorPrefs.SetBool("RenderSyncShow Server Running", true);
            }
        }
    }
    [MenuItem("RenderSyncShow/Stop &e", false, 2)]
    public static void option2()
    {
        if (Application.isPlaying == false)
        {
            Debug.LogWarning("<color=blue>[RenderSyncShow]</color> Please enter Play Mode to use this feature.");
        }
        else
        {
            if (FindObjectOfType<GameObject>().name != "RenderSyncRunner")
            {
                Debug.LogWarning("<color=blue>[RenderSyncShow]</color> RenderSyncShowServer is not running.");
            }
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (obj.name == "RenderSyncRunner")
                {
                    Debug.Log("<color=blue>[RenderSyncShow]</color> Stopping RenderSyncServer...");
                    obj.GetComponent<RenderSyncServerScript>().OnApplicationQuit();
                    Destroy(obj.GetComponent<RenderSyncServerScript>());
                    Destroy(obj.GetComponent<RenderSyncScript>());
                    Destroy(obj);
                    Debug.Log("<color=blue>[RenderSyncShow]</color> RenderSyncServer Stopped.");
                    EditorPrefs.GetBool("RenderSyncShow Server Running", false);
                }
            }
        }
    }
    [MenuItem("RenderSyncShow/Restart &r", false, 3)]
    public static void option3()
    {
        if (Application.isPlaying == false)
        {
            Debug.LogWarning("<color=blue>[RenderSyncShow]</color> Please enter Play Mode to use this feature.");
        }
        else
        {
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (obj.name == "RenderSyncRunner")
                {
                    obj.GetComponent<RenderSyncServerScript>().OnApplicationQuit();
                    Destroy(obj.GetComponent<RenderSyncServerScript>());
                    Destroy(obj.GetComponent<RenderSyncScript>());
                    Destroy(obj);
                }
            }
            RenderSyncScript.StartSync();
            Debug.Log("<color=blue>[RenderSyncShow]</color> Restarted.");
            EditorPrefs.SetBool("RenderSyncShow Server Running", true);
        }
    }
}
