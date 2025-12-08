using UnityEngine;

public class ActiveRoomsController : MonoBehaviour
{
    public ActiveRoomController room1;
    public ActiveRoomController room2;
    public ActiveRoomController room3;

    public void roomClicked(int id){
        Debug.Log("Room Clicked "+ id);
    }
    public void refresh(){
        Debug.Log("refreshing rooms");
        room1.showIfNeeded();
        room2.showIfNeeded();
        room3.showIfNeeded();
    }
}
