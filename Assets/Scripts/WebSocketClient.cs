using WebSocketSharp;
using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using TMPro;
using System;



public class WebSocketClient : MonoBehaviour
{
    WebSocket ws;
    public string nickname = "rafa";
    public connectWindowController cwc;

    private bool isConnected = false;
    bool isMultiplayer = false;
    private Stopwatch sw;
    float connectStart;
    public Saves sv;
    public feedbackController f;
    public GameObject fpanel;
    public ActiveRoomsController r;

    private List<Room> displayedServerRooms = new List<Room>();
    private List<Room> displayedMyOnlineRooms = new List<Room>();



    void Start()
    {
        sv = FindObjectOfType<Saves>();
        sw = new Stopwatch();
        ws = new WebSocket("ws://localhost:8080");
       

        ws.OnMessage += (sender, e) =>
        {
            Debug.Log("Received"+e.Data);
            switch(e.Data.Substring(0,1)){
                case "0": break;
                case "1": if(e.Data == "11") {
                    UnityMainThreadDispatcher.Instance().Enqueue(() => {
                        cwc.hide();
                    });
                    isConnected = true;
                    refreshMyActiveRooms();
                }
                break;
                case "2":
                    string msg = e.Data.Trim();
                    string[] parts = msg.Split(':');
                    if (parts.Length >= 3 && parts[0] == "21")
                        {
                           UnityMainThreadDispatcher.Instance().Enqueue(() => {
                        sv.changeUID(parts[1], parts[2]);
                    }); 
                     
                        }   
                else {
                    UnityMainThreadDispatcher.Instance().Enqueue(() => {
                        fpanel.SetActive(true);
                        f.showMessage(parts[1]);
                        });
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
                     }
                    break;
                case "9":
                    Debug.Log("received my rooms info");
                    clearActiveRoomList();
                    if(e.Data.Contains(";")){
                        string[] rooms = e.Data.Split(";");
                        foreach(string part in rooms){
                            Debug.Log("Part: '"+part+"'");
                        }
                    for(int i = 1; i < rooms.Length; i++){
                        string[] parts1 = rooms[i].Split(":");
                        Debug.Log("I"+i);
                        addRoomToMyOnlineList(int.Parse(parts1[3]), parts1[0], parts1[2], parts1[1]);
                        Debug.Log("I"+i); //count, name, id, pwd
                        
                    }
                    }
                    UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    r.refresh();
                });

                    break;
            }
            
        };
        ws.OnError += (sender, e) =>
        {
            Debug.Log("Encountered error!");
            if(!isConnected) {
                isConnected = true;
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    cwc.showRetry(e.Message);
                });
            } else {
                Debug.Log("asdf");
                UnityMainThreadDispatcher.Instance().Enqueue(() => {
                    fpanel.SetActive(true);
                    f.showMessage(e.Message);
                });
                
            }
            
        };
        ws.OnOpen += (sender, e) =>
        {
            ws.Send(showName());
        };

    }

    void OnDestroy()
    {
        ws.Close();
    }
    void Update(){
        if(isMultiplayer){
            if (!isConnected && Time.time - connectStart > 10f)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => {
                Debug.Log("Timeout");
                cwc.showRetry("Connection Timeout!");
            });
            
            ws.Close();
        }
        }

    }
    public void launchRoom(string name, string password, string uid, string id){
        ws.Send(createRoom(name, password, uid, id));
    }
    public void openMultiplayer(){
        isConnected = false;
        isMultiplayer = true;
        connectStart = Time.time;
        ws.ConnectAsync();
        UnityMainThreadDispatcher.Instance().Enqueue(() => {
            cwc.showConnecting();
        });
        
    }
    public void closeMultiplayer(){
        ws.Close();
        isMultiplayer = false;
    }
    public void refreshMyActiveRooms(){
        ws.Send(getMyActiveRooms());
    }
    void clearActiveRoomList(){
        displayedMyOnlineRooms.Clear();
    }
    void addRoomToMyOnlineList(int playerCount, string name, string id, string pwd){
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
{
    
    displayedMyOnlineRooms.Add(new Room(name, id, playerCount, pwd));
    Debug.Log("roomadd"); 
});

    }
    
    private string createRoom(string name, string password, string uid, string id){ //both launches and edits room
        return "2"+name+":"+password+":"+uid+":"+id;
    }
    private string joinRoom(string name, string id){
        return "3"+name+":"+id;
    }
    private string leaveRoom(){
        return "4";
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
        return "7";
    }
    public bool hasId(int id){
        Debug.Log(displayedMyOnlineRooms.Count + " " + id);
        if(displayedMyOnlineRooms.Count>id){
            return true;
        } else {
            return false;
        }
    }
    public Room getRoomFromMyRoomList(int id){
        return displayedMyOnlineRooms[id];
    }
}

