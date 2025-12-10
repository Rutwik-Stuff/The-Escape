using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections;
using UnityEngine.EventSystems;

public class ServerRoomController : MonoBehaviour, IDeselectHandler
{
    public TMP_Text name;
    public TMP_Text host;
    public TMP_Text uid;
    public TMP_Text playerCount;

    public int id;

    public ServerRoomsController parent;

    public WebSocketClient w;

    void Awake(){
        name = GetComponentsInChildren<TMP_Text>(true)
            .FirstOrDefault(t => t.name == "name");
        uid = GetComponentsInChildren<TMP_Text>(true)
            .FirstOrDefault(t => t.name == "uid");
        playerCount = GetComponentsInChildren<TMP_Text>(true)
            .FirstOrDefault(t => t.name == "playercount");
        host = GetComponentsInChildren<TMP_Text>(true)
            .FirstOrDefault(t => t.name == "host");
        w = FindObjectOfType<WebSocketClient>();
        hide();
    }

    public void hide(){
        gameObject.SetActive(false);
    }

    public void showIfNeeded(){
        if(w.hasSId(id)){
            Room room  = w.getRoomFromServerList(id);
            name.text = room.name;
            host.text = room.host;
            uid.text = room.id;
            playerCount.text = room.playerCount.ToString();
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
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

if (selected != null && selected.CompareTag("unselectable2"))
{
    Debug.Log("Reselecting");
            EventSystem.current.SetSelectedGameObject(gameObject);
}
}
}

