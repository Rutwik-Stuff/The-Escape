using UnityEngine;

public class WallBreak : MonoBehaviour, Savable
{
    public GameObject self;
    public int hitsToBreak = 3;
    private int hitsRemaining = 3;
    public int ID;
    public Saves sv;

    void Awake() {
    Debug.Log("Wall Awake");
    sv = FindObjectOfType<Saves>();
    }



    void OnTriggerEnter2D(Collider2D other) {
    if(other.CompareTag("Hit")){
        hitsRemaining--;
        if(hitsRemaining < 1){
            sv.setWallState(ID, 1);
            self.SetActive(false);
        } else {
            sv.setWallState(ID, 0); // optional for partial hits
        }
    }
}

    public void receiveChanges(){
        int state = sv.getWallState(ID);
        self.SetActive(state != 1);
        
    }


}
