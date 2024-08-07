using UnityEngine;

public class BrokenFloor : MonoBehaviour
{
    public float tiltAngle = -5.0f; // Amount to tilt each time
    public int maxCollisions = 3; // Number of collisions to fully break the floor
    private int collisionCount = 0; // Current number of collisions
    private Rigidbody floorRigidbody; // The Rigidbody component of the floor
    private bool isColliding = false; // Track if currently colliding

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
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object has the tag "Ball" and if not already colliding
        if (collision.gameObject.CompareTag("Ball") && !isColliding)
        {
            isColliding = true; // Set collision flag
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
            else if (collisionCount == maxCollisions)
            {
                // Destroy the child object before enabling gravity

                Destroy(gameObject);



                // Fully unlock all position and rotation constraints
                floorRigidbody.constraints = RigidbodyConstraints.None;
                Debug.Log("Floor is now fully broken and all constraints are removed.");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Reset collision flag when the object leaves
        if (collision.gameObject.CompareTag("Ball"))
        {
            isColliding = false;
        }
    }
}
