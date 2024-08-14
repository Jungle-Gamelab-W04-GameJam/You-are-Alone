using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoticeManager : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] private GameObject noticeCanvas;
    [SerializeField] private Image noticePanel;
    [SerializeField] private TextMeshProUGUI noticeText;
    [SerializeField] private List<string> noticeMessages;

    [Header("���� �ð� ����")]
    [SerializeField] private float startDelay = 1.5f;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float showingDuration = 3f;

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
        if (noticeMessages == null || noticeMessages.Count == 0)
        {
            return;
        }

        StartCoroutine(DisplayNotices());
    }

    private IEnumerator DisplayNotices()
    {
        yield return new WaitForSeconds(startDelay);
        isShowingNotice = true;

        audioSource.Play();

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

