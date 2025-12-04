using UnityEngine;
using System;

public class leftAnim : MonoBehaviour
{
    public GameObject left;
    public Movement mv;
    void stopAnim(){
        left.SetActive(false);
        mv.setHitting(false);
    }
}
