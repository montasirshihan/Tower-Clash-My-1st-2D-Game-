using UnityEngine;
public class AutoScroll : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        // This moves the parent (the Camera object), which carries 
        // the Cinemachine camera and the Collider together as one unit.
        transform.parent.position += Vector3.up * speed * Time.deltaTime;
    }
}