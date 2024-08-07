using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stage4_LeverController : MonoBehaviour
{
    public GameObject Door;

    private LeverController lever_Controller;
    private Stage2_Door_Controller door_Controller;

    // Start is called before the first frame update
    void Start()
    {
        door_Controller = Door.GetComponent<Stage2_Door_Controller>();
        lever_Controller = gameObject.GetComponent<LeverController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lever_Controller.Switch)
        {
            door_Controller.OpenDoor();
        }
        else
        {
            door_Controller.CloseDoor();
        }


    }

}