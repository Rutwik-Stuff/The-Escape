using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Saves : MonoBehaviour
{
    public int HitUnlocked;
    public int WallJumpUnlocked;
    public int AirJumpUnlocked;
    public int DashUnlocked;
    public int LastBenchID = 0; //default
    private string curentSaveID = "0";
    public string lvlTime;

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
        Debug.Log("First Wall State Load");
        loadSaves();
    }

    // Example: Add known walls here
    private void InitializeWallBreaks()
    {
        // Replace this with actual wall IDs from your game
        for (int id = 0; id <= 10; id++) // assuming 100 walls max
        {
            addNewWall(id);
        }
    }

    public void makeLatestSave()
    {
        Debug.Log("WallBreaks Size: " + wallBreaks.Count);
        makeIntSave("HitUnlocked", HitUnlocked);
        makeIntSave("WallJumpUnlocked", WallJumpUnlocked);
        makeIntSave("AirJumpUnlocked", AirJumpUnlocked);
        makeIntSave("DashUnlocked", DashUnlocked);
        makeIntSave("LastBenchID", LastBenchID);
        makeStringSave("lvlTime", lvlTime);

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
        Debug.Log("WallBreaks Size: " + wallBreaks.Count);
        LoadSavablesList();
        Debug.Log("Load at new Scene");
        loadSaves();
        foreach (var obj in savables)
        {
            obj.receiveChanges();
        }
    }

    private void LoadSavablesList()
    {
        savables = FindObjectsOfType<MonoBehaviour>().OfType<Savable>().ToList();
    }

    public void loadSavesEnforced() {
        HitUnlocked = getIntSave("HitUnlocked");
        WallJumpUnlocked = getIntSave("WallJumpUnlocked");
        AirJumpUnlocked = getIntSave("AirJumpUnlocked");
        DashUnlocked = getIntSave("DashUnlocked");
        LastBenchID = getIntSave("LastBenchID");
        if(LastBenchID < 0) LastBenchID = 0;
    

        // Load wallbreak values
        List<string> keys = new List<string>(wallBreaks.Keys);
        foreach (var key in keys)
        {
            if(!(getIntSave(key)==-1)){
                wallBreaks[key][1] = getIntSave(key);
                Debug.Log("WallBreaks load: " + key + " : " + wallBreaks[key][1]);
            }
            Debug.Log("WallBreaks state: " + key + " : " + wallBreaks[key][1]);
            
        }
    }

    private void loadSaves()
    {

        if(HitUnlocked < getIntSave("HitUnlocked")) HitUnlocked = HitUnlocked;
        WallJumpUnlocked = getIntSave("WallJumpUnlocked");
        AirJumpUnlocked = getIntSave("AirJumpUnlocked");
        DashUnlocked = getIntSave("DashUnlocked");
        LastBenchID = getIntSave("LastBenchID");
        if(LastBenchID < 0) LastBenchID = 0;
    

        // Load wallbreak values
        List<string> keys = new List<string>(wallBreaks.Keys);
        foreach (var key in keys)
        {
            if(!(getIntSave(key)==-1)){
                wallBreaks[key][1] = getIntSave(key);
                Debug.Log("WallBreaks load: " + key + " : " + wallBreaks[key][1]);
            }
            Debug.Log("WallBreaks state: " + key + " : " + wallBreaks[key][1]);
            
        }
    }

    public void addNewWall(int id)
    {
        string key = "wallbreak" + id;
        if (!wallBreaks.ContainsKey(key))
        {
            wallBreaks[key] = new int[2] { id, getIntSave(key) };
            Debug.Log("WallBreaks add: " + key + " : " + wallBreaks[key][1]); 
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
        Debug.Log("WallBreaks set: " + key + " : " + wallBreaks[key][1]);
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
        return PlayerPrefs.GetInt(curentSaveID + name, -1); // default to 0 instead of -1
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
    public void reload(){
        foreach (var obj in savables)
        {
            obj.receiveChanges();
        }
    }
    public void setSaveID(string id){
        curentSaveID = id;
    }
    public void createSaveName(string name, string id){
        PlayerPrefs.SetString(id+"Name", name);
    }
    public string loadSaveName(string id){
        return PlayerPrefs.GetString(id+"Name", "-");
    }
    public string getlvlTime(string id){
        return PlayerPrefs.GetString(id, "-:--");
    }
    public void deleteAllForId(string id){
        PlayerPrefs.DeleteKey(id+"Name");
        PlayerPrefs.DeleteKey(id+"lvlTime");
        PlayerPrefs.DeleteKey(id+"HitUnlocked");
        PlayerPrefs.DeleteKey(id+"WallJumpUnlocked");
        PlayerPrefs.DeleteKey(id+"AirJumpUnlocked");
        PlayerPrefs.DeleteKey(id+"DashUnlocked");
        PlayerPrefs.DeleteKey(id+"LastBenchID");
        PlayerPrefs.DeleteKey(id+"PWD");
        PlayerPrefs.DeleteKey(id+"UID");
        foreach (var entry in wallBreaks)
        {
            PlayerPrefs.DeleteKey(id+"wallBreak"+entry.Value[0]);
        }
        

    }
    public bool hasSave(string id){
        if(PlayerPrefs.HasKey(id+"Name")){
            return true;
        } else {
            return false;
        }
    }
    public string getCurrentSaveId(){
        return curentSaveID;
    }
    public void saveRoom(string name, string password, string id, string uid){
        PlayerPrefs.SetString("R"+id+"Name", name);
        PlayerPrefs.SetString("R"+id+"PWD", password);
        PlayerPrefs.SetString("R"+id+"UID", uid);
        setSaveID("R"+id);
        loadSavesEnforced();
        makeLatestSave();

    }
    public string loadPwd(string id){
        return PlayerPrefs.GetString("R"+id+"PWD");
    }
    public string loadUID(string id){
        return PlayerPrefs.GetString("R"+id+"UID", "-");
    }
    public void changeUID(string id, string uid)
{
    id = id.Trim();
    uid = uid.Trim();

    // Remove any dangerous characters
    id = new string(id.Where(c => c >= 32 && c <= 126).ToArray());
    uid = new string(uid.Where(c => c >= 32 && c <= 126).ToArray());

    string key = $"R{id}UID";

    Debug.Log("Saving PlayerPrefs key: " + key + "  value: " + uid);

    PlayerPrefs.SetString(key, uid);
    PlayerPrefs.Save();
}
    public string getIdMatchingUID(string uid){
        for(int i = 0; i < 4; i++){
            if(PlayerPrefs.GetString("R"+i+"UID", "0")==uid){
                return i.ToString();
            }
        }
        return null;
    }
    public string getNickname(){
        return PlayerPrefs.GetString("NICK", "");
    }
    public void saveNick(string nick){
        PlayerPrefs.SetString("NICK", nick);
    }
    public void saveGraphicsSettings(int fps, int rscale, int qlevel, int ppstatus)
    {
        PlayerPrefs.SetInt("TFPS", fps);
        PlayerPrefs.SetInt("RSCALE", rscale);
        PlayerPrefs.SetInt("QLVL", qlevel);
        PlayerPrefs.SetInt("PPS", ppstatus);
    }
    public int getFPS()
    {
        return PlayerPrefs.GetInt("TFPS", -1);
    }
    public int getRScale()
    {
        return PlayerPrefs.GetInt("RSCALE", 0);
    }
    public int getQlvl()
    {
        return PlayerPrefs.GetInt("QLVL", 0);
    }
    public int getPPS()
    {
        return PlayerPrefs.GetInt("PPS", 1);
    }
    public void saveKeyBind(string name, string keycode)
    {
        PlayerPrefs.SetString(name, keycode);
    }
    public string getKeyBind(string name)
    {
        return PlayerPrefs.GetString(name, "-");
    }
    public void saveServerAddress(string addr)
    {
        PlayerPrefs.SetString("ADDR", addr);
    }
    public string getServerAddress()
    {
        return PlayerPrefs.GetString("ADDR", "ws://localhost:8080");
    }
}

