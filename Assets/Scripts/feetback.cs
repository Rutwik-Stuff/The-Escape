using UnityEngine;
using TMPro;
public class feetback : MonoBehaviour
{
    public TMP_Text text;

    public void ShowFeedback(string message){
        gameObject.SetActive(true);
        text.text = message;
    }
    public void close(){
        gameObject.SetActive(false);
    }
}
