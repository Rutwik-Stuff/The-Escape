using UnityEngine;
using NativeWebSocket;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class WebSocketClient : MonoBehaviour
{
    WebSocket ws;

    public string nickname = "";
    public connectWindowController cwc;

    private bool isConnected = false;
    public bool isMultiplayer = false;
    float connectStart;
    float sendTime;

    public Saves sv;
    public feedbackController f;
    public GameObject fpanel;
    public ActiveRoomsController r;
    public ServerRoomsController s;
    public StatsController stc;
    public pwdInputController pic;
    public MainLogic logic;
    public GameObject nicknamepanel;
    public Movement mv;

    public TMP_Text nick;
    public TMP_InputField nickInput;

    private List<Room> displayedServerRooms = new List<Room>();
    public List<Room> displayedMyOnlineRooms = new List<Room>();
    public List<string> CurrentRoomPlayerList = new List<string>();

    bool gotResponse = false;
    string[] lastResponse = null;

    private static WebSocketClient instance;

void Awake()
{
    if (instance != null && instance != this)
    {
        Destroy(gameObject);
        return;
    }

    instance = this;
    DontDestroyOnLoad(gameObject);
}
public void reAssignFields(){
    cwc = FindObjectOfType<connectWindowController>(true);
    f = FindObjectOfType<feedbackController>(true);
    r = FindObjectOfType<ActiveRoomsController>(true);
    s = FindObjectOfType<ServerRoomsController>(true);
    stc = FindObjectOfType<StatsController>(true);
    pic = FindObjectOfType<pwdInputController>(true);
    GameObject o = GameObject.Find("Nick");
    nick = o.GetComponent<TMP_Text>();
    nick.text = sv.getNickname();
    Transform oo = Resources.FindObjectsOfTypeAll<Transform>()
                            .FirstOrDefault(c => c.gameObject.name == "NicknameEdit");
    nicknamepanel = oo.gameObject;
    Transform ooo = Resources.FindObjectsOfTypeAll<Transform>()
                            .FirstOrDefault(c => c.gameObject.name == "NickNameINPUT");
    nickInput = ooo.gameObject.GetComponent<TMP_InputField>();
}


    IEnumerator PollServerLoop()
    {
        while (isConnected)
        {
            lastResponse = null;
            if(SceneManager.GetActiveScene().name == "Menu"){
                ws.SendText("9"); //for servers stats
            } else {
                ws.SendText("91"); //for server stats + current room stats
            }
            

            yield return new WaitForSecondsRealtime(3f);

            if (gotResponse && lastResponse != null)
            {
                gotResponse = false;
                if(stc != null){
                    stc.updateStats(lastResponse[2], lastResponse[1]);
                }
                
            }
            else
            {
                Debug.Log("shutting down");
                if(!(SceneManager.GetActiveScene().name == "Menu")){
                    logic.exitMode();
                } else {
                    closeMultiplayer();
                openMultiplayer();
                }
                
            }
        }
    }

    async void Start()
    {
        Debug.Log("WS started");
        sv = FindObjectOfType<Saves>();

        ws = new WebSocket("ws://192.168.4.100:8080");

        ws.OnMessage += (bytes) =>
        {
            string data = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received " + data);

            switch (data[0])
            {
                case '0':
                    refreshMyActiveRooms();
                    break;

                case '1':
                    if (data == "11")
                    {
                        cwc.hide();
                        isConnected = true;
                        refreshMyActiveRooms();
                    }
                    break;

                case '2':
                    string[] parts = data.Split(':');
                    if (parts.Length >= 3 && parts[0] == "21")
                        sv.changeUID(parts[1], parts[2]);
                    else
                    {
                        f.showMessage(parts[1]);
                    }
                    break;

                case '3':
                    if (data.StartsWith("3p"))
                        pic.showInputPassword(data.Substring(2));
                    else if (data.StartsWith("30"))
                        f.showMessage(data.Substring(2));
                    else if (data.StartsWith("31m"))
                        logic.launchSave(sv.getCurrentSaveId());
                        isMultiplayer = true;
                    break;
                case '5':
                break;
                case '6':
                    displayedServerRooms.Clear();
                    if (data.Contains(";"))
                    {
                        string[] rooms = data.Split(';');
                        for (int i = 1; i < rooms.Length; i++)
                        {
                            string[] p = rooms[i].Split(':');
                            displayedServerRooms.Add(
                                new Room(p[0], p[1], p[2], int.Parse(p[3]))
                            );
                        }
                    }
                    s.refresh();
                    break;

                case '7':
                    CurrentRoomPlayerList.Clear();
                    gotResponse = true;
                    lastResponse = data.Split(';');
                    Debug.Log(lastResponse[0]);
                    if(data.Contains(";")){
                        Debug.Log(lastResponse[1]);
                        string[] players = lastResponse[1].Split(":");
                    for(int i = 0; i < players.Length; i++){
                        if(players[i] != ""){
                            CurrentRoomPlayerList.Add(players[i]);
                        }
                        
                    }
                    }
                    
                    lastResponse = lastResponse[0].Split(':');
                    break;

                case '8':
                    if (data.StartsWith("81"))
                    {
                        isConnected = true;
                        StartCoroutine(PollServerLoop());
                    }
                    break;

                case '9':
                    displayedMyOnlineRooms.Clear();
                    if (data.Contains(";"))
                    {
                        string[] rooms = data.Split(';');
                        for (int i = 1; i < rooms.Length; i++)
                        {
                            string[] p = rooms[i].Split(':');
                            displayedMyOnlineRooms.Add(
                                new Room(p[0], p[2], int.Parse(p[3]), p[1])
                            );
                        }
                    }
                    r.refresh();
                    Debug.Log("List size"+ displayedMyOnlineRooms.Count);
                    break;
            }
        };

        ws.OnError += (e) =>
        {   if(SceneManager.GetActiveScene().name == "Menu"){
            cwc.showRetry(e);
        } else {
            logic.exitMode();
            cwc.showRetry(e);
        }
            
        };

        ws.OnOpen += () =>
        {
            ws.SendText(showName());
        };
        ws.OnClose += (WebSocketCloseCode code) =>{
            if(isMultiplayer){
                openMultiplayer();
                cwc.showConnecting();
            }
        };
    }

    void Update()
    {
        if(Time.time*1000-sendTime>70 && isMultiplayer){
            string name = SceneManager.GetActiveScene().name;
            if(CurrentRoomPlayerList.Count > 1 && name != "Menu"){
                mv = FindObjectOfType<Movement>();
                ws.SendText("6"+":"+name.Substring(3)+":"+mv.getX()+":"+mv.getY()+":"+mv.getHit()+":"+mv.isJump());
                Debug.Log(name.Substring(3)+":"+mv.getX()+":"+mv.getY()+":"+mv.getHit()+":"+mv.isJump());
            }
            sendTime = Time.time*1000;
        }
        

        ws?.DispatchMessageQueue();


    }

    async void OnDestroy()
    {
        if (ws != null)
            await ws.Close();
    }

    public async void openMultiplayer()
    {
        isConnected = false;
        isMultiplayer = true;
        connectStart = Time.time;
        cwc.showConnecting();
        if(sv.getNickname()==""){
            nickname = "Player"+Random.Range(1, 10000000);
            nick.text = nickname;
            sv.saveNick(nickname);
            Debug.Log("Generating Nickname");
        } else {
            Debug.Log("Setting Nickname");
            nick.text = sv.getNickname();
        }
        await ws.Connect();

        
    }
    public void changeNick(){
        nicknamepanel.SetActive(true);
    }
    public void onNickEdited(){
        sv.saveNick(nickInput.text);
        nick.text = nickInput.text;
        nicknamepanel.SetActive(false);
        ws.SendText(showName());
    }
    public async void closeMultiplayer()
    {
        Debug.Log("closing multiplayer");
        isMultiplayer = false;
        isConnected = false;
        if (ws != null)
            await ws.Close();
    }

    public void launchRoom(string name, string password, string uid, string id)
    {
        ws.SendText("2" + name + ":" + password + ":" + uid + ":" + id);
    }

    public void stopActivRoom(int id)
    {
        Debug.Log(displayedMyOnlineRooms.Count);
        ws.SendText("8" + displayedMyOnlineRooms[id].id);
    }

    public void refreshMyActiveRooms()
    {
        Debug.Log("Refreshing active rooms");
        ws.SendText("7");
    }

    public void refreshServerRooms()
    {
        ws.SendText("0" + s.getPage());
    }

    public bool hasId(int id) => id < displayedMyOnlineRooms.Count;
    public bool hasSId(int id) => id < displayedServerRooms.Count;

    public Room getRoomFromMyRoomList(int id) => displayedMyOnlineRooms[id];
    public Room getRoomFromServerList(int id) => displayedServerRooms[id];

    public void joinMyRoom(int id)
    {
        ws.SendText("3" + displayedMyOnlineRooms[id].id);
    }

    public void joinServerRoom(int id)
    {
        ws.SendText("3" + displayedServerRooms[id].id);
    }

    public void joinByAddress(string id, string pwd)
    {
        ws.SendText("3" + id+":"+pwd);
    }
    public void joinAddress(string id){
        ws.SendText("3"+id);
    }
    string showName() => "1" +  sv.getNickname();
    public void leaveRoom(){
        ws.SendText("4");
    }
}



