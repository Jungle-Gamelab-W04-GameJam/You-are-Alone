using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject itemPrefab; // The prefab for the item to spawn
    public Transform spawnPoint; // The position where the item will spawn
    public AudioClip openSound; // Sound to play when the chest opens
    private AudioSource audioSource; // AudioSource component for playing sounds

    void Start()
    {
        // Get the AudioSource component attached to this object
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource component if it doesn't exist
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void Unlock()
    {
        if (itemPrefab != null && spawnPoint != null)
        {
            // Spawn the item prefab at the specified location
            Instantiate(itemPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        // Play the open sound
        if (openSound != null)
        {
            audioSource.PlayOneShot(openSound);
        }

        // Destroy the chest after spawning the item and playing the sound
        Destroy(gameObject, openSound.length); // Destroy after sound has finished
    }
}
