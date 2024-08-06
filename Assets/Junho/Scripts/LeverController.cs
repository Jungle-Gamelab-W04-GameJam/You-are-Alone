using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{

    public bool Switch;
    public GameObject LeverDown;
    public GameObject LeverUp;
    public GameObject Cube;

    private Material cubeMaterial;


    // Start is called before the first frame update
    void Start()
    {
        LeverDown.SetActive(false);
        LeverUp.SetActive(true);
        cubeMaterial = Cube.transform.GetComponent<Renderer>().material;

    }

    // Update is called once per frame
    void Update()
    {
        if (Switch)
        {
            LeverDown.SetActive(true);
            LeverUp.SetActive(false);

            cubeMaterial.color = Color.green;
        }
        else
        {
            LeverDown.SetActive(false);
            LeverUp.SetActive(true);

            cubeMaterial.color = Color.red;

        }

    }
}
