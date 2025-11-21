using WebSocketSharp;
using UnityEngine;

public class WebSocketClient : MonoBehaviour
{
    WebSocket ws;

    void Start()
    {
        ws = new WebSocket("ws://localhost:8080");

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Message from server: " + e.Data);
        };

        ws.Connect();
        ws.Send("Hello from Unity!");
    }

    void OnDestroy()
    {
        ws.Close();
    }
}

