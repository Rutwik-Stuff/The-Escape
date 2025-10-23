using UnityEngine;
using UnityEngine.SceneManagement;

public class WholeBodyCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
{
    if(collision.gameObject.CompareTag("death")){
        SceneManager.LoadScene("SampleScene");
    }
}

}
