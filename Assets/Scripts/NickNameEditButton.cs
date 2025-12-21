using UnityEngine;

public class NickNameEditButton : MonoBehaviour
{
    public WebSocketClient ws;

    void Awake(){
        ws = FindObjectOfType<WebSocketClient>();
    }

    public void OnClick(){
        ws.onNickEdited();
    }
}
