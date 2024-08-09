using UnityEngine;

public class Simple3DTextTrigger : MonoBehaviour
{
    public TextMesh textMesh;
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;
    public float floatDistance = 1f;
    public Color outlineColor = Color.black;
    public float outlineWidth = 0.1f;

    private bool isPlayerInside = false;
    private float currentAlpha = 0f;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private TextMesh[] outlineTextMeshes;

    private void Start()
    {
        if (textMesh == null)
        {
            Debug.LogError("TextMesh is not assigned!");
            return;
        }

        // 시작 위치 설정
        startPosition = textMesh.transform.position;
        endPosition = startPosition + Vector3.up * floatDistance;

        // 초기 텍스트 설정
        SetTextAlpha(0f);

        // 아웃라인 생성
        CreateOutline();

        Debug.Log("3DTextTrigger initialized. Start position: " + startPosition);
    }

    private void CreateOutline()
    {
        outlineTextMeshes = new TextMesh[8];
        for (int i = 0; i < 8; i++)
        {
            GameObject outlineObject = new GameObject("Outline " + i);
            outlineObject.transform.SetParent(textMesh.transform, false);
            outlineObject.transform.localPosition = GetOutlinePosition(i);

            TextMesh outlineTextMesh = outlineObject.AddComponent<TextMesh>();
            outlineTextMesh.text = textMesh.text;
            outlineTextMesh.font = textMesh.font;
            outlineTextMesh.fontSize = textMesh.fontSize;
            outlineTextMesh.fontStyle = textMesh.fontStyle;
            outlineTextMesh.anchor = textMesh.anchor;
            outlineTextMesh.alignment = textMesh.alignment;
            outlineTextMesh.color = outlineColor;

            outlineTextMeshes[i] = outlineTextMesh;
        }
    }

    private Vector3 GetOutlinePosition(int index)
    {
        switch (index)
        {
            case 0: return new Vector3(-outlineWidth, outlineWidth, 0);
            case 1: return new Vector3(outlineWidth, outlineWidth, 0);
            case 2: return new Vector3(outlineWidth, -outlineWidth, 0);
            case 3: return new Vector3(-outlineWidth, -outlineWidth, 0);
            case 4: return new Vector3(-outlineWidth, 0, 0);
            case 5: return new Vector3(outlineWidth, 0, 0);
            case 6: return new Vector3(0, outlineWidth, 0);
            case 7: return new Vector3(0, -outlineWidth, 0);
            default: return Vector3.zero;
        }
    }

    private void Update()
    {
        if (textMesh == null) return;

        if (isPlayerInside && currentAlpha < 1f)
        {
            currentAlpha += Time.deltaTime / fadeInDuration;
        }
        else if (!isPlayerInside && currentAlpha > 0f)
        {
            currentAlpha -= Time.deltaTime / fadeOutDuration;
        }

        currentAlpha = Mathf.Clamp01(currentAlpha);

        // 텍스트 알파값과 위치 업데이트
        SetTextAlpha(currentAlpha);
        textMesh.transform.position = Vector3.Lerp(startPosition, endPosition, currentAlpha);

        Debug.Log("Current text position: " + textMesh.transform.position);
    }

    private void SetTextAlpha(float alpha)
    {
        Color newColor = textMesh.color;
        newColor.a = alpha;
        textMesh.color = newColor;

        if (outlineTextMeshes != null)
        {
            foreach (TextMesh outlineTextMesh in outlineTextMeshes)
            {
                if (outlineTextMesh != null)
                {
                    Color outlineColor = outlineTextMesh.color;
                    outlineColor.a = alpha;
                    outlineTextMesh.color = outlineColor;
                }
            }
        }
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