using UnityEngine;

public class PanelController : MonoBehaviour
{
    public GameObject panel2; // Reference to the Panel2 object
    public AudioClip removalSound; // Sound to play when a child is removed
    private AudioSource audioSource; // AudioSource for playing sounds
    private int screwHitCount = 0; // Counter for how many times the panel is hit by a screw

    private void Start()
    {
        // Ensure Panel2 is initially deactivated
        if (panel2 != null)
        {
            panel2.SetActive(false);
        }

        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has the tag "Screw"
        if (other.CompareTag("Screw"))
        {
            screwHitCount++; // Increment the hit count
            Debug.Log("Screw hit count: " + screwHitCount);

            // If there are child objects to remove
            if (transform.childCount > 0)
            {
                // Remove the first child object (or any child)
                Destroy(transform.GetChild(0).gameObject);

                // Play the removal sound
                PlayRemovalSound();
            }

            // If this is the fourth hit
            if (screwHitCount == 4)
            {
                ActivatePanel2AndDeactivateSelf();
            }
        }
    }

    private void ActivatePanel2AndDeactivateSelf()
    {
        // Activate Panel2 if it's assigned
        if (panel2 != null)
        {
            panel2.SetActive(true);
            Debug.Log("Panel2 is now active.");
        }

        // Deactivate this panel
        gameObject.SetActive(false);
        Debug.Log("Panel is now deactivated.");
    }

    private void PlayRemovalSound()
    {
        if (audioSource != null && removalSound != null)
        {
            audioSource.PlayOneShot(removalSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or RemovalSound not set.");
        }
    }
}
