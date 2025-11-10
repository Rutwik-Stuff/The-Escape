using UnityEngine;

public class WallBreak : MonoBehaviour, Savable
{
    public GameObject self;
    public int hitsToBreak = 3;
    private int hitsRemaining = 3;
    public int ID;
    public Saves sv;

    void Awake() {
    sv = FindObjectOfType<Saves>();
    sv.addNewWall(ID); // ensures dictionary has the entry

    int savedState = sv.getWallState(ID);

    if(savedState == 1){
        hitsRemaining = 0;
        self.SetActive(false);
    } else {
        hitsRemaining = hitsToBreak; // or calculate partial hits if needed
        self.SetActive(true);
    }

    // This line ensures the dictionary is fully synced
    sv.setWallState(ID, savedState);
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
