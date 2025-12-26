using UnityEngine;

public class RetryButtonController : MonoBehaviour
{
    public WebSocketClient ws;
    void Awake(){
        ws = FindObjectOfType<WebSocketClient>();
    }
    public void OnClick(){
        ws.openMultiplayer();
    }
}
