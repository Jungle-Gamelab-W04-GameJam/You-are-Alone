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

    // 게임 종료 메서드
    void QuitGame()
    {
        // 유니티 에디터에서 실행 중일 때는 에디터를 종료합니다.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 애플리케이션에서 실행 중일 때는 애플리케이션을 종료합니다.
        Application.Quit();
#endif
    }
}
