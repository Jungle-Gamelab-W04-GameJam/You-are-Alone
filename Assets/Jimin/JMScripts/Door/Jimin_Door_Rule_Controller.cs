using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jimin_Door_Rule_Controller : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;

    private bool Clear;

    private Jimin_Door_Controller door_Controller1;
    private Jimin_Door_Controller door_Controller2;
    // Start is called before the first frame update
    void Start()
    {
        Clear = false;
        door_Controller1 = Door1.GetComponent<Jimin_Door_Controller>();
        door_Controller2 = Door2.GetComponent<Jimin_Door_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (door_Controller1.isOpen && door_Controller2.isOpen)
        {
            Clear = true;
        }

        if (Clear)
        {
            door_Controller1.isOpen = true;
            door_Controller2.isOpen = true;
        }

    }
}
