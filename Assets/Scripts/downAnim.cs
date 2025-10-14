using UnityEngine;

public class downAnim : MonoBehaviour
{
    public GameObject down;
    public Movement mv;
    void stopAnim(){
        down.SetActive(false);
        mv.setHitting(false);
    }
}
