using UnityEngine;

public class Saves : MonoBehaviour
{
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

        // Initialize default values if needed
        
    }
    public void PushSaves(){
        PlayerPrefs.Save();
    }
    public void makeIntSave(string name, int value){
        PlayerPrefs.SetInt(name, value);
    }
    public void makeStringSave(string name, string value){
        PlayerPrefs.SetString(name, value);
    }
    public void makeFloatSave(string name, float value){
        PlayerPrefs.SetFloat(name, value);
    }
    public int getIntSave(string name){
        return PlayerPrefs.GetInt(name, 0);
    }
    public string getStringSave(string name){
        return PlayerPrefs.GetString(name, "UNKNOWN");
    }
    public float getFloatSave(string name){
        return PlayerPrefs.GetFloat(name, 0f);
    }

}
