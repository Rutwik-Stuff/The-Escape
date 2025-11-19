using UnityEngine;

public class GetSkills : MonoBehaviour
{
    private Saves sv;
    public string skill;
    public int Air_AirJump__Hit_HittingAbility__Wall_WallJUMP__Dash_Dashing;
    void Start(){
        sv = FindObjectOfType<Saves>();
        if(sv.HitUnlocked == 1){
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.CompareTag("Player")){
            if(skill == "Hit"){
                sv.HitUnlocked = 1;
                gameObject.SetActive(false);
            } else if(skill == "Wall"){
                sv.WallJumpUnlocked = 1;
            } else if(skill == "Air"){
                sv.AirJumpUnlocked = 1;
            } else if(skill == "Dash"){
                sv.DashUnlocked = 1;
            }
            sv.reload();
        }
    }
}
