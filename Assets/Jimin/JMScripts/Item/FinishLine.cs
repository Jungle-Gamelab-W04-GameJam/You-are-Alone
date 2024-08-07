using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager를 사용하기 위해 필요

public class FinishLine : MonoBehaviour
{
    public string nextSceneName; // 전환할 씬의 이름

    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 "Player" 태그를 가지고 있는지 확인
        if (other.CompareTag("Player"))
        {
            // 다음 씬 로드
            SceneManager.LoadScene(nextSceneName);
            Debug.Log("씬 전환: " + nextSceneName);
        }
    }
}
