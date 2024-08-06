using UnityEngine;

public class SphereGenerator : MonoBehaviour
{
    public GameObject spherePrefab;
    public Transform spawnPoint;

    private GameObject currentSphere;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && currentSphere == null)
        {
            GenerateSphere();
        }
    }

    void GenerateSphere()
    {
        currentSphere = Instantiate(spherePrefab, spawnPoint.position, Quaternion.identity);
    }

    public void DestroySphere()
    {
        if (currentSphere != null)
        {
            Destroy(currentSphere);
            currentSphere = null;
        }
    }
}