using UnityEngine;

public class DownHitTrigger : MonoBehaviour
{
    public Movement mv;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision){
        if(collision.CompareTag("Hit_Death")){
            Debug.Log("hit death");
            mv.HitUpImpulse();
        }
    }
}
