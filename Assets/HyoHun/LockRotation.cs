using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // 초기 회전을 저장합니다.
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // 자식 오브젝트의 회전을 초기 회전으로 되돌립니다.
        transform.rotation = initialRotation;
    }
}