using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayInteract : MonoBehaviour
{
    //������ �߻��� ī�޶�
    private Camera playerCam;
    //������ ����
    [SerializeField]float distance = 20f;
    //������ �˻��� ���̾�
    public LayerMask whatIsTarget;

    //��ü ��ȣ�ۿ�� �������� ��ġ����
    private Transform moveTarget;
    //��ü ��ȣ�ۿ�� �Ÿ������� ���� �Ҵ���� �Ÿ���
    private float targetDistance;

    //���� �Է�
    private StarterAssetsInputs _input;

    //��� ���� ������Ʈ
    public GameObject holdingProp;

    [SerializeField] float pickUpOffset = 2f;

    private void Start()
    {
        //����ĳ��Ʈ�� ī�޶� ����ī�޶�� �Ҵ�
        playerCam = Camera.main;
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (_input.interact)
        {
            OnInteract();
            _input.interact = false; // �Է� ó���� �Ϸ�� �� interact ���¸� �ʱ�ȭ
        }

        if (holdingProp != null)
        {
            Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            Vector3 rayDir = playerCam.transform.forward;
            Ray ray = new Ray(rayOrigin, rayDir);
            Debug.DrawRay(ray.origin, ray.direction * 20f, Color.green);

            // �浹 ������Ʈ�� ��ġ���� "������ ������(ī�޶� ���߾�) + (���� ���� x ��ü���� �Ÿ�)" �� �Ҵ�
            moveTarget.position = ray.origin + ray.direction * targetDistance;
        }
    }

    private void OnInteract()
    {
        if (holdingProp == null)
        {
            Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            Vector3 rayDir = playerCam.transform.forward;
            Ray ray = new Ray(rayOrigin, rayDir);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distance, whatIsTarget))
            {
                // �浹 ���ӿ�����Ʈ�� hitTarget�� ����
                GameObject hitTarget = hit.collider.gameObject;

                // �浹 ������Ʈ�� ��ġ���� moveTarget�� ����
                moveTarget = hitTarget.transform;
                // �浹 ��ü������ �Ÿ��� targetDistance�� ����
                //targetDistance = hit.distance;
                targetDistance = pickUpOffset;

                // ��� �ִ� ������Ʈ�� �Ҵ�
                holdingProp = hitTarget;

                /*
                if(targetDistance < pickUpOffset)
                {
                    targetDistance = pickUpOffset;
                }
*/
                Debug.Log("Picked up: " + holdingProp.name);
            }
        }
        else
        {
            // ��� �ִ� ��ü�� ����߸�
            //��ü�� ����߸��� ���� �ӵ��� �ʱ�ȭ�ؼ� ��� ����������
            Rigidbody holdingRb = holdingProp.GetComponent<Rigidbody>();
            holdingRb.velocity = Vector3.zero;

            holdingProp = null;
            moveTarget = null;

            Debug.Log("Dropped the object.");
        }
    }
}