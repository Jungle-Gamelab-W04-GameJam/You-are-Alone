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
    public AudioClip openSound;  // Audio clip for opening sound
    public AudioSource audioSource; // Reference to the AudioSource component

    private Jimin_Door_Controller door_Controller1;
    private Jimin_Door_Controller door_Controller2;
    private Material cubeMaterial;

    void Start()
    {
        LeverDown.SetActive(false);
        LeverUp.SetActive(true);
        cubeMaterial = Cube.transform.GetComponent<Renderer>().material;
        door_Controller1 = Door1.GetComponent<Jimin_Door_Controller>();
        door_Controller2 = Door2.GetComponent<Jimin_Door_Controller>();

        // Get the AudioSource component attached to the lever
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from the lever object.");
        }
    }

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
        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }
    }


}
