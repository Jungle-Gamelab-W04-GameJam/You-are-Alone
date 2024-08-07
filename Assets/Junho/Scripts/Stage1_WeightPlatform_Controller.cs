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





    private void OnTriggerEnter(Collider collision)
    {
        if (IsTriggerValid(collision))
        {
            doorController.OpenDoor();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (IsTriggerValid(collision))
        {
            doorController.CloseDoor();
        }
    }

    private bool IsTriggerValid(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Prop") || (collision.gameObject.transform.parent != null && collision.gameObject.transform.parent.CompareTag("Player")))
        {
            return true;
        }
        return false;
    }
}
