using UnityEngine;
using TMPro;

public class feedbackController : MonoBehaviour
{
    public TMP_Text message;

    public void showMessage(string message){
        Debug.Log("llll");
        gameObject.SetActive(true);
        this.message.text = message;
    }

    public void close(){
        gameObject.SetActive(false);
    }
}
