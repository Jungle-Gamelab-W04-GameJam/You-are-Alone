using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object's tag is "Chest"
        if (collision.collider.CompareTag("Chest"))
        {
            // Get the Chest script from the collided object
            Chest chest = collision.collider.GetComponent<Chest>();
            if (chest != null)
            {
                // Call the Unlock method on the Chest script
                chest.Unlock();

                // Destroy the Key object
                Destroy(gameObject);
            }
        }
    }
}
