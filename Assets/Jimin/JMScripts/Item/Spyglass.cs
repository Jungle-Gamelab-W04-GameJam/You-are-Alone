using UnityEngine;
using Cinemachine;

public class Spyglass : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine virtual camera
    private float originalFOV; // Store the original field of view
    public float spyglassFOV = 1f; // Field of view when using the spyglass

    void Start()
    {
        // Get and store the original field of view from the virtual camera
        if (virtualCamera != null)
        {
            originalFOV = virtualCamera.m_Lens.FieldOfView;
        }
    }

    // Call this method to use the spyglass
    public void ZoomIn()
    {
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.FieldOfView = spyglassFOV;
        }
    }

    // Call this method to stop using the spyglass
    public void ZoomOut()
    {
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.FieldOfView = originalFOV;
        }
    }
}
