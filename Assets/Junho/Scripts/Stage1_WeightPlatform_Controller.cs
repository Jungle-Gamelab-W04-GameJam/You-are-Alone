using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage1_WeightPlatform_Controller : MonoBehaviour
{
    private Stage2_Door_Controller doorController;
    [SerializeField]
    private GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        doorController = door.GetComponent<Stage2_Door_Controller>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Landing");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Prop") || collision.gameObject.CompareTag("Player"))
        {
            doorController.OpenDoor();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Leaving");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Prop") || collision.gameObject.CompareTag("Player"))
        {
            doorController.CloseDoor();
        }
    }
}
