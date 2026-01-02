using UnityEngine;

public class InSettingsUIController : MonoBehaviour
{
    public GameObject displayPanel;
    public GameObject controlsPanel;
    public GameObject aboutPanel;
    public GameObject otherPanel;

    public void enableDisplayPanel(){
        displayPanel.SetActive(true);
    }
    
    public void enableControlsPanel(){
        controlsPanel.SetActive(true);
    }
    
    public void enableAboutPanel(){
        aboutPanel.SetActive(true);
    }
    
    public void enableOtherPanel(){
        otherPanel.SetActive(true);
    }
    
    public void closeAllInSettingsPanels(){
        displayPanel.SetActive(false);
        controlsPanel.SetActive(false);
        aboutPanel.SetActive(false);
        otherPanel.SetActive(false);
    }

}
