using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckCheck : MonoBehaviour
{
    private BoxCollider boxCol;

    private MonoBehaviour doorControl;

    private void Start()
    {
        // �θ� ������Ʈ���� �� �� ��ũ��Ʈ�� �õ�
        doorControl = GetComponentInParent<Stage2_Door_Controller>() as MonoBehaviour
                      ?? GetComponentInParent<Stage7_Door1>() as MonoBehaviour
                      ?? GetComponentInParent<Jimin_Door_Controller>() as MonoBehaviour;

        if (doorControl != null)
        {
            Debug.Log("�� ��Ʈ�ѷ��� ã�ҽ��ϴ�: " + doorControl.GetType().Name);
        }
        else
        {
            Debug.Log("�θ� ������Ʈ�� �� ��Ʈ�ѷ��� �����ϴ�.");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Prop")
        {
            Debug.Log(other.gameObject.name);
            if (doorControl != null)
            {
                // Stage2_Door_Controller���� Ȯ���� ��, �ٿ�ĳ�����Ͽ� ����
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
                // Stage2_Door_Controller���� Ȯ���� ��, �ٿ�ĳ�����Ͽ� ����
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
