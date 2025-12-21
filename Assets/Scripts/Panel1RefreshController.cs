using UnityEngine;

public class Panel1RefreshController : MonoBehaviour
{
    public WebSocketClient ws;

    void Awake(){
        ws = FindObjectOfType<WebSocketClient>();
    }

    public void OnClick(){
        ws.refreshMyActiveRooms();
    }
}
