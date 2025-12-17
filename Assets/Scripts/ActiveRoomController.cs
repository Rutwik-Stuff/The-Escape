using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;
using System.Collections;


public class ActiveRoomController : MonoBehaviour, IDeselectHandler
{
    public TMP_Text name;
    public TMP_Text password;
    public TMP_Text playerCount;
    public TMP_InputField uid;

    public int id;

    public int roomid;

    public ActiveRoomsController parent;

    public WebSocketClient w;

    public void showIfNeeded(){
        if(w.hasId(id)){
            Room room = w.getRoomFromMyRoomList(id);
            name.text = room.name;
            password.text = room.password;
            playerCount.text = room.playerCount.ToString();
            uid.text = room.id;
            gameObject.SetActive(true);
            uid.gameObject.SetActive(true);
        }  else {
            Debug.Log("no id");
        gameObject.SetActive(false);
        }
        

    }
    public void hide(){
        gameObject.SetActive(false);
    }
    void Awake(){
        name = GetComponentsInChildren<TMP_Text>(true)
            .FirstOrDefault(t => t.name == "name");
        uid = GetComponentsInChildren<TMP_InputField>(true)
            .FirstOrDefault(t => t.name == "uid");
        playerCount = GetComponentsInChildren<TMP_Text>(true)
            .FirstOrDefault(t => t.name == "count");
        password = GetComponentsInChildren<TMP_Text>(true)
            .FirstOrDefault(t => t.name == "pwd");
        w = FindObjectOfType<WebSocketClient>();
        hide();
    }

    public bool isSelected(){
        Debug.Log(EventSystem.current.currentSelectedGameObject == gameObject);
        return EventSystem.current.currentSelectedGameObject == gameObject;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        
        StartCoroutine(SelectNextFrame());
        
    }
    IEnumerator SelectNextFrame()
{
    yield return null; 
    GameObject selected = EventSystem.current.currentSelectedGameObject;

if (selected != null && selected.CompareTag("unselectable"))
{
    Debug.Log("Reselecting");
            EventSystem.current.SetSelectedGameObject(gameObject);
}
}
}
