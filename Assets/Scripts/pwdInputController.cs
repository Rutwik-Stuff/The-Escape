using UnityEngine;
using TMPro;


public class pwdInputController : MonoBehaviour
{
    public TMP_InputField pwd;

    private string id;

    public WebSocketClient w;

    public void showInputPassword(string id){
        gameObject.SetActive(true);
        this.id = id;
    }

    public void sendPwd(){
        w.joinByAddress(id, pwd.text);
        gameObject.SetActive(false);
    }

}
