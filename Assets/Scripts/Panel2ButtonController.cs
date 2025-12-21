using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Panel2ButtonController : MonoBehaviour
{
    public multiplayerSaves sv;
    public WebSocketClient ws;

    void Awake(){
        ws = FindObjectOfType<WebSocketClient>();
    }

    public void OnPointerClick()
    {
        sv.hostRoom();
        Debug.Log("Before refreshing");
        ws.refreshMyActiveRooms();
        Debug.Log("After refreshing");
    }
}
