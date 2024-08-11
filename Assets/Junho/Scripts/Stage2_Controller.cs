using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage2_Controller : MonoBehaviour
{
    public GameObject Door1;
    public GameObject Door2;

    public GameObject button1;
    public GameObject button2;
    public GameObject Text1;
    private bool Clear;

    private Stage2_Door_Controller door_Controller1;
    private Stage2_Door_Controller door_Controller2;
    private ButtonController button_Controller1;
    private ButtonController button_Controller2;
    private float button1HoldTime;
    private float button2HoldTime;

    // Start is called before the first frame update
    void Start()
    {
        Clear = false;
        door_Controller1 = Door1.GetComponent<Stage2_Door_Controller>();
        door_Controller2 = Door2.GetComponent<Stage2_Door_Controller>();
        button_Controller1 = button1.GetComponent<ButtonController>();
        button_Controller2 = button2.GetComponent<ButtonController>();
        Text1.SetActive(false);

        button1HoldTime = 0f;
        button2HoldTime = 0f;


    }

    // Update is called once per frame
    void Update()
    {

        if (button_Controller1.Switch)
        {
            button1HoldTime += Time.deltaTime;
        }
        else
        {
            button1HoldTime = 0f;
        }

        if (button_Controller2.Switch)
        {
            button2HoldTime += Time.deltaTime;
        }
        else
        {
            button2HoldTime = 0f;
        }

        if (button1HoldTime >= 2.5f && button2HoldTime >= 2.5f)
        {
            Clear = true;
        }
        if (Clear)
        {
            door_Controller1.isOpen = true;
            door_Controller2.isOpen = true;
            door_Controller1.OpenDoor();
            door_Controller2.OpenDoor();
            button_Controller1.Switch = true;
            button_Controller2.Switch = true;
            Text1.SetActive(true);

        }


    }
}
