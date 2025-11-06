using UnityEngine;

public class RightHitTrigger : MonoBehaviour
{
    public Movement mv;
    
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Hit_Death")){
            Debug.Log("hit death");
            mv.HitLeftImpulse();
        }
    }
}
