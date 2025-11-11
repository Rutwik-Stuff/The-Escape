using UnityEngine;

public class BenchLogic : MonoBehaviour
{
    private int benchID = 0;
    public Saves sv;
    void Awake(){
        sv = FindObjectOfType<Saves>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            sv.LastBenchID = benchID;
            sv.makeLatestSave();
        }
    }
}
