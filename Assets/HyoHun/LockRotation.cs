using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Vector3 initailPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // 초기 회전 및 위치 저장
        initailPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // 자식 오브젝트 고정
        transform.position = initailPosition;
        transform.rotation = initialRotation;
    }
}