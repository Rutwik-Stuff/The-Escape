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
    public GameObject left;
    public GameObject down;
    public GameObject right;
    private bool isHitting;
    private static bool isRight;
    public bool isOnWall = false;
    public bool isRightWall;
    private bool disableRight;
    private bool disableLeft;
    private bool wallJump;

    void Update()
    {
        if(isOnGround || isJumping || isOnWall){
            if (Input.GetKey(KeyCode.Space)){
                if(!isJumping) time = DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond;
                    isJumping = true;
                    if(DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond-time<500){   
                        if(isOnWall && !isOnGround || wallJump){
                            wallJump = true;
                            if(DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond-time<100){
                                 disableLeft = true;
                            } else {
                                disableLeft = false;
                            }
                            var v = rb.linearVelocity;
                            v.y = jumpVelocity;   
                            v.x = 12f;
                            rb.linearVelocity = v;
                        } else {
                            var v = rb.linearVelocity;
                            v.y = jumpVelocity;   
                            rb.linearVelocity = v;
                            disableLeft = false;
                            disableRight = false;
                        }
                    } else {
                        isJumping = false;
                        wallJump = false;
                    }
            
            } else {
                isJumping = false;
                wallJump = false;
                disableLeft = false;
                disableRight = false;
            }
        }
        
        if (Input.GetKey(KeyCode.D) && !disableRight){
        
            isRight = true;
            var v = rb.linearVelocity;
            v.x = walkVelocity;   
            rb.linearVelocity = v;
            if(Input.GetMouseButtonDown(0)){
                if(!isHitting){
                    isHitting = true;
                    right.SetActive(true);
                }
            }
        } else if (Input.GetKey(KeyCode.A) && !disableLeft){
        
            isRight = false;
            var v = rb.linearVelocity;
            v.x = -walkVelocity;   
            rb.linearVelocity = v;
            if(Input.GetMouseButtonDown(0)){
                if(!isHitting){
                    left.SetActive(true);
                    isHitting = true;
                }
            }
        } else if(Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.S)){
            if(!isHitting){
                isHitting = true;
                if(isRight) right.SetActive(true);
                else 
                left.SetActive(true);
            }
                
        } else if(isOnGround){
                var v= rb.linearVelocity;
                v.x = 0;
                rb.linearVelocity = v;
            
        } 
        if(Input.GetKey(KeyCode.S)){
            if(!isOnGround){
                if(Input.GetMouseButtonDown(0)){
                    if(!isHitting){
                        isHitting = true;
                        down.SetActive(true);
                    }
                }
            }
        }
        if(!isOnGround && isOnWall){
            rb.linearDamping = 9f;
        } else {
            rb.linearDamping = 1f;
        }
    }
    public void onGround(bool state){
        isOnGround = state;
    }
    public void setHitting(bool state){
        isHitting = state;
    }
    public void setOnWall(bool state, bool wallKind){
        isOnWall = state;
        isRightWall = wallKind;
    }
}
