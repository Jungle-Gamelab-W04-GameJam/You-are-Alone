using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private StarterAssetsInputs _input;
    public string currentSceneName;

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Start()
    {
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        RestartScene();
    }


    private void RestartScene()
    {
        if (currentSceneName == "End" && _input.restart)
        {
            QuitGame();
        }

        if (_input.restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // ���� ���� �޼���
    void QuitGame()
    {
        // ����Ƽ �����Ϳ��� ���� ���� ���� �����͸� �����մϴ�.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ����� ���ø����̼ǿ��� ���� ���� ���� ���ø����̼��� �����մϴ�.
        Application.Quit();
#endif
    }
}
