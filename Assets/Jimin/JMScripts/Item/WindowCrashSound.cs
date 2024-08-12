using UnityEngine;

public class WindowCrashSound : MonoBehaviour
{
    public AudioClip breakingSound; // 깨지는 소리 파일
    private AudioSource audioSource; // 오디오 소스 컴포넌트

    private void Start()
    {
        // 오디오 소스 컴포넌트를 가져오거나 추가
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = breakingSound;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // "Window" 태그가 있는 오브젝트와 충돌했는지 확인
        if (collision.collider.CompareTag("Window"))
        {
            // 깨지는 소리 재생
            audioSource.PlayOneShot(breakingSound);

        }
    }
}
