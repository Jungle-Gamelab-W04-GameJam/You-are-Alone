using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject itemPrefab; // The prefab for the item to spawn
    public Transform spawnPoint; // The position where the item will spawn

    public void Unlock()
    {
        if (itemPrefab != null && spawnPoint != null)
        {
            // Spawn the item prefab at the specified location
            Instantiate(itemPrefab, spawnPoint.position, spawnPoint.rotation);
        }

        // Destroy the chest after spawning the item
        Destroy(gameObject);
    }
}
