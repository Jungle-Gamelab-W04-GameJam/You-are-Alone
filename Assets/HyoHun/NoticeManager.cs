using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager를 사용하기 위해 추가
using UnityEngine.UI;
using System.Collections;

public class NoticeManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] private GameObject noticeCanvas;
    [SerializeField] private Image noticePanel;
    [SerializeField] private TextMeshProUGUI noticeText;
    [SerializeField] private string csvFileName = "Localization.csv"; // CSV 파일 이름
    [SerializeField] private string language = "Korean"; // 기본 언어 설정

    private List<string> noticeMessages = new List<string>();

    [Header("공지 시간 설정")]
    [SerializeField] private float startDelay = 1.5f;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float showingDuration = 5f;

    public bool isShowingNotice;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (noticeCanvas != null)
        {
            noticeCanvas.SetActive(false);
        }
        isShowingNotice = false;
    }

    private void Start()
    {
        language = PlayerPrefs.GetString("SelectedLanguage", "English");

        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex > 0)
        {
            int csvId = sceneIndex - 1;
            LoadNoticeMessages(csvId);
        }

        if (noticeMessages.Count == 0)
        {
            return;
        }

        StartCoroutine(DisplayNotices());
    }

    private void LoadNoticeMessages(int csvId)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, csvFileName);
        Debug.Log("Attempting to load file from: " + filePath); // 파일 경로 출력

        if (File.Exists(filePath))
        {
            string[] data = File.ReadAllLines(filePath);
            noticeMessages.Clear();

            // 첫 번째 줄 (헤더) 처리
            string[] headers = data[0].Split(',');
            int languageIndex = -1;

            for (int i = 0; i < headers.Length; i++)
            {
                if (headers[i].ToLower() == language.ToLower())
                {
                    languageIndex = i;
                    break;
                }
            }

            if (languageIndex != -1)
            {
                for (int i = 1; i < data.Length; i++)
                {
                    string[] lineData = data[i].Split(',');

                    if (lineData.Length > languageIndex)
                    {
                        if (int.TryParse(lineData[0], out int id) && id == csvId)
                        {
                            string noticeMessage = lineData[languageIndex];

                            if (!string.IsNullOrWhiteSpace(noticeMessage))
                            {
                                noticeMessages.Add(noticeMessage);
                            }
                            break; // ID가 일치하는 첫 메시지만 로드
                        }
                    }
                }
            }
            else
            {
                Debug.LogError("Language not found in CSV file.");
            }
        }
        else
        {
            Debug.LogError("Cannot find CSV file at path: " + filePath); // 경로가 잘못된 경우
        }
    }

    private IEnumerator DisplayNotices()
    {
        yield return new WaitForSeconds(startDelay);
        isShowingNotice = true;

        //audioSource.Play();

        foreach (var message in noticeMessages)
        {
            yield return StartCoroutine(DisplayNotice(message));
        }

        isShowingNotice = false;
    }

    private IEnumerator DisplayNotice(string message)
    {
        noticeText.text = message;
        if (noticeCanvas != null)
        {
            noticeCanvas.SetActive(true);
        }

        // Fade in
        yield return StartCoroutine(FadeNotice(0f, 0.7f, fadeDuration));

        // Stay visible
        yield return new WaitForSeconds(showingDuration);

        // Fade out
        yield return StartCoroutine(FadeNotice(0.7f, 0f, fadeDuration));

        noticeCanvas.SetActive(false);
    }

    private IEnumerator FadeNotice(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);

            SetCanvasGroupAlpha(alpha);

            yield return null;
        }

        SetCanvasGroupAlpha(endAlpha);
    }

    private void SetCanvasGroupAlpha(float alpha)
    {
        Color panelColor = noticePanel.color;
        panelColor.a = alpha;
        noticePanel.color = panelColor;

        Color textColor = noticeText.color;
        textColor.a = alpha;
        noticeText.color = textColor;
    }
}
