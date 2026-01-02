using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using System;

public class MainLogic : MonoBehaviour
{
    UniversalRenderPipelineAsset urpAsset;
    public GameObject skillPanel;
    private Dictionary<int, float[]> benches = new Dictionary<int, float[]>();
    public Movement mv;
    public PausePanelController ppc;
    public WebSocketClient ws;
    public PlayerListController pl;
    public OnlinePlayersController opc;

    private KeyCode pausek;
    private KeyCode playersk;


    public Saves sv;

    private static MainLogic instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {   ppc = FindObjectOfType<PausePanelController>();
        sv = FindObjectOfType<Saves>();
        ws = FindObjectOfType<WebSocketClient>();
        opc = FindObjectOfType<OnlinePlayersController>();
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
        urpAsset = GraphicsSettings.defaultRenderPipeline as UniversalRenderPipelineAsset;
        applyGlobalGSettings();
        refreshKeyBinds();

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
        benches[1] = new float[]{3, 3.5f, 4}; //x y lvl
    }
    public void refreshKeyBinds()
    {
        pausek = (KeyCode)Enum.Parse(typeof(KeyCode),sv.getKeyBind("PAUSEKEY"));
        playersk = (KeyCode)Enum.Parse(typeof(KeyCode),sv.getKeyBind("PLAYERSKEY"));
    }
    
    void Update()
    {
        keyCallbacks();
    }
    void keyCallbacks(){
        if(SceneManager.GetActiveScene().name != "Menu"){
            if(ws.isMultiplayer){
                if(Input.GetKeyDown(playersk)){
                pl.showPlayerTab();
            }
            if(Input.GetKeyUp(playersk)){
                pl.hidePlayerTab();
            }
            }
            
            if(Input.GetKeyDown(pausek)){
                pause();
            }
        }
        
        //if(Input.GetKey(KeyCode.X)){
        //    sv.delete();
        //}
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(!(scene.name == "Menu")){
            Debug.Log("new scene loaded");
            sv.LoadSceneSaves();
            opc.players.Clear(); //clear player cache
            ppc = FindObjectOfType<PausePanelController>();
            ws = FindObjectOfType<WebSocketClient>();
            pl = FindObjectOfType<PlayerListController>(true);
            mv = FindObjectOfType<Movement>();
            Time.timeScale = 1f;
            Volume volume = FindObjectOfType<Volume>();
            if(volume != null)
            {
                if(sv.getPPS()==1) volume.enabled = true;
            else 
            volume.enabled = false;
            }
            mv.refreshKeyBinds();

        }
        ws.OnSceneLoaded();
    }
    public void death(){
        Movement.startPosX = benches[sv.LastBenchID][0];
        Movement.startPosY = benches[sv.LastBenchID][1];
        sv.loadSavesEnforced();
        SceneManager.LoadScene("lvl" + sv.getCurrentSaveId());

    }
    public void launchSave(string id){
        sv.loadSavesEnforced();
        sv.setSaveID(id);
        int lastbenchid = sv.getIntSave("LastBenchID");
        if(lastbenchid<0) lastbenchid = 0;
        Debug.Log(lastbenchid + "lastbenchid");
        for(int i = 0; i < benches.Count; i++){
            if(i == lastbenchid){ //if benches id in level matches last bench id of that level
                Movement.startPosX = benches[i][0];
                Movement.startPosY = benches[i][1];
                SceneManager.LoadScene("lvl" + benches[i][2]);
            }
        }
    }
    public void pause(){
        Time.timeScale = 0f;
        ppc.Activate();
    }
    public void unPause(){
        Time.timeScale = 1f;
    }
    public void exitMode(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        if(ws.isMultiplayer){
            ws.canPing = false;
            ws.leaveRoom();
            StartCoroutine(mpanelOpen());
            Debug.Log("Multiplayer open");
        } else {
            StartCoroutine(spanelOpen());
            Debug.Log("SinglePlayer open");
        }
    }
    IEnumerator mpanelOpen(){
        yield return null;
        yield return null;
        Transform mpanel = Resources.FindObjectsOfTypeAll<Transform>()
                            .FirstOrDefault(c => c.gameObject.name == "multiplayer");

            mpanel.gameObject.SetActive(true);
            ws.reAssignFields();
            opc.clearPlayers();
    }
    IEnumerator spanelOpen(){
        yield return null;
        yield return null;
        Transform spanel = Resources.FindObjectsOfTypeAll<Transform>()
                            .FirstOrDefault(c => c.gameObject.name == "Singleplayer");

            spanel.gameObject.SetActive(true);
}
    public void applyGlobalGSettings()
    {
       StartCoroutine(applyingChanges());
    }
    IEnumerator applyingChanges()
    {
        yield return new WaitForSeconds(1f);
        Application.targetFrameRate = sv.getFPS();
        QualitySettings.SetQualityLevel(sv.getQlvl()+1, true);
        urpAsset.renderScale = (float)sv.getRScale()*0.15f+0.7f;
        Debug.Log("Changes applied");
    }
    
}
