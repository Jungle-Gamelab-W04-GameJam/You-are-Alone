using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutDownLever : MonoBehaviour
{
    public bool Switch;
    public GameObject LeverDown;
    public GameObject LeverUp;
    public GameObject Door3;
    public GameObject Door4;
    public GameObject Door5;
    public GameObject Door6;

    public AudioClip openSound;  // Audio clip for opening sound
    public AudioSource audioSource; // Reference to the AudioSource component
    private bool doorsDestroyed = false; // Flag to check if doors are already destroyed

    void Start()
    {
        LeverDown.SetActive(false);
        LeverUp.SetActive(true);

        // Get the AudioSource component attached to the lever
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from the lever object.");
        }
    }

    void Update()
    {
        // Check if the switch is on and the doors haven't been destroyed yet
        if (Switch && !doorsDestroyed)
        {
            LeverDown.SetActive(true);
            LeverUp.SetActive(false);

            // Destroy the doors
            if (Door3 != null)
            {
                Destroy(Door3);
                Debug.Log("Door3 destroyed");
            }

            if (Door4 != null)
            {
                Destroy(Door4);
                Debug.Log("Door4 destroyed");
            }
            if (Door5 != null)
            {
                Destroy(Door5);
                Debug.Log("Door3 destroyed");
            }

            if (Door6 != null)
            {
                Destroy(Door6);
                Debug.Log("Door4 destroyed");
            }


            doorsDestroyed = true; // Mark doors as destroyed
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
