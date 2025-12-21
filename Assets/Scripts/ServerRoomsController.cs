using UnityEngine;
using TMPro;
public class ServerRoomsController : MonoBehaviour
{
    public ServerRoomController room0;
    public ServerRoomController room1;
    public ServerRoomController room2;
    public ServerRoomController room3;
    public ServerRoomController room4;
    public ServerRoomController room5;

    public GameObject addressPanel;
    public TMP_InputField address;

    public GameObject leftArrow;
    public GameObject rightArrow;

    public TMP_Text pageCount;
    public Saves sv;

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
    void Awake(){
        w = FindObjectOfType<WebSocketClient>();
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
            string id = sv.getIdMatchingUID(lastSelected.uid.text);
            if(id == null){
                //not my room
                sv.setSaveID("R"+lastSelected.uid.text);
            } else {
                //my room
                sv.setSaveID("R"+id);
            }
            w.joinServerRoom(lastSelected.id);
        }

    }
    public void nextPage(){
        page++;
        if(page > 10){
            page = 1;
        }
        pageCount.text = page.ToString();
            w.refreshServerRooms();
    }
    public void prevPage(){
        page--;
        if(page < 1){
            page = 10;
            
        }
        pageCount.text = page.ToString();
            w.refreshServerRooms();
    }
    public void showAddressPanel(){
        addressPanel.SetActive(true);
    }
    public void closeAddresspanel(){
        address.text = "";
        addressPanel.SetActive(false);
    }
    public void joinByAddress(){
        //join
        addressPanel.SetActive(false);
        w.joinAddress(address.text);
    }
}
