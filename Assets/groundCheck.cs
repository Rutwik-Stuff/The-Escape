using UnityEngine;

public class groundCheck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Movement mv;

    void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("jumpable")){
            mv.onGround(true);
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("jumpable")){
            mv.onGround(false);
        }
    }
    void OnTriggerStay2D(Collider2D other){
        if(other.CompareTag("jumpable")){
            mv.onGround(true);
        }
    }

}
