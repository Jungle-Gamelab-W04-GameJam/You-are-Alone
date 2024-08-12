using UnityEngine;
using System.Collections;

public class OutlineEffect : MonoBehaviour
{
    public Material outlineMaterial; // 윤곽선 머티리얼
    private Material[] defaultMaterials; // 원래의 머티리얼들
    private Renderer objectRenderer;

    private void Start()
    {
        // 오브젝트의 Renderer 컴포넌트 참조
        objectRenderer = GetComponent<Renderer>();

        // 원래 머티리얼을 저장
        if (objectRenderer != null)
        {
            defaultMaterials = objectRenderer.materials;
        }
    }

    public void ShowOutline(float duration)
    {
        if (outlineMaterial != null && objectRenderer != null)
        {
            StartCoroutine(OutlineCoroutine(duration));
        }
    }

    private IEnumerator OutlineCoroutine(float duration)
    {
        // 현재 머티리얼 배열 복사
        Material[] currentMaterials = objectRenderer.materials;

        // 윤곽선 머티리얼을 추가
        Material[] outlineMaterials = new Material[currentMaterials.Length + 1];
        currentMaterials.CopyTo(outlineMaterials, 0);
        outlineMaterials[outlineMaterials.Length - 1] = outlineMaterial;

        objectRenderer.materials = outlineMaterials;

        yield return new WaitForSeconds(duration);

        // 원래 머티리얼로 복구
        objectRenderer.materials = defaultMaterials;
    }
}
