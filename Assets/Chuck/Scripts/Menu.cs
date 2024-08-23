using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject languagePanel; // 언어 선택 패널
    public Button languageButton;    // 언어 선택 버튼
    public Button playButton;
    public Button quitButton;
    public Button koreanButton;      // Korean 버튼
    public Button englishButton;     // English 버튼
    public Button creditButton;
    public GameObject creditPanel;

    public Button closeCreditPanel;
    public Button closeLanguagePanel;
    private void Start()
    {
        // 초기 언어 설정
        string selectedLanguage = PlayerPrefs.GetString("SelectedLanguage", "English");
        ApplyLanguage(selectedLanguage);
        UpdateButtonColors(selectedLanguage);

        // 언어 패널을 비활성화합니다.
        languagePanel.SetActive(false);

        // 버튼 이벤트 추가
        languageButton.onClick.AddListener(OnLanguageButton);
        koreanButton.onClick.AddListener(() => OnLanguageSelect("Korean"));
        englishButton.onClick.AddListener(() => OnLanguageSelect("English"));
        playButton.onClick.AddListener(OnPlayButton);
        quitButton.onClick.AddListener(OnQuitButton);
        creditButton.onClick.AddListener(ToggleCreditPanel);
        closeCreditPanel.onClick.AddListener(CloseCreditPanel);
        closeLanguagePanel.onClick.AddListener(CloseLanguagePanel);
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
    public void ToggleCreditPanel()
    {
        creditPanel.SetActive(!creditPanel.activeSelf);
    }
    public void CloseCreditPanel()
    {
        creditPanel.SetActive(false);
    }
    public void CloseLanguagePanel()
    {
        languagePanel.SetActive(false);
    }
    public void OnLanguageButton()
    {
        // 언어 선택 패널 표시/숨기기
        languagePanel.SetActive(!languagePanel.activeSelf);
    }

    public void OnLanguageSelect(string language)
    {
        PlayerPrefs.SetString("SelectedLanguage", language);
        PlayerPrefs.Save();

        ApplyLanguage(language);
        UpdateButtonColors(language);

        // 언어 선택 후 패널을 숨깁니다.
        languagePanel.SetActive(false);
    }

    private void ApplyLanguage(string language)
    {
        // 선택된 언어를 적용하는 로직을 구현합니다.
        Debug.Log("Language applied: " + language);
    }

    private void UpdateButtonColors(string selectedLanguage)
    {
        Color normalColor = Color.white;
        Color selectedColor = new Color(0.7f, 0.7f, 0.7f); // 어둡게 할 색상

        if (selectedLanguage == "Korean")
        {
            koreanButton.image.color = selectedColor;
            englishButton.image.color = normalColor;
        }
        else if (selectedLanguage == "English")
        {
            koreanButton.image.color = normalColor;
            englishButton.image.color = selectedColor;
        }
    }
}
