using UnityEngine;

public class WallBreakTrig : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){

        }
    }
    void OnTriggerExit2D(Collider2D collider){
        if(collider.CompareTag("Player")){

        }
    }
    
}
