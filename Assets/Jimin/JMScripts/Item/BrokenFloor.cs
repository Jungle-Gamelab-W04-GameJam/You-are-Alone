using UnityEngine;

public class BrokenFloor : MonoBehaviour
{
    public float tiltAngle = -5.0f; // Amount to tilt each time
    public int maxCollisions = 3; // Number of collisions to fully break the floor
    private int collisionCount = 0; // Current number of collisions
    private Rigidbody floorRigidbody; // The Rigidbody component of the floor
    public AudioClip collisionSound; // Sound to play on collision
    private AudioSource audioSource; // Audio source component

    void Start()
    {
        // Get the Rigidbody component
        floorRigidbody = GetComponent<Rigidbody>();

        if (floorRigidbody == null)
        {
            Debug.LogError("BrokenFloor object does not have a Rigidbody component.");
            return;
        }

        // Initially freeze all position and rotation constraints
        floorRigidbody.constraints = RigidbodyConstraints.FreezeAll;

        // Get or add the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object has the tag "Ball" or "Camcorder"
        if (collision.gameObject.CompareTag("Ball") || collision.gameObject.CompareTag("Camcorder"))
        {
            // Play the collision sound
            if (collisionSound != null)
            {
                audioSource.PlayOneShot(collisionSound);
            }

            collisionCount++;

            if (collisionCount < maxCollisions)
            {
                // Temporarily unfreeze x-axis rotation to apply tilt
                floorRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

                // Apply tilt on the x-axis
                transform.Rotate(Vector3.right * tiltAngle);

                // Reapply all constraints to lock x-axis rotation again
                floorRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                // Fully unlock all position and rotation constraints
                floorRigidbody.constraints = RigidbodyConstraints.None;
                Debug.Log("Floor is now fully broken and all constraints are removed.");

                // Destroy the object after a delay to ensure any sound or effects are complete
                Destroy(gameObject, collisionSound.length);
            }
        }
    }
}
