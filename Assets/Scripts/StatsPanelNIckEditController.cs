using UnityEngine;

public class StatsPanelNIckEditController : MonoBehaviour
{
    public WebSocketClient ws;
    void Awake(){
        ws = FindObjectOfType<WebSocketClient>();
    }
    public void OnClick(){
        ws.changeNick();
    }
}
