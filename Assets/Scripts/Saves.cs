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

    // Singleton pattern
    public static Saves Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Rebuild wallBreaks first if needed (add known wall IDs here)
        InitializeWallBreaks();

        // Load all saves
        loadSaves();
    }

    // Example: Add known walls here
    private void InitializeWallBreaks()
    {
        // Replace this with actual wall IDs from your game
        for (int id = 1; id <= 100; id++) // assuming 100 walls max
        {
            addNewWall(id);
        }
    }

    public void makeLatestSave()
    {
        makeIntSave("HitUnlocked", HitUnlocked);
        makeIntSave("WallJumpUnlocked", WallJumpUnlocked);
        makeIntSave("AirJumpUnlocked", AirJumpUnlocked);
        makeIntSave("DashUnlocked", DashUnlocked);
        makeIntSave("LastBenchID", LastBenchID);

        foreach (var entry in wallBreaks)
        {
            makeIntSave("wallbreak" + entry.Value[0], entry.Value[1]);
            Debug.Log("made wallbreak save " + getIntSave("wallbreak" + entry.Value[0]));
        }

        Debug.Log("Saves:");
        Debug.Log(getIntSave("HitUnlocked"));
        Debug.Log(getIntSave("WallJumpUnlocked"));
        Debug.Log(getIntSave("AirJumpUnlocked"));
        Debug.Log(getIntSave("DashUnlocked"));
        Debug.Log(getIntSave("LastBenchID"));

        PushSaves();
    }

    public void LoadSceneSaves()
    {
        LoadSavablesList();
        foreach (var obj in savables)
        {
            obj.receiveChanges();
        }
    }

    private void LoadSavablesList()
    {
        savables = FindObjectsOfType<MonoBehaviour>().OfType<Savable>().ToList();
    }

    private void loadSaves()
    {
        HitUnlocked = getIntSave("HitUnlocked");
        WallJumpUnlocked = getIntSave("WallJumpUnlocked");
        AirJumpUnlocked = getIntSave("AirJumpUnlocked");
        DashUnlocked = getIntSave("DashUnlocked");
        LastBenchID = getIntSave("LastBenchID");

        // Load wallbreak values
        List<string> keys = new List<string>(wallBreaks.Keys);
        foreach (var key in keys)
        {
            wallBreaks[key][1] = getIntSave(key);
        }
    }

    public void addNewWall(int id)
    {
        string key = "wallbreak" + id;
        if (!wallBreaks.ContainsKey(key))
        {
            wallBreaks[key] = new int[2] { id, getIntSave(key) }; // load existing value if any
        }
    }

    public int getWallState(int id)
    {
        string key = "wallbreak" + id;
        if (!wallBreaks.ContainsKey(key))
            addNewWall(id);
        return wallBreaks[key][1];
    }

    public void setWallState(int id, int value)
    {
        string key = "wallbreak" + id;
        if (!wallBreaks.ContainsKey(key))
            addNewWall(id);
        wallBreaks[key][1] = value;
        Debug.Log("wallstate : " + wallBreaks[key][1]);
    }

    public void PushSaves()
    {
        PlayerPrefs.Save();
    }

    public void makeIntSave(string name, int value)
    {
        PlayerPrefs.SetInt(curentSaveID + name, value);
    }

    public void makeStringSave(string name, string value)
    {
        PlayerPrefs.SetString(curentSaveID + name, value);
    }

    public void makeFloatSave(string name, float value)
    {
        PlayerPrefs.SetFloat(curentSaveID + name, value);
    }

    public int getIntSave(string name)
    {
        return PlayerPrefs.GetInt(curentSaveID + name, 0); // default to 0 instead of -1
    }

    public string getStringSave(string name)
    {
        return PlayerPrefs.GetString(curentSaveID + name, "UNKNOWN");
    }

    public float getFloatSave(string name)
    {
        return PlayerPrefs.GetFloat(curentSaveID + name, 0f);
    }

    public void delete(){
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); // Make sure to actually apply the deletion

    }
}
