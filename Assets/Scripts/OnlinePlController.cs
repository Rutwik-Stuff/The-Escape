using UnityEngine;

public class OnlinePlController : MonoBehaviour
{
    public GameObject leftAnim;
    public GameObject rightAnim;
    public GameObject downAnim;

    public GameObject jumpAnim;

    public float speed = 5f;

    private float x;
    private float y;

    private string nick;

    

    public void setCoords(float x, float y){
        this.x = x;
        this.y = y;
    }
    public void toggleHitAnim(string code){
        if(code=="l"){
            leftAnim.SetActive(true);
        } else if(code == "r"){
            rightAnim.SetActive(true);
        } else if(code == "d"){
            downAnim.SetActive(true);
        }
    }
    public void toggleJumpAnim(string code){
        if(code == "j"){
            jumpAnim.SetActive(true);
        } else {
            jumpAnim.SetActive(false);
        }
    }
    public void stopAllAnims(){
        leftAnim.SetActive(false);
        rightAnim.SetActive(false);
        downAnim.SetActive(false);
    }

    public void processMovement(){
        Vector2 target = new Vector2(x, y);
        Vector2 current = transform.position;
        current  = Vector2.MoveTowards(current, target, speed * Time.deltaTime);
        transform.position = current;
    }
}
