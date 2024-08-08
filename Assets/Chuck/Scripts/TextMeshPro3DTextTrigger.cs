using UnityEngine;
using TMPro;

public class TextMeshPro3DTextTrigger : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;
    public float floatDistance = 1f;

    private bool isPlayerInside = false;
    private float currentAlpha = 0f;
    private Vector3 startPosition;
    private Vector3 endPosition;

    private void Start()
    {
        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshPro is not assigned!");
            return;
        }

        // ���� ��ġ ����
        startPosition = textMeshPro.transform.position;
        endPosition = startPosition + Vector3.up * floatDistance;

        // �ʱ� �ؽ�Ʈ ����
        textMeshPro.alpha = 0f;

        Debug.Log("TextMeshPro3DTextTrigger initialized. Start position: " + startPosition);
    }

    private void Update()
    {
        if (textMeshPro == null) return;

        if (isPlayerInside && currentAlpha < 1f)
        {
            currentAlpha += Time.deltaTime / fadeInDuration;
        }
        else if (!isPlayerInside && currentAlpha > 0f)
        {
            currentAlpha -= Time.deltaTime / fadeOutDuration;
        }

        currentAlpha = Mathf.Clamp01(currentAlpha);

        // �ؽ�Ʈ ���İ��� ��ġ ������Ʈ
        textMeshPro.alpha = currentAlpha;
        textMeshPro.transform.position = Vector3.Lerp(startPosition, endPosition, currentAlpha);

        Debug.Log("Current text position: " + textMeshPro.transform.position + ", Alpha: " + currentAlpha);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            Debug.Log("Player entered trigger zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            Debug.Log("Player exited trigger zone.");
        }
    }
}