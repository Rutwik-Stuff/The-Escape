using UnityEngine;
using TMPro;

public class connectWindowController : MonoBehaviour
{
    public GameObject connectionPanel;
    public TMP_Text errorText;
    public GameObject retry;
    public GameObject connectingText;

    public void showConnecting(){
        connectionPanel.SetActive(true);
        connectingText.SetActive(true);
        retry.SetActive(false);
        errorText.gameObject.SetActive(false);
    }
    public void showRetry(string text){
        connectionPanel.SetActive(true);
        connectingText.SetActive(false);
        retry.SetActive(true);
        errorText.gameObject.SetActive(true);
        text = text+"                ";
        errorText.text = text.Substring(0, 30);
    }
    public void hide(){
        connectionPanel.SetActive(false);
    }
}
