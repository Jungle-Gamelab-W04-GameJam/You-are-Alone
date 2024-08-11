using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public float buttonTime = 2f;

    public bool Switch;
    public GameObject ButtonFalse;
    public GameObject ButtonTrue;
    public GameObject Door;

    private MonoBehaviour doorControl;
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
        doorControl = Door.GetComponent<Stage2_Door_Controller>() as MonoBehaviour
              ?? Door.GetComponent<Stage7_Door1>() as MonoBehaviour
              ?? Door.GetComponent<Jimin_Door_Controller>() as MonoBehaviour;

        if(doorControl != null)
        {
            Debug.Log(("문 컨트롤러 찾음 : " + doorControl.GetType().Name));
        } else
        {
            Debug.Log("문 스크립트 존재하지 않음");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Switch)
        {
            ButtonTrue.SetActive(true);
            ButtonFalse.SetActive(false);
            if (doorControl != null)
            {
                Debug.Log("문 컨트롤러 있음");
                // Stage2_Door_Controller인지 확인한 후, 다운캐스팅하여 접근
                if (doorControl is Stage2_Door_Controller st2Controller)
                {
                    st2Controller.OpenDoor();
                }
                else if (doorControl is Stage7_Door1 st7Controller)
                {
                    Debug.Log("문 열기 호출됨");
                    st7Controller.OpenDoor();
                }
                else if (doorControl is Jimin_Door_Controller jmController)
                {
                    jmController.OpenDoor();
                }
            }
        }
        else
        {
            ButtonTrue.SetActive(false);
            ButtonFalse.SetActive(true);
            if (doorControl != null)
            {
                // Stage2_Door_Controller인지 확인한 후, 다운캐스팅하여 접근
                if (doorControl is Stage2_Door_Controller st2Controller)
                {
                    st2Controller.CloseDoor();
                }
                else if (doorControl is Stage7_Door1 st7Controller)
                {
                    st7Controller.CloseDoor();
                }
                else if (doorControl is Jimin_Door_Controller jmController)
                {
                    jmController.CloseDoor();
                }
            }
        }


    }
    public void Use()
    {
        if (!Switch) // Play only when Switch is false.
        {
            Debug.Log("버튼 눌림");
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
