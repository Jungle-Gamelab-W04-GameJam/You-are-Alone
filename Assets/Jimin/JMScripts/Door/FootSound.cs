using UnityEngine;

public class FootSound : MonoBehaviour
{
    public AudioClip soundEffect;  // 재생할 소리 파일
    private AudioSource audioSource;  // 오디오 소스 컴포넌트

    private void Start()
    {
        // 오디오 소스 컴포넌트를 가져오거나 추가
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // 플레이어가 트리거 콜라이더에 들어올 때 호출
    private void OnTriggerEnter(Collider other)
    {
        // 충돌한 오브젝트가 플레이어 태그를 가지고 있으면
        if (other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(soundEffect);  // 소리 재생
        }
    }
}
