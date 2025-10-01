using UnityEngine;
using System;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpVelocity = 10f;  
    public float walkVelocity = 5f;
    bool isOnGround = true;
    bool isJumping = false;
    long time;

    void Update()
    {
        if(isOnGround || isJumping){
            if (Input.GetKey(KeyCode.Space)) 
        {
            if(!isJumping) time = DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond;
            isJumping = true;
            if(DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond-time<500){
                var v = rb.linearVelocity;
                v.y = jumpVelocity;   
                rb.linearVelocity = v;
            } else {
                isJumping = false;
            }
            
        } else {
            isJumping = false;
        }
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
            if(isOnGround){
                var v= rb.linearVelocity;
                v.x = 0;
            
                rb.linearVelocity = v;
            }
            
        }
    }
    public void onGround(bool state){
        isOnGround = state;
    }
    
}
