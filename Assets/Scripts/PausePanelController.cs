using UnityEngine;

public class PausePanelController : MonoBehaviour
{
    public GameObject panel;
    public void Activate(){
        panel.SetActive(true);
    }
    public void onContinue(){
        Debug.Log("Continue");
        panel.SetActive(false);
        FindObjectOfType<MainLogic>().unPause();
    }
    public void onExit(){
        panel.SetActive(false);
        FindObjectOfType<MainLogic>().exitMode();
    }
}
