using UnityEngine;
using NativeWebSocket;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class WebSocketClient : MonoBehaviour
{
    WebSocket ws;

    public string nickname = "rafa";
    public connectWindowController cwc;

    private bool isConnected = false;
    public bool isMultiplayer = false;
    float connectStart;

    public Saves sv;
    public feedbackController f;
    public GameObject fpanel;
    public ActiveRoomsController r;
    public ServerRoomsController s;
    public StatsController stc;
    public pwdInputController pic;
    public MainLogic logic;

    private List<Room> displayedServerRooms = new List<Room>();
    private List<Room> displayedMyOnlineRooms = new List<Room>();

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


    IEnumerator PollServerLoop()
    {
        while (isConnected)
        {
            lastResponse = null;
            ws.SendText("9");

            yield return new WaitForSecondsRealtime(3f);

            if (gotResponse && lastResponse != null)
            {
                gotResponse = false;
                stc.updateStats(lastResponse[2], lastResponse[1]);
            }
            else
            {
                Debug.Log("shutting down");
                if(!(SceneManager.GetActiveScene().name == "Menu")){
                    logic.exitMode();
                }
                closeMultiplayer();
                openMultiplayer();
            }
        }
    }

    async void Start()
    {
        Debug.Log("WS started");
        sv = FindObjectOfType<Saves>();

        ws = new WebSocket("ws://localhost:8080");

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
                        fpanel.SetActive(true);
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
                    gotResponse = true;
                    lastResponse = data.Split(':');
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
                    break;
            }
        };

        ws.OnError += (e) =>
        {
            fpanel.SetActive(true);
            f.showMessage(e);
        };

        ws.OnOpen += () =>
        {
            ws.SendText(showName());
        };
    }

    void Update()
    {
#if UNITY_WEBGL
        ws?.DispatchMessageQueue();
#endif
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
        await ws.Connect();
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
        ws.SendText("8" + displayedMyOnlineRooms[id].id);
    }

    public void refreshMyActiveRooms()
    {
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
        ws.SendText("3" + id + ":" + pwd);
    }

    string showName() => "1" + nickname;
}



