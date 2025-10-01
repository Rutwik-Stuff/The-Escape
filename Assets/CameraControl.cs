using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public BoxCollider2D bounds;
    public Transform player;
    float yPos;
    float xPos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        yPos = player.position.y;
        xPos = player.position.x;

        float verExtent = Camera.main.orthographicSize;
        float horExtent = verExtent * Camera.main.aspect;

        yPos = Mathf.Clamp(yPos, bounds.bounds.min.y+verExtent, bounds.bounds.max.y - verExtent);
        xPos = Mathf.Clamp(xPos, bounds.bounds.min.x+horExtent, bounds.bounds.max.x - horExtent);

        transform.position = new Vector3(xPos, yPos, transform.position.z);
    
    }
}
