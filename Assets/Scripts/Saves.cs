using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Saves : MonoBehaviour
{
    public int HitUnlocked;
    public int WallJumpUnlocked;
    public int AirJumpUnlocked;
    public int DashUnlocked;
    public int LastBenchID;
    public string curentSaveID = "0";


    private Dictionary<string, int[]> wallBreaks = new Dictionary<string, int[]>();
    private List<Savable> savables = new List<Savable>();

    // Singleton pattern so you can access it anywhere
    public static Saves Instance { get; private set; }

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: persist across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        loadSaves();
        // Initialize default values if needed
    }
    public void makeLatestSave(){
        makeIntSave("HitUnlocked", HitUnlocked);
        makeIntSave("WallJumpUnlocked", WallJumpUnlocked);
        makeIntSave("AirJumpUnlocked", AirJumpUnlocked);
        makeIntSave("DashUnlocked", DashUnlocked);
        makeIntSave("LastBenchID", LastBenchID);

        foreach (KeyValuePair<string, int[]> entry in wallBreaks)
        {
            makeIntSave("wallBreak"+entry.Value[0], entry.Value[1]);
        }

        Debug.Log("Saves:");
        Debug.Log(getIntSave("HitUnlocked"));
        Debug.Log(getIntSave("WallJumpUnlocked"));
        Debug.Log(getIntSave("AirJumpUnlocked"));
        Debug.Log(getIntSave("DashUnlocked"));
        Debug.Log(getIntSave("LastBenchID"));
    }
    public void LoadSceneSaves(){
        LoadSavablesList();
        foreach(var obj in savables){
            obj.receiveChanges();
        }
    }
    private void LoadSavablesList(){
        savables = FindObjectsOfType<MonoBehaviour>().OfType<Savable>().ToList();
    }
    private void loadSaves(){
        //load saves fields from playeprefs
        HitUnlocked = 1;
        AirJumpUnlocked = 0;
        WallJumpUnlocked = 1;
        DashUnlocked = 0;
    }
    public void addNewWall(int id){
        if(wallBreaks.ContainsKey("wallbreak"+id)){
            //idk what to do if it is added already
        } else {
            wallBreaks["wallbreak"+id] = new int[2]{id, 0};
        }
        
    }
    public int getWallState(int id){
        return wallBreaks["wallbreak"+id][1];
    }
    public void setWallState(int id, int value){
        wallBreaks["wallbreak"+id][1] = value;
        Debug.Log("wallstate : " + wallBreaks["wallbreak"+id][1]);
    }


    
    public void PushSaves(){
        PlayerPrefs.Save();
    }
    public void makeIntSave(string name, int value){
        PlayerPrefs.SetInt(curentSaveID + name, value);
    }
    public void makeStringSave(string name, string value){
        PlayerPrefs.SetString(curentSaveID + name, value);
    }
    public void makeFloatSave(string name, float value){
        PlayerPrefs.SetFloat(curentSaveID + name, value);
    }
    public int getIntSave(string name){
        return PlayerPrefs.GetInt(curentSaveID + name, -1);
    }
    public string getStringSave(string name){
        return PlayerPrefs.GetString(curentSaveID + name, "UNKNOWN");
    }
    public float getFloatSave(string name){
        return PlayerPrefs.GetFloat(curentSaveID + name, 0f);
    }

}
