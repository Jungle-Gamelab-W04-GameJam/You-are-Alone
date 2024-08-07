using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JiminLeverController : MonoBehaviour
{

    public bool Switch;
    public GameObject LeverDown;
    public GameObject LeverUp;
    public GameObject Cube;
    public GameObject Door1;
    public GameObject Door2;
    private Jimin_Door_Controller door_Controller1;
    private Jimin_Door_Controller door_Controller2;

    private Material cubeMaterial;


    // Start is called before the first frame update
    void Start()
    {
        LeverDown.SetActive(false);
        LeverUp.SetActive(true);
        cubeMaterial = Cube.transform.GetComponent<Renderer>().material;
        door_Controller1 = Door1.GetComponent<Jimin_Door_Controller>();
        door_Controller2 = Door2.GetComponent<Jimin_Door_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Switch)
        {
            LeverDown.SetActive(true);
            LeverUp.SetActive(false);
            door_Controller1.OpenDoor();
            door_Controller2.OpenDoor();

            cubeMaterial.color = Color.green;
        }
        else
        {
            LeverDown.SetActive(false);
            LeverUp.SetActive(true);
            door_Controller1.CloseDoor();
            door_Controller2.CloseDoor();

            cubeMaterial.color = Color.red;
        }

    }

    public void Use()
    {
        Switch = !Switch;
    }
}
