using UnityEngine;

public class BrokenFloor2 : MonoBehaviour
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
        // Check if the object is in the "props" layer and if not already colliding
        if (collision.gameObject.layer == LayerMask.NameToLayer("Prop") && !isColliding)
        {
            isColliding = true; // Set collision flag
            collisionCount++;

            if (collisionCount < maxCollisions)
            {
                // Temporarily unfreeze y-axis rotation to apply tilt
                floorRigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

                // Apply tilt on the y-axis
                transform.Rotate(Vector3.up * tiltAngle);

                // Reapply all constraints to lock y-axis rotation again
                floorRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
            else if (collisionCount == maxCollisions)
            {
                // Fully unlock all position and rotation constraints
                floorRigidbody.constraints = RigidbodyConstraints.None;
                // Destroy the floor object itself
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Reset collision flag when the object leaves
        if (collision.gameObject.layer == LayerMask.NameToLayer("Prop"))
        {
            isColliding = false;
        }
    }
}