using UnityEngine;
using Cinemachine;

public class Spyglass : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine virtual camera
    private float originalFOV; // Store the original field of view
    public float spyglassFOV = 1f; // Field of view when using the spyglass
    public GameObject childObject; // The child object to activate/deactivate

    void Start()
    {
        // Get and store the original field of view from the virtual camera
        if (virtualCamera != null)
        {
            originalFOV = virtualCamera.m_Lens.FieldOfView;
        }

        // Ensure childObject is assigned, or find the first child if it's null
        if (childObject == null && transform.childCount > 0)
        {
            childObject = transform.GetChild(0).gameObject;
        }
    }

    // Call this method to use the spyglass
    public void ZoomIn()
    {
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.FieldOfView = spyglassFOV;
        }

        if (childObject != null)
        {
            childObject.SetActive(false); // Deactivate the child object
        }
    }

    // Call this method to stop using the spyglass
    public void ZoomOut()
    {
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.FieldOfView = originalFOV;
        }

        if (childObject != null)
        {
            childObject.SetActive(true); // Activate the child object
        }
    }
}
