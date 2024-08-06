using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;

    private bool isOpen = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation * Quaternion.Euler(0, openAngle, 0);
    }

    private void Update()
    {
        if (isOpen)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * openSpeed);
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
        Debug.Log("Door is opening");
    }

    public void CloseDoor()
    {
        isOpen = false;
        Debug.Log("Door is closing");
    }
}
