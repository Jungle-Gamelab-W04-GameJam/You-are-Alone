using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckCheck : MonoBehaviour
{
    private BoxCollider boxCol;

    private MonoBehaviour doorControl;

    private void Start()
    {
        // 부모 오브젝트에서 각 문 스크립트를 시도
        doorControl = GetComponentInParent<Stage2_Door_Controller>() as MonoBehaviour
                      ?? GetComponentInParent<Stage7_Door1>() as MonoBehaviour
                      ?? GetComponentInParent<Jimin_Door_Controller>() as MonoBehaviour;

        if (doorControl != null)
        {
            Debug.Log("문 컨트롤러를 찾았습니다: " + doorControl.GetType().Name);
        }
        else
        {
            Debug.Log("부모 오브젝트에 문 컨트롤러가 없습니다.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Prop")
        {
            Debug.Log(other.gameObject.name);
            if (doorControl != null)
            {
                // Stage2_Door_Controller인지 확인한 후, 다운캐스팅하여 접근
                if (doorControl is Stage2_Door_Controller st2Controller)
                {
                    st2Controller.isStuck = true;
                }
                else if (doorControl is Stage7_Door1 st7Controller)
                {
                    st7Controller.isStuck = true;
                }
                else if (doorControl is Jimin_Door_Controller jmController)
                {
                    jmController.isStuck = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Prop")
        {
            if (doorControl != null)
            {
                // Stage2_Door_Controller인지 확인한 후, 다운캐스팅하여 접근
                if (doorControl is Stage2_Door_Controller st2Controller)
                {
                    st2Controller.isStuck = false;
                }
                else if (doorControl is Stage7_Door1 st7Controller)
                {
                    st7Controller.isStuck = false;
                }
                else if (doorControl is Jimin_Door_Controller jmController)
                {
                    jmController.isStuck = false;
                }
            }
        }
    }
}
