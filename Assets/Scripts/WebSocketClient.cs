using WebSocketSharp;
using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;



public class WebSocketClient : MonoBehaviour
{
    WebSocket ws;
    public string nickname = "rafa";

    private bool connecting = false;
    private bool isConnected = false;
    private float connectTimeout = 0;
    private bool playerCreated = false;
    private float roomRefreshTimeout = 0;
    private long defaultServerTimeout = 2000;
    private Stopwatch sw;

    private List<Room> displayedServerRooms = new List<Room>();
    private List<Room> displayedMyOfflineRooms = new List<Room>();
    private List<Room> displayedMyOnlineRooms = new List<Room>();



    void Start()
    {
        sw = new Stopwatch();
        ws = new WebSocket("ws://localhost:8080");

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Received"+e.Data);
            switch(e.Data.Substring(0,1)){
                case "0": break;
                case "1": if(e.Data == "11") {
                    Debug.Log("player created successfully");
                    playerCreated = true;
                    isConnected = false;
                    connecting = false;
                }
                break;
                case "2": if(e.Data.Substring(0,2)=="21"){

                }
                break;
                case "3": break;
                case "4": break;
                case "5": break;
                case "6": break;

                case "7": break;
                case "8": 
                    if(e.Data.Substring(0,2) == "81") {
                        Debug.Log("Connection Successful!"+e.Data.Substring(2));
                        isConnected = true;
                        connecting = false;
                     }
                    break;
            }
            
        };
    }

    void OnDestroy()
    {
        ws.Close();
    }
    void Update(){
        if(sw.ElapsedMilliseconds > defaultServerTimeout){
            Debug.Log("Server timeout!");
            isConnected = false;
            playerCreated = false;
            connecting = true;
            sw.Stop();
            sw.Reset();
        }
        float millis = Time.unscaledTime * 1000f;

        if(connecting){
            if(millis-connectTimeout>1500){
                Debug.Log("connecting to server"); //general connection
                ws.Connect();
                connectTimeout = millis;
            }
        }
        if(isConnected){
                Debug.Log("creating player"); // player creation
                ws.Send(showName());
                sw.Restart();
            }
        if(playerCreated){
            if(millis-roomRefreshTimeout>3000){
                Debug.Log("Requesting rooms"); //room scan
                ws.Send(getRooms(0));
                roomRefreshTimeout=millis;
                sw.Restart();
            }
        }

    }

    public void openMultiplayer(){
        //connecting = true;
    }
    public void closeMultiplayer(){
        connecting = false;
        isConnected = false;
        playerCreated = false;
        //ws.Close();
    }
    public void loadSavedRooms(){

    }
    public void publishRoom(){

    }
    public void displayOfflineRooms(){
        
    }
    public void displayMyActiveRooms(){

    }
    public void displayServerActiveRooms(){

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
    private string getMyActiveRooms(){
        return "";
    }
}

