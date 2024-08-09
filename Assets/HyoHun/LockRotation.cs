using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion initialRotation;

    void Start()
    {
        // �ʱ� ȸ���� �����մϴ�.
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // �ڽ� ������Ʈ�� ȸ���� �ʱ� ȸ������ �ǵ����ϴ�.
        transform.rotation = initialRotation;
    }
}