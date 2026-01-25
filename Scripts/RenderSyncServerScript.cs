using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Threading;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR

public class RenderSyncServerScript : MonoBehaviour
{
    private HttpListener listener;
    private Texture2D textureToSend;
    private Thread serverThread;
    private ConcurrentQueue<HttpListenerContext> requestQueue = new ConcurrentQueue<HttpListenerContext>();
    public void StartRenderSync()
    {
        if (textureToSend == null)
        {
            Debug.LogError("<color=blue>[RenderSyncShow]</color> Texture not found");
            return;
        }
        StartServer(EditorPrefs.GetString("RenderSyncShow_Address", "127.0.0.1"), EditorPrefs.GetString("RenderSyncShow_Port", "8080"));
    }
    public void StreamTexture(Texture2D img)
    {
        textureToSend = img;
    }
    void StartServer(string address, string port)
    {
        serverThread = new Thread(() => RunServer(address, port));
        serverThread.Start();
    }
    void StopServer()
    {
        if (listener != null)
        {
            listener.Stop();
            listener.Close();
        }
        serverThread.Join();
    }
    void RunServer(string address, string port)
    {
        try
        {
            listener = new HttpListener();
            string link = "http://" + address + ":" + port + "/";
            listener.Prefixes.Add(link);
            listener.Start();
            Debug.Log("<color=blue>[RenderSyncShow]</color> Server started on " + link + ".");
            while (listener.IsListening)
            {
                HttpListenerContext context = listener.GetContext();
                requestQueue.Enqueue(context);
            }
        }
        catch (HttpListenerException e)
        {
            if (e.ErrorCode == 5)
            {
                Debug.Log("<color=blue>[RenderSyncShow]</color> Error: Port " + EditorPrefs.GetString("RenderSyncShow_Port", "8080") + " is already in use. Please try a different port or stop the application using this port.");
            }
        }
    }
    void Update()
    {
        if (requestQueue.TryDequeue(out HttpListenerContext context))
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            if (request.Url.AbsolutePath == "/texture")
            {
                byte[] textureBytes = textureToSend.EncodeToPNG();
                response.ContentType = "image/png";
                response.ContentLength64 = textureBytes.Length;
                Stream output = response.OutputStream;
                output.Write(textureBytes, 0, textureBytes.Length);
                output.Close();
            }
        }
    }
    public void OnApplicationQuit()
    {
        StopServer();
    }
}

#endif