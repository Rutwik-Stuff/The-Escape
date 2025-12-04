using UnityEngine;
using UnityEngine.SceneManagement;

public class WholeBodyCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
{
    if(collision.gameObject.CompareTag("death") || collision.gameObject.CompareTag("Hit_Death")){
        FindObjectOfType<MainLogic>().death();
    }
}

}
