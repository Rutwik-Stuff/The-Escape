using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MainLogic : MonoBehaviour
{
    public GameObject skillPanel;
    private Dictionary<int, float[]> benches = new Dictionary<int, float[]>();
    public Movement mv;

    public Saves sv;

    private static MainLogic instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {   
        sv = FindObjectOfType<Saves>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // keeps this object alive between scenes
        }
        else
        {
            Destroy(gameObject); // destroy duplicates
        }
        mv = FindObjectOfType<Movement>();
        fillBenches();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void fillBenches(){
        benches[0] = new float[]{0, -2.4f, 0};
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
    public void death(){
        Movement.startPosX = benches[sv.LastBenchID][0];
        Movement.startPosY = benches[sv.LastBenchID][1];
        SceneManager.LoadScene("lvl" + benches[sv.LastBenchID][2]);

    }
}
