using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2_Door_Controller : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;

    public bool isOpen = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;
        targetRotation = initialRotation * Quaternion.Euler(0, openAngle, 0);
    }

    // Update is called once per frame
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