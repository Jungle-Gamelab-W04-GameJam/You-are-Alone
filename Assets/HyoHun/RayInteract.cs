using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayInteract : MonoBehaviour
{
    //광선을 발사할 카메라
    private Camera playerCam;
    //광선의 길이
    [SerializeField]float distance = 20f;
    //광선이 검사할 레이어
    public LayerMask whatIsTarget;

    //물체 상호작용시 갱신해줄 위치정보
    private Transform moveTarget;
    //물체 상호작용시 거리유지를 위해 할당받을 거리값
    private float targetDistance;

    //들어올 입력
    private StarterAssetsInputs _input;

    //들고 있을 오브젝트
    public GameObject holdingProp;

    [SerializeField] float pickUpOffset = 2f;

    private void Start()
    {
        //레이캐스트할 카메라에 메인카메라로 할당
        playerCam = Camera.main;
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (_input.interact)
        {
            OnInteract();
            _input.interact = false; // 입력 처리가 완료된 후 interact 상태를 초기화
        }

        if (holdingProp != null)
        {
            Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            Vector3 rayDir = playerCam.transform.forward;
            Ray ray = new Ray(rayOrigin, rayDir);
            Debug.DrawRay(ray.origin, ray.direction * 20f, Color.green);

            // 충돌 오브젝트의 위치값을 "광선의 시작점(카메라 정중앙) + (광선 방향 x 물체까지 거리)" 로 할당
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
                // 충돌 게임오브젝트를 hitTarget에 저장
                GameObject hitTarget = hit.collider.gameObject;

                // 충돌 오브젝트의 위치값을 moveTarget에 저장
                moveTarget = hitTarget.transform;
                // 충돌 물체까지의 거리를 targetDistance에 저장
                //targetDistance = hit.distance;
                targetDistance = pickUpOffset;

                // 들고 있는 오브젝트로 할당
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
            // 들고 있는 물체를 떨어뜨림
            //물체를 떨어뜨리기 전에 속도를 초기화해서 살살 떨어지도록
            Rigidbody holdingRb = holdingProp.GetComponent<Rigidbody>();
            holdingRb.velocity = Vector3.zero;

            holdingProp = null;
            moveTarget = null;

            Debug.Log("Dropped the object.");
        }
    }
}