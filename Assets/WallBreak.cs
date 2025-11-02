using UnityEngine;

public class WallBreak : MonoBehaviour
{
    public GameObject self;
    public int hitsToBreak = 3;
    private int hitsRemaining = 3;

    void Start(){
        if(gameObject.activeSelf){
            hitsRemaining = hitsToBreak;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        hitsRemaining--;
        if(hitsRemaining < 1){
            self.SetActive(false);
        }
    }

}
