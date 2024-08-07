using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_Controller : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;

    private bool Clear;

    private Stage2_Door_Controller door_Controller1;
    private Stage2_Door_Controller door_Controller2;
    // Start is called before the first frame update
    void Start()
    {
        Clear = false;
        door_Controller1 = Door1.GetComponent<Stage2_Door_Controller>();
        door_Controller2 = Door2.GetComponent<Stage2_Door_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Clear : " + Clear);
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
