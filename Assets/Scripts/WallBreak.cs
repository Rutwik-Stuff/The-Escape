using UnityEngine;

public class WallBreak : MonoBehaviour, Savable
{
    public GameObject self;
    public int hitsToBreak = 3;
    private int hitsRemaining = 3;
    public int ID;
    public Saves sv;

    void Awake(){
        sv = FindObjectOfType<Saves>();
        if(sv.getWallState(ID)==1){
            Debug.Log("Broken");
            self.SetActive(false);
        } else{
            self.SetActive(true);
        }
        if(gameObject.activeSelf){
            hitsRemaining = hitsToBreak;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Hit")){
            hitsRemaining--;
            if(hitsRemaining < 1){
                sv.setWallState(ID, 1);
                self.SetActive(false);
            }
        }
        
    }
    public void receiveChanges(){
        sv.addNewWall(ID);
        if(sv.getWallState(ID)==1){
            Debug.Log("Broken");
            self.SetActive(false);
        } else {
            self.SetActive(true);
        }
    }

}
