using UnityEngine;

public class Rope : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has the tag "Knife"
        if (other.CompareTag("Knife"))
        {
            // Notify the parent before destroying the Rope
            NotifyParent();

            // Destroy the Rope object
            Destroy(gameObject);
            Debug.Log("Rope has been cut by a knife and destroyed.");
        }
    }

    private void NotifyParent()
    {
        // Get the parent object's Rigidbody and modify its constraints
        Transform parentTransform = transform.parent;
        if (parentTransform != null)
        {
            Rigidbody parentRigidbody = parentTransform.GetComponent<Rigidbody>();
            if (parentRigidbody != null)
            {
                // Enable gravity
                parentRigidbody.useGravity = true;

                // Remove Y-axis position freeze
                parentRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;

                Debug.Log("Gravity enabled and Y position freeze removed for the parent object: " + parentTransform.name);
            }
        }
    }
}
