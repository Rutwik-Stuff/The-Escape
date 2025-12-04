using UnityEngine;

public class rightAnim : MonoBehaviour
{
    public GameObject right;
    public Movement mv;
    void stopAnim(){
        mv.setHitting(false);
        right.SetActive(false);
    }
}
