using UnityEngine;

public class InteractionButton : MonoBehaviour
{
    public float doorOpenDuration = 5f;
    private DoorController doorController;
    private bool isThrown = false;
    private float throwTime;

    void Start()
    {
        FindDoor();
    }

    void FindDoor()
    {
        GameObject doorObject = GameObject.FindGameObjectWithTag("InteractableDoor");
        if (doorObject != null)
        {
            doorController = doorObject.GetComponent<DoorController>();
            if (doorController == null)
            {
                Debug.LogError("Door object does not have a DoorController component!");
            }
        }
        else
        {
            Debug.LogError("No object with InteractableDoor tag found in the scene!");
        }
    }

    public void OnPickUp()
    {
        if (doorController != null)
        {
            doorController.OpenDoor(doorOpenDuration);
        }
    }

    public void OnDrop()
    {
        if (doorController != null)
        {
            doorController.CloseDoor();
        }
    }

    public void OnThrow()
    {
        isThrown = true;
        throwTime = Time.time;
    }

    void Update()
    {
        if (isThrown && Time.time - throwTime > 5f)
        {
            GameObject.FindObjectOfType<SphereGenerator>().DestroySphere();
            Destroy(gameObject);
        }
    }
}