using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject singlePlayerPanel;

    public void enableSinglePlayerPanel() {
        singlePlayerPanel.SetActive(true);
    }
    public void disableSinglePlayerPanel(){
        singlePlayerPanel.SetActive(false);
    }
}
