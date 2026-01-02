using UnityEngine;
using System;

public class Movement : MonoBehaviour, Savable
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
    private bool isDown;
    public bool isOnWall = false;
    private bool isExtraJump = false;
    private bool canAirJump = false;
    public bool isRightWall;
    private bool disableRight;
    private bool disableLeft;
    private int dashesLeft = 0;
    private bool wallJump;
    private long dashTime;
    public static float startPosX;
    public static float startPosY;
    public float startX = 0;
    public float startY = 0;
    public float hitImpulse = 5f;
    public GameObject fire;
    public bool DashRegistered;
    bool wasHit = false;

    //----------Key Codes ---------------
    private static KeyCode downk;
    private static KeyCode leftk;
    private static KeyCode rightk;
    private static KeyCode jumpk;
    private static KeyCode dashk;
    private static KeyCode hitk;
    //-----------------------------------

    private bool HitUnlocked;
    private bool AirJumpUnlocked;
    private bool DashUnlocked;
    private bool WallJumpUnlocked;

    public Saves sv;

    void Start(){
        if(startPosX != 0 && startPosY != 0){
            transform.position = new Vector2(startPosX, startPosY);
        } else {
            Debug.Log(startX + " " + startY);
            transform.position = new Vector2(startX, startY);
        }
        startPosX = 0;
        startPosY = 0;
    }

    void Update()
    {
        if(isOnGround || isJumping || isOnWall || canAirJump){
            if (Input.GetKey(jumpk)){
                if(!isJumping) time = DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond;
                    isJumping = true;
                    fire.SetActive(true);
                    if(canAirJump) isExtraJump = false;
                    if(DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond-time<500){   
                        if((isOnWall && !isOnGround || wallJump) && WallJumpUnlocked){ 
                            wallJump = true;
                            if(DateTime.Now.Ticks/TimeSpan.TicksPerMillisecond-time<100){
                                 disableLeft = true;
                                 disableRight = true;
                            } else {
                                disableLeft = false;
                                disableRight = false;
                            }
                            var v = rb.linearVelocity;
                            v.y = jumpVelocity;
                            if(!isRightWall){
                                v.x = 8f;
                            } else {
                                v.x = -8f;
                            }
                            
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
                        canAirJump = false;
                    }
            
            } else {
                isJumping = false;
                wallJump = false;
                disableLeft = false;
                disableRight = false;
                canAirJump = false;
            }
        }
        
        if (Input.GetKey(rightk) && !disableRight){
        
            isRight = true;
            isDown = false;
            var v = rb.linearVelocity;
            if(v.x <= walkVelocity){
                v.x = walkVelocity;   
                rb.linearVelocity = v;
            }
            if(Input.GetKey(downk)){
                if(!isOnGround){
                 if(Input.GetKeyDown(hitk) && HitUnlocked){
                     if(!isHitting){
                        isHitting = true;
                        down.SetActive(true);
                        isDown = true;
                        wasHit = true;
                    }
                }
            }
        } else if(Input.GetKeyDown(hitk) && HitUnlocked){
                if(!isHitting){
                    isHitting = true;
                    right.SetActive(true);
                    isDown = false;
                    wasHit = true;
                }
            }
        } else if (Input.GetKey(leftk) && !disableLeft){
        
            isRight = false;
            var v = rb.linearVelocity;
            if(v.x >= -walkVelocity){
                v.x = -walkVelocity;   
                rb.linearVelocity = v;
            }
            if(Input.GetKey(downk)){
            if(!isOnGround){
                if(Input.GetKeyDown(hitk) && HitUnlocked){
                    if(!isHitting){
                        isHitting = true;
                        down.SetActive(true);
                        isDown = true;
                        wasHit = true;
                    }
                }
            }
        } else if(Input.GetKeyDown(hitk) && HitUnlocked){
                if(!isHitting){
                    left.SetActive(true);
                    isHitting = true;
                    isDown = false;
                    wasHit = true;
                }
            }
        } else if(Input.GetKeyDown(hitk) && !Input.GetKey(downk) && HitUnlocked){
            if(!isHitting){
                isHitting = true;
                isDown = false;
                wasHit = true;
                if(isRight) right.SetActive(true);
                else 
                left.SetActive(true);
            }
                
        } else if(isOnGround){
                var v= rb.linearVelocity;
                v.x = 0;
                rb.linearVelocity = v;
            
        } 
        if(Input.GetKey(downk)){
            if(!isOnGround){
                if(Input.GetKeyDown(hitk) && HitUnlocked){
                    if(!isHitting){
                        isHitting = true;
                        isDown = true;
                        wasHit = true;
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
        if(!isJumping && !isOnGround && !isOnWall && isExtraJump && AirJumpUnlocked){
            canAirJump = true;
        }
        if(!isJumping){
            fire.SetActive(false);
        }
        if(Input.GetKeyDown(dashk)){
            if(dashesLeft>0 && !isOnGround && DashUnlocked){
                dashesLeft--;
                DashRegistered = true;
            if(isRight){
                //Debug.Log("Dashing");
                rb.AddForce(new Vector2(1,0)*15f, ForceMode2D.Impulse);
            } else {
                rb.AddForce(new Vector2(-1,0)*15f, ForceMode2D.Impulse);
            }

        }
        }
        
        
    }
    public void onGround(bool state){
        isOnGround = state;
        if(!state){
            isExtraJump = true;
        } else {
            dashesLeft = 1;
        }
    }
    public void setHitting(bool state){
        isHitting = state;
    }
    public void setOnWall(bool state, bool wallKind){
        if(WallJumpUnlocked){
            isOnWall = state;
            isRightWall = wallKind;
        } else {
            isOnWall = false;
        }
        
        if(!state){
            isExtraJump = true;
        } else {
            dashesLeft = 1;
        }
    }
    public void HitUpImpulse(){
        rb.AddForce(new Vector2(0,1)*hitImpulse, ForceMode2D.Impulse);
    }
    public void HitLeftImpulse(){
        rb.AddForce(new Vector2(-1,0)*hitImpulse, ForceMode2D.Impulse);
    }
    public void HitRightImpulse(){
        rb.AddForce(new Vector2(1,0)*hitImpulse, ForceMode2D.Impulse);
    }
    public void receiveChanges(){
        sv = FindObjectOfType<Saves>();
        HitUnlocked = sv.HitUnlocked == 1;
        AirJumpUnlocked = sv.AirJumpUnlocked == 1;
        WallJumpUnlocked = sv.WallJumpUnlocked == 1;
        DashUnlocked = sv.DashUnlocked == 1;
        
        Debug.Log(HitUnlocked);
        Debug.Log(AirJumpUnlocked);
        Debug.Log(WallJumpUnlocked);
        Debug.Log(DashUnlocked);
        
    }
    public float getX(){
        return transform.position.x;
    }
    public float getY(){
        return transform.position.y;
    }
    public string getHit(){
        if(isHitting && wasHit){
            Debug.Log("hit send");
            if(isRight){
                wasHit = false;
                return "r";
            } else if (!isRight){
                wasHit = false;
                return "l";
            } else if(isDown){
                wasHit = false;
                return "d";
            } else {
                wasHit = false;
                return "0";
            }
            
        } else {
            return "0";
        }
        
    }
    public string isJump(){
        if(isJumping){
            return "1";
        } else {
            return "0";
        }
    }
    public void refreshKeyBinds()
    {
        downk = (KeyCode)Enum.Parse(typeof(KeyCode), sv.getKeyBind("DOWNKEY"));
        leftk = (KeyCode)Enum.Parse(typeof(KeyCode), sv.getKeyBind("LEFTKEY"));
        rightk = (KeyCode)Enum.Parse(typeof(KeyCode), sv.getKeyBind("RIGHTKEY"));
        jumpk = (KeyCode)Enum.Parse(typeof(KeyCode), sv.getKeyBind("JUMPKEY"));
        dashk = (KeyCode)Enum.Parse(typeof(KeyCode), sv.getKeyBind("DASHKEY"));
        hitk = (KeyCode)Enum.Parse(typeof(KeyCode), sv.getKeyBind("HITKEY"));
    }

}
