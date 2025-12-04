using UnityEngine;
using TMPro;
using System.Linq;

public class multiplayerSaves : MonoBehaviour
{
    public TMP_Text name;
    public TMP_Text pwd;
    public Transform add;
    public Transform edit;
    public Transform delete;
    public Transform host;

    public string id;
    public multiplayerFill mf;

    public Saves sv;
    public WebSocketClient w;

    void OnEnable() {
        if(sv.hasSave("R"+id)){
            showDetails();
        } else {
            hideDetails();
        }
    }

    public void showDetails(){
        name.gameObject.SetActive(true);
        pwd.gameObject.SetActive(true);
        add.gameObject.SetActive(false);
        host.gameObject.SetActive(true);
        edit.gameObject.SetActive(true);
        delete.gameObject.SetActive(true);
        name.text = sv.loadSaveName("R"+id);
        pwd.text = sv.loadPwd(id);

    }
    public void hideDetails(){
        name.gameObject.SetActive(false);
        pwd.gameObject.SetActive(false);
        add.gameObject.SetActive(true);
        host.gameObject.SetActive(false);
        edit.gameObject.SetActive(false);
        delete.gameObject.SetActive(false);
    }
    public void hostRoom(){
        w.launchRoom(sv.loadSaveName("R"+id), sv.loadPwd(id), sv.loadUID(id), id);

    }
     public void deletesaves(){
        sv.deleteAllForId("R"+id);
        hideDetails();
    }

    public void createSave(){
        showDetails();
        renamelvl();
    }

    public void renamelvl(){
        mf.changeName(id, this);
    }




    void Awake(){
        name = GetComponentsInChildren<TMP_Text>(true)
            .FirstOrDefault(t => t.name == "name");
        host = GetComponentsInChildren<Transform>(true)
            .FirstOrDefault(t => t.name == "Button");
        edit = GetComponentsInChildren<Transform>(true)
            .FirstOrDefault(t => t.name == "edit");
        delete = GetComponentsInChildren<Transform>(true)
            .FirstOrDefault(t => t.name == "delete");
        pwd = GetComponentsInChildren<TMP_Text>(true)
            .FirstOrDefault(t => t.name == "pwd");
        add = GetComponentsInChildren<Transform>(true)
            .FirstOrDefault(t => t.name == "add");
        sv = FindObjectOfType<Saves>();
        w = FindObjectOfType<WebSocketClient>();
    }
}
