using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Renderer bulbRenderer; // 전구의 렌더러 컴포넌트
    public List<Color[]> colorPatterns; // 색상 패턴 목록
    private Color[] currentPattern; // 현재 패턴
    private int patternLength = 6; // 패턴의 길이
    public Material bulbMaterial; // 전구의 머티리얼

    void Start()
    {
        GenerateColorPatterns(); // 색상 패턴 생성
        SetRandomPattern(); // 초기 패턴 설정
    }

    // 버튼을 눌러 패턴을 새로 설정하고 전구를 실행
    public void OnButtonPress()
    {
        SetRandomPattern();
        StartCoroutine(PlayPattern());
    }

    // 랜덤 패턴 설정
    private void SetRandomPattern()
    {
        currentPattern = colorPatterns[Random.Range(0, colorPatterns.Count)];
    }

    // 색 패턴을 따라 전구를 켜고 끄는 코루틴
    private IEnumerator PlayPattern()
    {
        foreach (Color color in currentPattern)
        {
            TurnOnLight(color);
            yield return new WaitForSeconds(1f); // 전구가 켜진 상태 유지 시간
            TurnOffLight();
            yield return new WaitForSeconds(0.5f); // 전구가 꺼진 상태 유지 시간
        }
    }

    // 전구 켜기
    private void TurnOnLight(Color color)
    {
        bulbMaterial.color = color;
        bulbRenderer.enabled = true;
    }

    // 전구 끄기
    private void TurnOffLight()
    {
        bulbRenderer.enabled = false;
    }

    // 색상 패턴 생성
    private void GenerateColorPatterns()
    {
        colorPatterns = new List<Color[]>();

        // 예제 패턴들 (여기서는 6가지 색을 임의로 정의)
        colorPatterns.Add(new Color[] { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.cyan });
        colorPatterns.Add(new Color[] { Color.cyan, Color.magenta, Color.yellow, Color.blue, Color.green, Color.red });
        // 필요에 따라 더 많은 패턴 추가 가능
    }
}
