using UnityEngine;

public class rightWallCheck : MonoBehaviour
{
    public Movement mv;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("wjump")){
            mv.setOnWall(true, true);     
        }
    }
    void OnTriggerExit2D(Collider2D collider){
        mv.setOnWall(false, true);
    }
}
