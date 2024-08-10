using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Vector3 initailPosition;
    private Quaternion initialRotation;

    void Start()
    {
        // �ʱ� ȸ�� �� ��ġ ����
        initailPosition = transform.position;
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // �ڽ� ������Ʈ ����
        transform.position = initailPosition;
        transform.rotation = initialRotation;
    }
}