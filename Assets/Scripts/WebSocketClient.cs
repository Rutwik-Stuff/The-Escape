using WebSocketSharp;
using UnityEngine;

public class WebSocketClient : MonoBehaviour
{
    WebSocket ws;
    public string nickname = "rafa";

    void Start()
    {
        ws = new WebSocket("ws://localhost:8080");

        ws.OnMessage += (sender, e) =>
        {
            if(e.Data == "81"){
                Debug.Log("received data " + e.Data);
                ws.Send("1"+nickname);
            }
            
        };
        connectToServer();
    }

    void OnDestroy()
    {
        ws.Close();
    }
    void connectToServer(){
        ws.Connect();
        
    }
}

