using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject singlePlayerPanel;
    public GameObject multiplayerPanel;

    public void enableSinglePlayerPanel() {
        singlePlayerPanel.SetActive(true);
    }
    public void disableSinglePlayerPanel(){
        singlePlayerPanel.SetActive(false);
    }
    public void enableMultiplayerPanel(){
        multiplayerPanel.SetActive(true);
    }
    public void disableMultiplayerPanel(){
        FindObjectOfType<WebSocketClient>().closeMultiplayer();
        multiplayerPanel.SetActive(false);
    }
}
