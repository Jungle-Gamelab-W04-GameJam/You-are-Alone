using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    public float lowerYPosition = -10.0f; // The target Y position for the platform to move down to
    public float moveSpeed = 2.0f; // The speed at which the platform moves
    private Vector3 originalPosition; // The original position of the platform
    private Rigidbody floorRigidbody; // The Rigidbody component of the platform
    public bool isSteppedOn = false; // Indicates whether the platform is being stepped on

    void Start()
    {
        // Store the original position of the platform
        originalPosition = transform.position;

        // Get the Rigidbody component
        floorRigidbody = GetComponent<Rigidbody>();

        if (floorRigidbody == null)
        {
            Debug.LogError("MoveFloor object does not have a Rigidbody component.");
            return;
        }

        // Initially freeze all position and rotation constraints
        floorRigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Update()
    {
        if (isSteppedOn)
        {
            MoveDown();
        }
        else
        {
            MoveUp();
        }
    }

    public void MoveDown()
    {
        // Unfreeze the Y position constraint to allow movement
        floorRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        // Calculate the target position
        Vector3 targetPosition = new Vector3(originalPosition.x, lowerYPosition, originalPosition.z);

        // Move the platform towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Stop movement when the platform reaches the target position
        if (Mathf.Abs(transform.position.y - lowerYPosition) < 0.01f)
        {
            // Freeze the platform in place when it reaches the target position
            floorRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public void MoveUp()
    {
        // Unfreeze the Y position constraint to allow movement
        floorRigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;

        // Move the platform back to the original position
        transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed * Time.deltaTime);

        // Stop movement when the platform reaches the original position
        if (Vector3.Distance(transform.position, originalPosition) < 0.01f)
        {
            // Freeze all position constraints once the platform returns to its original position
            floorRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    // Public method to set the stepped-on state
    public void SetSteppedOn(bool steppedOn)
    {
        isSteppedOn = steppedOn;
    }
}