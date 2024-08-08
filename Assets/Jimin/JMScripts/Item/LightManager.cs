using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Renderer bulbRenderer; // 전구의 렌더러
    public Material bulbMaterial; // 전구의 머티리얼
    private bool isPlayingPattern = false; // 패턴이 재생 중인지 확인하는 플래그
    private int patternLength = 6; // 패턴의 길이
    private LightColor[] colors; // 전구 색상 목록
    private List<LightColor> currentPattern; // 현재 재생 중인 패턴
    private List<LightColor> playerSequence; // 플레이어의 입력 순서
    public GameObject door; // 사라질 문 오브젝트

    // 오디오 설정
    public AudioClip successSound; // 성공 시 재생할 소리
    public AudioClip failureSound; // 실패 시 재생할 소리
    private AudioSource audioSource; // AudioSource 컴포넌트
    void Start()
    {
        // 전구 색상 목록 정의
        colors = new LightColor[] { LightColor.Red, LightColor.Green, LightColor.Blue, LightColor.Yellow, LightColor.Orange, LightColor.Purple };
        playerSequence = new List<LightColor>(); // 플레이어 입력 초기화
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource 컴포넌트가 없으면 추가
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 버튼을 누르면 호출되는 메소드
    public void OnButtonPress()
    {
        if (!isPlayingPattern)
        {
            StartCoroutine(PlayPattern());
        }
    }

    // 랜덤 패턴 생성 및 재생
    private IEnumerator PlayPattern()
    {
        isPlayingPattern = true;
        playerSequence.Clear(); // 플레이어 입력 초기화
        currentPattern = GenerateRandomPattern();

        foreach (LightColor color in currentPattern)
        {
            TurnOnLight(ConvertToUnityColor(color));
            yield return new WaitForSeconds(1f); // 전구가 켜진 상태 유지 시간
            TurnOffLight();
            yield return new WaitForSeconds(0.5f); // 전구가 꺼진 상태 유지 시간
        }

        isPlayingPattern = false;
    }

    // 랜덤 색상 패턴 생성 (중복되지 않도록)
    private List<LightColor> GenerateRandomPattern()
    {
        List<LightColor> pattern = new List<LightColor>();
        List<LightColor> availableColors = new List<LightColor>(colors); // 사용할 수 있는 색상 목록

        for (int i = 0; i < patternLength; i++)
        {
            int randomIndex = Random.Range(0, availableColors.Count);
            LightColor newColor = availableColors[randomIndex];
            pattern.Add(newColor);
            availableColors.RemoveAt(randomIndex); // 선택한 색상 제거
        }

        return pattern;
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

    // 플레이어가 플랫폼을 밟았을 때 호출
    public void OnPlatformStepped(LightColor platformColor)
    {
        if (isPlayingPattern) return; // 패턴 재생 중에는 입력을 받지 않음

        playerSequence.Add(platformColor);
        CheckSequence();
    }

    // 플레이어 입력이 올바른지 확인
    private void CheckSequence()
    {
        if (playerSequence.Count == currentPattern.Count)
        {
            if (IsCorrectSequence())
            {
                Debug.Log("정답입니다!");
                audioSource.PlayOneShot(successSound);
                if (door != null)
                {
                    Destroy(door);
                    Debug.Log("문이 열렸습니다!");
                }
            }
            else
            {
                Debug.Log("틀렸습니다, 다시 시도하세요!");
                audioSource.PlayOneShot(failureSound);

            }
            ResetSequence();
        }
    }

    // 입력된 순서가 올바른지 확인
    private bool IsCorrectSequence()
    {
        for (int i = 0; i < currentPattern.Count; i++)
        {
            if (playerSequence[i] != currentPattern[i])
            {
                return false;
            }
        }
        return true;
    }

    // 입력 순서 초기화
    private void ResetSequence()
    {
        playerSequence.Clear();
    }

    // LightColor 열거형을 UnityEngine.Color로 변환
    private Color ConvertToUnityColor(LightColor lightColor)
    {
        switch (lightColor)
        {
            case LightColor.Red:
                return new Color(1.0f, 0.0f, 0.0f); // 빨간색
            case LightColor.Green:
                return new Color(0.0f, 1.0f, 0.0f); // 초록색
            case LightColor.Blue:
                return new Color(0.0f, 0.0f, 1.0f); // 파란색
            case LightColor.Yellow:
                return new Color(1.0f, 1.0f, 0.0f); // 노란색
            case LightColor.Orange:
                return new Color(1.0f, 0.5f, 0.0f); // 주황색
            case LightColor.Purple:
                return new Color(0.5f, 0.0f, 0.5f); // 보라색
            default:
                return Color.white;
        }
    }
}

// 전구 색상 열거형
public enum LightColor
{
    Red,
    Green,
    Blue,
    Yellow,
    Orange,
    Purple
}
