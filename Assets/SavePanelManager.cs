using UnityEngine;
using TMPro;
using System.Linq;

public class SavePanelManager : MonoBehaviour
{
    public Transform add;
    public Transform play;
    public Transform edit;
    public Transform delete;
    public Transform nameBackground;
    public TMP_Text levelName;

    public saveNameManager snm;
    public MainLogic logic;

    public string id;

    public Saves sv;
    void OnEnable() {
        if(sv.hasSave(id)){
            showDetails();
        } else {
            hideDetails();
        }
    }


    void Awake(){
        play = GetComponentsInChildren<Transform>(true)
            .FirstOrDefault(t => t.name == "play");
        edit = GetComponentsInChildren<Transform>(true)
            .FirstOrDefault(t => t.name == "play (1)");
        delete = GetComponentsInChildren<Transform>(true)
            .FirstOrDefault(t => t.name == "delete");
        nameBackground = GetComponentsInChildren<Transform>(true)
            .FirstOrDefault(t => t.name == "Image");
        levelName = GetComponentsInChildren<TMP_Text>(true)
            .FirstOrDefault(t => t.name == "Text (TMP)");
        add = GetComponentsInChildren<Transform>(true)
            .FirstOrDefault(t => t.name == "Button");

        sv = FindObjectOfType<Saves>();
        logic = FindObjectOfType<MainLogic>();
        snm = FindObjectOfType<saveNameManager>(includeInactive: true);
    }
    void showDetails(){
        levelName.gameObject.SetActive(true);
        add.gameObject.SetActive(false);
        play.gameObject.SetActive(true);
        edit.gameObject.SetActive(true);
        delete.gameObject.SetActive(true);
        nameBackground.gameObject.SetActive(true);
        levelName.text = sv.loadSaveName(id);
    }
    public void createSave(){
        showDetails();
        renamelvl();
    }
    public void reloadSaves(){
        showDetails();
    }
    public void renamelvl(){
        snm.changeName(id, this);
    }
    public void deletesaves(){
        sv.deleteAllForId(id);
        hideDetails();
    }
    void hideDetails(){
        levelName.gameObject.SetActive(false);
        add.gameObject.SetActive(true);
        play.gameObject.SetActive(false);
        edit.gameObject.SetActive(false);
        delete.gameObject.SetActive(false);
        nameBackground.gameObject.SetActive(false);
        
    }
    public void playSave(){
        logic.launchSave(id);
    }
}
