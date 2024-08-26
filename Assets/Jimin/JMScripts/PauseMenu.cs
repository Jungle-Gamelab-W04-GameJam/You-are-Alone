using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject languagePanel;

    public GameObject creditPanel;

    public Button resumeButton;
    public Button restartButton;
    public Button languageButton;
    public Button creditButton;

    public Button koreanButton;
    public Button englishButton;
    public Button quitButton;
    public Button closeCreditPanel;
    public Button closeLanguagePanel;

    private void Start()
    {
        // 초기화
        pauseMenuUI.SetActive(false);
        languagePanel.SetActive(false); // 언어 선택 패널 초기 비활성화

        // 버튼 클릭 이벤트 설정
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(RestartLevel);
        languageButton.onClick.AddListener(ToggleLanguagePanel);
        creditButton.onClick.AddListener(ToggleCreditPanel);
        closeCreditPanel.onClick.AddListener(CloseCreditPanel);
        closeLanguagePanel.onClick.AddListener(CloseLanguagePanel);
        koreanButton.onClick.AddListener(() => OnLanguageSelect("Korean"));
        englishButton.onClick.AddListener(() => OnLanguageSelect("English"));
        quitButton.onClick.AddListener(QuitGame);

        // 이전에 선택된 언어로 언어 버튼 초기화
        UpdateButtonColors(PlayerPrefs.GetString("SelectedLanguage", "English"));
    }

    private void Update()
    {
        // 퍼즈 메뉴를 토글하는 키 입력 처리 (예: Escape 키)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ResumeGame()
    {
        CloseCreditPanel();
        CloseLanguagePanel();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;  // 게임 시간 다시 흐름

        // 마우스 커서를 다시 숨기고 잠금 상태로 전환
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;  // 게임 시간 정지

        // 마우스 커서를 보이게 하고 잠금 해제
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;  // 게임 시간 다시 흐름
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // 현재 씬 다시 로드
    }

    public void ToggleLanguagePanel()
    {
        languagePanel.SetActive(!languagePanel.activeSelf);
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
    public void OnLanguageSelect(string language)
    {
        PlayerPrefs.SetString("SelectedLanguage", language);
        PlayerPrefs.Save();

        UpdateButtonColors(language);
        languagePanel.SetActive(false);
    }
    private void UpdateButtonColors(string selectedLanguage)
    {
        if (koreanButton == null || englishButton == null)
        {
            Debug.LogError("Korean or English button is not assigned. Please check the Inspector.");
            return;
        }

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

        // if (languageButton != null)
        // {
        //     languageButton.GetComponentInChildren<TextMeshProUGUI>().text = $"Language: {selectedLanguage}";
        // }
        // else
        // {
        //     Debug.LogError("Language button is not assigned. Please check the Inspector.");
        // }
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
