using WebSocketSharp;
using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;


public class WebSocketClient : MonoBehaviour
{
    WebSocket ws;
    public string nickname = "rafa";

    private bool connecting = true;
    private bool isConnected = false;
    private float connectTimeout = 0;
    private bool playerCreated = false;
    private long roomRefreshTimeout = 0;
    private long defaultServerTimeout = 2000;
    private Stopwatch sw;

    private List<Room> displayedRooms = new ArrayList<Room>();

    void Start()
    {
        sw = new Stopwatch();
        ws = new WebSocket("ws://localhost:8080");

        ws.OnMessage += (sender, e) =>
        {
            switch(e.Data[0]){
                case 0:
                case 1: if(e.Data == "11") {
                    Debug.Log("player created successfully");
                    playerCreated = true;
                    connecting = false;
                }
                case 2:
                case 3: 
                case 4:
                case 5:
                case 6:

                case 7:
                case 8: 
                    if(e.Data == "81") {
                        Debug.Log("Connection Successful!");
                        isConnected = true;
                        connecting = false;
                     }
                    break;
            }
            
        };
        connectToServer();
    }

    void OnDestroy()
    {
        ws.Close();
    }
    void Update(){
        if(sw > defaultServerTimeout){
            Debug.Log("Server timeout!");
            isConnected = false;
            playerCreated = false;
            connecting = true;
            sw.Stop();
        }
        float millis = Time.unscaledTime * 1000f;

        if(connecting){
            if(millis-connectTimeout>500){ //general connection
                ws.Connect();
                connectTimeout = millis;
            }
        }
        if(isConnected){
                if(nickname != ""){ // player creation
                    ws.Send(showName());
                    sw.Restart();
                }
            }
        if(playerCreated){
            if(millis-roomRefreshTimeout>3000){ //room scan
                ws.Send(getRooms(0));
                sw.Restart();
            }
        }

    }
    private string createRoom(string name, string password, int id){
        return "2"+name+":"+password;
    }
    private string joinRoom(string name, string id){
        return "3"+name+":"+id;
    }
    private string leaveRoom(){
        return "4";
    }
    private string changeRoom(string name, string password){
        return "5"+name+":"+password;
    }
    private string sendCoords(float x, float y, float vx, float vy){
        return "6"+x+":"+y+":"+vx+":"+vy+":"+nickname;
    }
    private string getRooms(int page){
        return "0"+page;
    }
    private string showName(){
        return "1"+nickname;
    }
}

