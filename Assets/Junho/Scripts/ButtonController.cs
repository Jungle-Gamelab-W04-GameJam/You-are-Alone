using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public float buttonTime = 3f;

    public bool Switch;
    public GameObject ButtonFalse;
    public GameObject ButtonTrue;
    public GameObject Door;

    private Stage2_Door_Controller doorController;
    private Material TrueMaterial;
    private Material FalseMaterial;


    // Start is called before the first frame update
    void Start()
    {
        ButtonTrue.SetActive(false);
        ButtonFalse.SetActive(true);
        TrueMaterial = ButtonTrue.transform.GetComponent<Renderer>().material;
        FalseMaterial = ButtonFalse.transform.GetComponent<Renderer>().material;
        TrueMaterial.color = Color.green;
        FalseMaterial.color = Color.red;
        doorController = Door.GetComponent<Stage2_Door_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Switch)
        {
            ButtonTrue.SetActive(true);
            ButtonFalse.SetActive(false);
            doorController.OpenDoor();
        }
        else
        {
            ButtonTrue.SetActive(false);
            ButtonFalse.SetActive(true);
            doorController.CloseDoor();
        }


    }
    public void Use()
    {
        if (!Switch) // Play only when Switch is false.
        {
            StartCoroutine(SwitchOnCoroutine());
        }
    }

    private IEnumerator SwitchOnCoroutine()
    {
        Switch = true;
        yield return new WaitForSeconds(buttonTime);
        Switch = false;
    }

    private void OnTriggerEnter(Collider collision) // when it take some bugs, let's make it by triggerEnter.
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Prop")) // change name by Object????
        {
            Use();
        }
    }

}
