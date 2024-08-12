using UnityEngine;

public class Window : MonoBehaviour
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
        audioSource.clip = soundEffect;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object we collided with has the tag "Heavy Tool"
        if (collision.collider.CompareTag("Heavy Tool"))
        {
            audioSource.PlayOneShot(soundEffect);  // 소리 재생
            // 소리 재생 후 오브젝트 파괴
            Invoke(nameof(DestroyWindow), soundEffect.length);
        }
    }

    private void DestroyWindow()
    {
        Destroy(gameObject);
    }
}
