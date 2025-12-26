using UnityEngine;
using System.Collections.Generic;

public class OnlinePlayersController : MonoBehaviour
{
   
   public Dictionary<string, OnlinePlController> players = new Dictionary<string, OnlinePlController>();

   public GameObject prefab;

   public WebSocketClient ws;

    void Awake(){
        ws = FindObjectOfType<WebSocketClient>();
    }
    
    public void processStatus(float x, float y, string nick, string scene, string jcode, string hcode){
        OnlinePlController player = players[nick];
        if(player!=null){
            player.setCoords(x, y);
            player.toggleHitAnim(hcode);
            player.toggleJumpAnim(jcode);
        } else {
            Instantiate(prefab, new Vector2(1, 1), Quaternion.identity);
        }
    }
    void Update()
    {
        foreach(var pair in players){
            pair.Value.processMovement();
        }
    }
    public void checkPlayerList(){
        for(int i = 0; i < ws.CurrentRoomPlayerList.Count; i++){
            if(players.ContainsKey(ws.CurrentRoomPlayerList[i])){
                //good
            } else {
                //unnecessary player record
                Destroy(players[ws.CurrentRoomPlayerList[i]]);
                players.Remove(ws.CurrentRoomPlayerList[i]);
            }
        }
    }
}
