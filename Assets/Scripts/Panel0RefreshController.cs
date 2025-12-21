using UnityEngine;

public class Panel0RefreshController : MonoBehaviour
{
    public WebSocketClient ws;
    void Awake(){
        ws = FindObjectOfType<WebSocketClient>();
    }
    public void OnClick(){
        ws.refreshServerRooms();
    }
}
