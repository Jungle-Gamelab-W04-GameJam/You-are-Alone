using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Test4_WeightCanvasController : MonoBehaviour
{
    public GameObject woodFloor;
    private MoveFloor moveFloor;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text.gameObject.SetActive(false);
        moveFloor = woodFloor.transform.GetComponent<MoveFloor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveFloor.isSteppedOn == true)
        {
            text.gameObject.SetActive(true);
        }
        else
        {
            text.gameObject.SetActive(false);
        }

    }
}
