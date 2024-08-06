using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; // IEnumerator를 사용하기 위해 추가

public class KeypadManager : MonoBehaviour
{
    public GameObject keypadUI; // Keypad UI Canvas
    public TextMeshProUGUI inputText; // 입력된 숫자를 표시할 텍스트
    public TextMeshProUGUI successText; // 성공 메시지를 표시할 텍스트
    public TextMeshProUGUI failText; // 실패 메시지를 표시할 텍스트
    public string correctCode = "123456"; // 정답 코드
    private string enteredCode = ""; // 플레이어가 입력한 코드
    public bool KeypadUnlocked { get; private set; } = false; // 키패드가 열렸는지 여부
    private float failDisplayTime = 2f; // 실패 메시지를 표시할 시간
    private float successDisplayTime = 1f; // 성공 메시지를 표시할 시간

    void Start()
    {
        // keyPadUI.SetActive(false); // UI를 처음에는 비활성화 상태로 유지
        successText.gameObject.SetActive(false);
        failText.gameObject.SetActive(false);
    }

    public void OnNumberButtonClick(string number)
    {
        if (enteredCode.Length < 6)
        {
            enteredCode += number;
            inputText.text = enteredCode;
        }
    }

    public void OnClearButtonClick()
    {
        enteredCode = "";
        inputText.text = enteredCode;
    }

    public void OnConfirmButtonClick()
    {
        if (enteredCode.Length == 6)
        {
            CheckCode();
        }
    }

    private void CheckCode()
    {
        if (enteredCode == correctCode)
        {
            StartCoroutine(ShowSuccessMessage());
        }
        else
        {
            StartCoroutine(ShowFailMessage());
        }
    }

    private IEnumerator ShowSuccessMessage()
    {
        inputText.text = "";
        successText.gameObject.SetActive(true);
        yield return new WaitForSeconds(successDisplayTime);
        successText.gameObject.SetActive(false);
        KeypadUnlocked = true;
        HideKeypad();
    }

    private IEnumerator ShowFailMessage()
    {
        inputText.text = "";
        failText.gameObject.SetActive(true);
        yield return new WaitForSeconds(failDisplayTime);
        failText.gameObject.SetActive(false);
        OnClearButtonClick();
    }

    public void ShowKeypad()
    {
        keypadUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideKeypad()
    {
        keypadUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        OnClearButtonClick();
        successText.gameObject.SetActive(false);
    }
}
