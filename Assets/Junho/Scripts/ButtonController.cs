using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
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
        TrueMaterial.color = Color.green;
        FalseMaterial.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        if (Switch)
        {
            ButtonTrue.SetActive(false);
            ButtonFalse.SetActive(true);
        }
        else
        {
            ButtonTrue.SetActive(true);
            ButtonFalse.SetActive(false);
        }

    }
}
