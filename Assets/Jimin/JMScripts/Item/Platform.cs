using UnityEngine;

public class Platform : MonoBehaviour
{
    public LightColor platformColor; // This platform's color
    public LightManager lightManager; // Reference to the LightManager script
    public AudioClip stepSound; // Sound to play when the player steps on the platform
    private AudioSource audioSource; // Audio source component
    private void Start()
    {
        // Get or add the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            // Play the step sound
            audioSource.PlayOneShot(stepSound);

            lightManager.OnPlatformStepped(platformColor);
            Debug.Log($"Player stepped on {platformColor} platform.");
        }
    }
}
