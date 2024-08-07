using UnityEngine;

public class Platform : MonoBehaviour
{
    public LightColor platformColor; // This platform's color
    public LightManager lightManager; // Reference to the LightManager script

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            lightManager.OnPlatformStepped(platformColor);
            Debug.Log($"Player stepped on {platformColor} platform.");
        }
    }
}
