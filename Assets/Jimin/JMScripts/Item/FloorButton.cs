using UnityEngine;

public class FloorButton : MonoBehaviour
{
    public MoveFloor moveFloor; // Reference to the MoveFloor script

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger area
        if (other.CompareTag("Player"))
        {
            // Notify the platform to start descending
            if (moveFloor != null)
            {
                Debug.Log("Player stepped on the platform");
                moveFloor.SetSteppedOn(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player has exited the trigger area
        if (other.CompareTag("Player"))
        {
            // Notify the platform to start ascending quickly
            if (moveFloor != null)
            {
                Debug.Log("Player stepped off the platform");
                moveFloor.SetSteppedOn(false);
            }
        }
    }
}
