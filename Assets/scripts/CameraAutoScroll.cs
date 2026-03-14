using UnityEngine;
public class CameraAutoScroll : MonoBehaviour
{
    public float scrollSpeed = 2f;

    void Update()
    {
        // This moves the parent, which carries the camera and the collider
        transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
    }
}