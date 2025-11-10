using UnityEngine;
using UnityEngine.SceneManagement;

public class MainLogic : MonoBehaviour
{
    public GameObject skillPanel;

    public Saves sv;

    private static MainLogic instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // keeps this object alive between scenes
        }
        else
        {
            Destroy(gameObject); // destroy duplicates
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    void Update()
    {
        keyCallbacks();
    }
    void keyCallbacks(){
        /*
        if(Input.GetKey(KeyCode.Tab)){
            skillPanel.SetActive(true);
        } else {
            skillPanel.SetActive(false);
        }*/
        if(Input.GetKey(KeyCode.X)){
            sv.delete();
        }
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(!(scene.name == "Menu")){
            Debug.Log("new scene loaded");
            sv.LoadSceneSaves();
        }
    }
}
