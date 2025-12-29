using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class OnlinePlayersController : MonoBehaviour
{
   
   public Dictionary<string, OnlinePlController> players = new Dictionary<string, OnlinePlController>();

   public GameObject prefab;
   public GameObject newInstance;

   public WebSocketClient ws;

    void Awake(){
        ws = FindObjectOfType<WebSocketClient>();
    }
    
    public void processStatus(float x, float y, string nick, string scene, string jcode, string hcode){
        if(scene == SceneManager.GetActiveScene().name.Substring(3)){
            OnlinePlController player;
        if(players.ContainsKey(nick)){
            player = players[nick];
            player.setCoords(x, y);
            player.toggleHitAnim(hcode);
            player.toggleJumpAnim(jcode);
        } else {
            Debug.Log("Instantiated");
            newInstance = Instantiate(prefab, new Vector2(1, 1), Quaternion.identity);
            players[nick] = newInstance.GetComponent<OnlinePlController>();
        }
        }
        
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "Menu"){
            foreach(var pair in players){
            pair.Value.processMovement();
        }
        }
        
    }
    public void checkPlayerList(){
List<string> toRemove = new List<string>();

    foreach (var pair in players)
    {
        bool match = false;

        for (int i = 0; i < ws.CurrentRoomPlayerList.Count; i++)
        {
            if (ws.CurrentRoomPlayerList[i] == pair.Key)
            {
                match = true;
                break;
            }
        }

        if (!match)
        {
            toRemove.Add(pair.Key);
        }
    }

    foreach (string key in toRemove)
    {
        Destroy(players[key].gameObject);
        players.Remove(key);
        Debug.Log("Removed " + key);
    }
        }
    public void clearPlayers(){
        players.Clear();
    }
    

}
