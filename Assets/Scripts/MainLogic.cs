using UnityEngine;

public class MainLogic : MonoBehaviour
{
    public GameObject skillPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    
    void Update()
    {
        keyCallbacks();
    }
    void keyCallbacks(){
        if(Input.GetKey(KeyCode.Tab)){
            skillPanel.SetActive(true);
        } else {
            skillPanel.SetActive(false);
        }
    }
}
