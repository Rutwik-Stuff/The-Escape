using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class saveNameManager : MonoBehaviour
{
    private string id;
    private SavePanelManager spm;

    public TMP_InputField input;

    public void changeName(string id, SavePanelManager spm){
        gameObject.SetActive(true);
        this.id = id;
        this.spm = spm;
    }
    public void onOkPressed(){
        Saves sv = FindObjectOfType<Saves>();
        string text = input.text+"          ";
        sv.createSaveName(text.Substring(0, 9), id);
        spm.reloadSaves();
        gameObject.SetActive(false);
    }
}
