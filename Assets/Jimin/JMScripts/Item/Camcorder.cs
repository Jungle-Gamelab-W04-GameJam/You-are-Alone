using UnityEngine;

public class CamcorderInteraction : MonoBehaviour
{
    public Camera camcorderCamera; // The camera attached to the Camcorder
    public RenderTexture camcorderView; // The Render Texture for the Camcorder's view
    public GameObject monitorObject; // The Monitor object
    private bool isActive = false; // Tracks if the Camcorder is active

    private Rigidbody camcorderRigidbody; // Rigidbody component of the Camcorder

    void Start()
    {
        // Get the Rigidbody component of the Camcorder
        camcorderRigidbody = GetComponent<Rigidbody>();

        if (camcorderRigidbody == null)
        {
            Debug.LogError("Camcorder object does not have a Rigidbody component.");
            return;
        }

        // Initially set the camera to not render
        camcorderCamera.enabled = false;

        // Set Use Gravity to false at the start (if needed)
        camcorderRigidbody.useGravity = false;
    }

    public void InteractWithCamcorder()
    {
        // Get the Renderer component of the Monitor
        Renderer monitorRenderer = monitorObject.GetComponent<Renderer>();

        if (monitorRenderer == null)
        {
            Debug.LogError("Monitor object does not have a Renderer component.");
            return;
        }

        if (!isActive)
        {
            // Activate the Camcorder
            camcorderCamera.enabled = true;
            monitorRenderer.material.mainTexture = camcorderView;
            isActive = true;
            Debug.Log("Camcorder is now active.");
        }
        else
        {
            // Deactivate the Camcorder
            camcorderCamera.enabled = false;
            monitorRenderer.material.mainTexture = null;
            isActive = false;
            Debug.Log("Camcorder is now inactive.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // When a collision occurs, set Use Gravity to true
        if (camcorderRigidbody != null)
        {
            camcorderRigidbody.useGravity = true;
        }
    }
}
