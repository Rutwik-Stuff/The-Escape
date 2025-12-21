using UnityEngine;

public class ActiveRoomsController : MonoBehaviour
{
    public ActiveRoomController room1;
    public ActiveRoomController room2;
    public ActiveRoomController room3;

    private ActiveRoomController lastSelected;

    public WebSocketClient w;
    public Saves sv;

    public void roomClicked(int id){
        Debug.Log("Room Clicked "+ id);
    }
    public void refresh(){
        Debug.Log("refreshing rooms");
        room1.showIfNeeded();
        room2.showIfNeeded();
        room3.showIfNeeded();
    }

    public void checkSelected(){
        if(room1.isSelected()) lastSelected = room1;
        else if(room2.isSelected()) lastSelected = room2;
        else if(room3.isSelected()) lastSelected = room3;
        else lastSelected = null;

    }
    void Awake(){
        w = FindObjectOfType<WebSocketClient>();
    }

    public void join(){
        checkSelected();
        if(lastSelected != null){
            string id = sv.getIdMatchingUID(lastSelected.uid.text);
             if(id == null){
                //not my room
                sv.setSaveID("R"+lastSelected.uid.text);
            } else {
                //my room
                sv.setSaveID("R"+id);
            }
            w.joinMyRoom(lastSelected.id);
        }
        
    }
    public void stop(){
        checkSelected();
        if(lastSelected != null){
            Debug.Log("Selected to stop "+ lastSelected.id);
            w.stopActivRoom(lastSelected.id);
        }
    }
    public void players(){
        checkSelected();
        if(lastSelected != null){
            Debug.Log("Selected to players "+ lastSelected.id);
        }
    }

    
}
