using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public float posx, posy;
    public string sceneName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.CompareTag("Player")){
            Movement.startPosX = posx;
            Movement.startPosY = posy;
            SceneManager.LoadScene(sceneName);
        }
    }
}
