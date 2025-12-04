using UnityEngine;

public class leftWallCheck : MonoBehaviour
{
    public Movement mv;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("wjump")){
            mv.setOnWall(true, false);     
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        mv.setOnWall(false, false);
    }
}
