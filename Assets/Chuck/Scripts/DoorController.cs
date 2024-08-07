using UnityEngine;
using System.Collections;

public class DoorController : MonoBehaviour
{
    public float openSpeed = 2f;
    public float openAngle = 90f;

    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private bool isOpen = false;
    private bool isLockedOpen = false;

    void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation * Quaternion.Euler(0, openAngle, 0);
    }

    public void OpenDoor(float duration = 0f)
    {
        isOpen = true;
        if (duration > 0 && !isLockedOpen)
        {
            StartCoroutine(CloseDoorAfterDelay(duration));
        }
    }

    public void CloseDoor()
    {
        if (!isLockedOpen)
        {
            isOpen = false;
        }
    }

    public void LockOpenDoor()
    {
        isOpen = true;
        isLockedOpen = true;
    }

    IEnumerator CloseDoorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CloseDoor();
    }

    void Update()
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
}