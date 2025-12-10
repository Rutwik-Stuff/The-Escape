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

    public ServerRoomController lastSelected;

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

    public void checkSelected(){
        if(room0.isSelected()) lastSelected = room0;
        else if(room1.isSelected()) lastSelected = room1;
        else if(room2.isSelected()) lastSelected = room2;
        else if(room3.isSelected()) lastSelected = room3;
        else if(room4.isSelected()) lastSelected = room4;
        else if(room5.isSelected()) lastSelected = room5;
        else lastSelected = null;

    }
    public void join(){
        checkSelected();
        if(lastSelected != null){
            Debug.Log("Selected to join "+ lastSelected.id);
        }

    }
}
