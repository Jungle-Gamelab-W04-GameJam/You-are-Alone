using UnityEngine;

public class Window : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object we collided with has the tag "Heavy Tool"
        if (collision.collider.CompareTag("Heavy Tool"))
        {
            // Destroy the Window object
            Destroy(gameObject);
        }
    }
}
