using WebSocketSharp;
using UnityEngine;
using System.Diagnostics;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using TMPro;



public class WebSocketClient : MonoBehaviour
{
    WebSocket ws;
    public string nickname = "rafa";
    public feetback f;
    public connectWindowController cwc;

    private bool isConnected = false;
    bool isMultiplayer = false;
    private Stopwatch sw;
    float connectStart;
    public Saves sv;

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
                case "2": if(e.Data.Substring(0,2)=="21"){
                    string[] parts  = e.Data.Split(":");
                    sv.changeUID(parts[1], parts[2]);

                } else {
                    f.ShowFeedback("idkk");
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
                f.ShowFeedback(e.Message);
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
}

