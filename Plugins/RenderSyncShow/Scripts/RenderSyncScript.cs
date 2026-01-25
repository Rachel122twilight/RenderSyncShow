using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR

public class RenderSyncScript : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    public static void StartSync()
    {
        if (EditorPrefs.GetString("RenderSyncShow_Address") == "" || EditorPrefs.GetString("RenderSyncShow_Port") == "")
        {
            EditorPrefs.SetString("RenderSyncShow_Address", "127.0.0.1");
            EditorPrefs.SetString("RenderSyncShow_Port", "8080");
        }
        var go = new GameObject("RenderSyncRunner");
        DontDestroyOnLoad(go);
        var runner = go.AddComponent<RenderSyncScript>();
        var texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
        var server = go.AddComponent<RenderSyncServerScript>();
        server.StreamTexture(texture);
        runner.StartCoroutine(runner.UpdateImage(server, texture));
        server.StartRenderSync();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        OnDestroy();
    }

    public IEnumerator UpdateImage(RenderSyncServerScript server, Texture2D texture)
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (texture == null || texture.width != Screen.width || texture.height != Screen.height)
            {
                if (texture != null)
                {
                    Destroy(texture);
                }
                texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGBA32, false);
            }
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();
            server.StreamTexture(texture);
        }
    }

    static void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //public void RenderSettings()
    //{
    // var rendersettings = RenderSettings.current;
    // rendersettings.SetRenderSyncEnabled(true);
    // rendersettings.SetRenderSyncInterval(1.0f / 60.0f);
    // rendersettings.SetRenderSyncMaxPacketSize(1024 * 1024);
    // rendersettings.SetRenderSyncMaxFPS(60);
    // rendersettings.SetRenderSyncMaxPacketCount(10);
    // rendersettings.SetRenderSyncMaxPacketRate(1000000);
    // rendersettings.SetRenderSyncMaxPacketRateInterval(1.0f);
    // rendersettings.SetRenderSyncMaxPacketRateBurst(1000000);
    // rendersettings.SetRenderSyncMaxPacketRateBurstInterval(1.0f);
    // rendersettings.SetRenderSyncMaxPacketRateBurstSize(1024 * 1024);
    // rendersettings.SetRenderSyncMaxPacketRateBurstDuration(1.0f);
    // rendersettings.SetRenderSyncMaxPacketRateBurstDelay(1.0f);
    // rendersettings.SetRenderSyncMaxPacketRateBurstJitter(1.0f);
    // rendersettings.SetRenderSyncMaxPacketRateBurstJitterInterval(1.0f);
    // rendersettings.SetRenderSyncMaxPacketRateBurstJitterSize(1024 * 1024);
    // rendersettings.SetRenderSyncMaxPacketRateBurstJitterDuration(1.0f);
    // rendersettings.SetRenderSyncMaxPacketRateBurstJitterDelay(1.0f);
    // rendersettings.SetGraphicsAPI(SystemInfo.graphicsDeviceType);
    // rendersettings.SetShaderCompiler(SystemInfo.graphicsShaderLevel);
    // rendersettings.SetMultiThreadedRendering(SystemInfo.graphicsMultiThreaded);
    // rendersettings.SetQuality(QualitySettings.GetQualityLevel());
    // rendersettings.SetVSync(QualitySettings.vSyncCount);
    // rendersettings.SetShaderQuality(Shader.globalMaximumLOD);
    // rendersettings.SetShadowQuality(QualitySettings.shadows);
    // rendersettings.SetAnisotropicFiltering(QualitySettings.anisotropicFiltering);
    // rendersettings.SetAntiAliasing(QualitySettings.antiAliasing);
    // rendersettings.SetColorSpace(QualitySettings.activeColorSpace);
    // rendersettings.SetScreenResolution(Screen.width, Screen.height);
    // rendersettings.SetCameraClearFlags(Camera.main.clearFlags);
    // rendersettings.SetCameraBackgroundColor(Camera.main.backgroundColor);
    // rendersettings.SetCameraFieldOfView(Camera.main.fieldOfView);
    // rendersettings.SetCameraNearClipPlane(Camera.main.nearClipPlane);
    // rendersettings.SetCameraFarClipPlane(Camera.main.farClipPlane);
    // rendersettings.SetCameraOrthographicSize(Camera.main.orthographicSize);
    // rendersettings.SetCameraDepth(Camera.main.depth);
    // rendersettings.SetCameraCullingMask(Camera.main.cullingMask);
    // rendersettings.SetCameraRenderingPath(Camera.main.renderingPath);
    // rendersettings.SetCameraHDR(Camera.main.allowHDR);
    // rendersettings.SetCameraRenderMode(Camera.main.renderingPath);
    // rendersettings.SetCameraCullingMask(Camera.main.cullingMask);
    // rendersettings.SetCameraLayerCullDistances(Camera.main.layerCullDistances);
    // rendersettings.SetCameraLayerCullSpherical(Camera.main.layerCullSpherical);
    // rendersettings.SetCameraDepthTextureMode(Camera.main.depthTextureMode);
    // rendersettings.SetCameraOcclusionCulling(Camera.main.useOcclusionCulling);
    // rendersettings.SetCameraOpaqueSortMode(Camera.main.opaqueSortMode);
    // rendersettings.SetCameraTransparencySortAxis(Camera.main.transparencySortAxis);
    // rendersettings.SetCameraTargetTexture(Camera.main.targetTexture);
    // rendersettings.SetCameraTargetDisplay(Camera.main.targetDisplay);
    // rendersettings.SetCameraWorldToCameraMatrix(Camera.main.worldToCameraMatrix);
    // rendersettings.SetCameraProjectionMatrix(Camera.main.projectionMatrix);
    // rendersettings.SetCameraViewMatrix(Camera.main.worldToCameraMatrix);
    // rendersettings.SetCameraViewProjectionMatrix(Camera.main.projectionMatrix * Camera.main.worldToCameraMatrix);
    // rendersettings.SetCameraViewportRect(Camera.main.pixelRect);
    // rendersettings.SetCameraViewportSize(Camera.main.pixelWidth, Camera.main.pixelHeight);
    // rendersettings.SetCameraScreenToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
    // rendersettings.SetCameraScreenToViewportPoint(new Vector3(0.5f, 0.5f, 0.0f));
    // rendersettings.SetCameraViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
    // rendersettings.SetCameraWorldToScreenPoint(new Vector3(0.0f, 0.0f, 0.0f));
    // rendersettings.SetCameraViewportToScreenPoint(new Vector3(0.0f, 0.0f, 0.0f));
    // rendersettings.SetCameraScreenToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
    //}
}

#endif