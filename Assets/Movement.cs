using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpVelocity = 10f;  
    public float walkVelocity = 5f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            var v = rb.linearVelocity;
            v.y = jumpVelocity;   
            rb.linearVelocity = v;
        }
        if (Input.GetKey(KeyCode.D)) 
        {
            var v = rb.linearVelocity;
            v.x = walkVelocity;   
            rb.linearVelocity = v;
        } else if (Input.GetKey(KeyCode.A)) 
        {
            var v = rb.linearVelocity;
            v.x = -walkVelocity;   
            rb.linearVelocity = v;
        } else {
            var v= rb.linearVelocity;
            v.x = 0;
            
            rb.linearVelocity = v;
        }
    }
    
}
