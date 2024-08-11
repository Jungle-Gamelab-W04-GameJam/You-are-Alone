using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage7_Door1 : MonoBehaviour
{
    [SerializeField] float openHeight = 3f;
    [SerializeField] float openSpeed = 1f;

    public bool isOpen = false;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    //make Door Stuck
    public bool isStuck = false;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        targetPosition = initialPosition + new Vector3(0, openHeight, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * openSpeed);
        }
        else if(!isStuck)
        {
            transform.position = Vector3.Lerp(transform.position, initialPosition, Time.deltaTime * openSpeed);
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
    }

    public void CloseDoor()
    {
        isOpen = false;
    }
}