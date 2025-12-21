using UnityEngine;
using TMPro;

public class PlayerEntry : MonoBehaviour
{
    public int id;

    public WebSocketClient ws;

    void Awake(){
        ws = FindObjectOfType<WebSocketClient>();
    }

    public void showIfNeeded(){
        if(ws.CurrentRoomPlayerList.Count>id){
            gameObject.SetActive(true);
            gameObject.GetComponent<TMP_Text>().text = ws.CurrentRoomPlayerList[id];
        } else {
            gameObject.SetActive(false);
        }
    }
}
