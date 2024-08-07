using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButton : MonoBehaviour
{

    public bool Switch;
    public GameObject ButtonFalse;
    public GameObject ButtonTrue;
    private Material TrueMaterial;
    private Material FalseMaterial;


    // Start is called before the first frame update
    void Start()
    {
        ButtonTrue.SetActive(false);
        ButtonFalse.SetActive(true);
        TrueMaterial = ButtonTrue.transform.GetComponent<Renderer>().material;
        FalseMaterial = ButtonFalse.transform.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Switch)
        {
            ButtonTrue.SetActive(true);
            ButtonFalse.SetActive(false);
        }
        else
        {
            ButtonTrue.SetActive(false);
            ButtonFalse.SetActive(true);
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
        yield return new WaitForSeconds(3);
        Switch = false;
    }

    private void OnCollisionEnter(Collision collision) // when it take some bugs, let's make it by triggerEnter.
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Prop")) // change name by Object????
        {
            Use();
        }
    }

}
