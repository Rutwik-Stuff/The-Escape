using UnityEngine;

public class PlayerListController : MonoBehaviour
{
    public PlayerEntry p1;
    public PlayerEntry p2;
    public PlayerEntry p3;
    public PlayerEntry p4;
    public PlayerEntry p5;

    public void showPlayerTab(){
        gameObject.SetActive(true);
        p1.showIfNeeded();
        p2.showIfNeeded();
        p3.showIfNeeded();
        p4.showIfNeeded();
        p5.showIfNeeded();

    }
    public void hidePlayerTab(){
        gameObject.SetActive(false);
    }
}
