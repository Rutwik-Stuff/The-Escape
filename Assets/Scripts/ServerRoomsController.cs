using UnityEngine;

public class ServerRoomsController : MonoBehaviour
{
    public ServerRoomController room0;
    public ServerRoomController room1;
    public ServerRoomController room2;
    public ServerRoomController room3;
    public ServerRoomController room4;
    public ServerRoomController room5;

    public WebSocketClient w;

    private int page = 1;

    public int getPage(){
        return page;
    }

    public void refresh(){
        Debug.Log("refreshing rooms");
        room0.showIfNeeded();
        room1.showIfNeeded();
        room2.showIfNeeded();
        room3.showIfNeeded();
        room4.showIfNeeded();
        room5.showIfNeeded();
    }


}
