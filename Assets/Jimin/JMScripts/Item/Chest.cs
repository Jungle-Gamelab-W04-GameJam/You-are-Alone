using UnityEngine;

public class Chest : MonoBehaviour
{
    public AudioClip openSound; // Sound to play when the chest opens
    private AudioSource audioSource; // AudioSource component for playing sounds

    public GameObject hiddenObject; // Reference to the hidden object to activate

    void Start()
    {
        // Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource component if it doesn't exist
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Ensure the hidden object is initially inactive
        if (hiddenObject != null)
        {
            hiddenObject.SetActive(false);
        }
    }

    public void Unlock()
    {
        // Activate the hidden object
        if (hiddenObject != null)
        {
            hiddenObject.SetActive(true);
        }

        // Play the open sound
        if (openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }

        // Destroy the chest after playing the sound
        Destroy(gameObject, openSound.length); // Destroy after sound has finished
    }
}
