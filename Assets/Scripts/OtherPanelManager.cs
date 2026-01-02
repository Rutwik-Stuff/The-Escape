using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OtherPanelManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public Saves sv;

    void Awake()
    {
        sv = FindObjectOfType<Saves>();
    }

    public void saveChanges()
    {
        sv.saveServerAddress(inputField.text);
        
    }
    public void deleteData()
    {
        sv.delete();
    }
    public void loadSaves()
    {
        inputField.text = sv.getServerAddress();
    }
}
