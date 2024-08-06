using UnityEngine;

public class WoodenBoard : MonoBehaviour
{
    public int hitsToBreak = 3; // Number of hits needed to break the board
    private int currentHits = 0; // Current number of hits
    public AudioClip hitSound; // Sound to play when hit
    private AudioSource audioSource; // AudioSource component for playing sounds
    private bool isAxeInContact = false; // Whether the Axe is currently touching the board

    void Start()
    {
        // Get the AudioSource component attached to the object
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Add an AudioSource component if it doesn't exist
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object's tag is "Axe"
        if (collision.collider.CompareTag("Axe"))
        {
            // Only increase hit count if the Axe is not already in contact
            if (!isAxeInContact)
            {
                isAxeInContact = true; // Set the contact flag to true
                currentHits++; // Increase the hit count

                // Play the hit sound
                if (hitSound != null)
                {
                    audioSource.PlayOneShot(hitSound);
                }

                // Check if the board should be destroyed
                if (currentHits >= hitsToBreak)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Reset the contact flag when the Axe leaves the board
        if (collision.collider.CompareTag("Axe"))
        {
            isAxeInContact = false;
        }
    }
}
