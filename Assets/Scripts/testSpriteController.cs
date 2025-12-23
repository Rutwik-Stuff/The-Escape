using UnityEngine;

public class testSpriteController : MonoBehaviour
{
    public void setCoords(float x, float y){
        transform.position = new Vector2(x, y);
    }
}
