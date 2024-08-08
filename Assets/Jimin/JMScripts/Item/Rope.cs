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
        // Get the parent object's MoveFloor component and trigger MoveDown
        Transform parentTransform = transform.parent;
        if (parentTransform != null)
        {
            MoveFloor moveFloor = parentTransform.GetComponent<MoveFloor>();
            if (moveFloor != null)
            {
                // Directly set isSteppedOn to true to simulate stepping on
                moveFloor.SetSteppedOn(true);

                // Optionally, directly call MoveDown() if needed
                // moveFloor.MoveDown();

                Debug.Log("Parent notified to move down: " + parentTransform.name);
            }
            else
            {
                Debug.LogError("No MoveFloor component found on parent.");
            }
        }
    }
}
