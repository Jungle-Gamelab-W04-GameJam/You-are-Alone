using UnityEngine;

public class CamcorderInteraction : MonoBehaviour
{
    public Camera camcorderCamera; // The camera attached to the Camcorder
    public RenderTexture camcorderView; // The Render Texture for the Camcorder's view
    public GameObject monitorObject; // The Monitor object
    public GameObject camcorderDisplayObject; // The display object on the Camcorder
    private bool isActive = false; // Tracks if the Camcorder is active

    private Rigidbody camcorderRigidbody; // Rigidbody component of the Camcorder
    private bool gravityEnabled = false; // Tracks if gravity has been enabled once

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
        //camcorderCamera.enabled = false;

        // Set Use Gravity to false at the start (if needed)
        camcorderRigidbody.useGravity = false;
    }

    public void InteractWithCamcorder()
    {
        // Get the Renderer component of the Monitor
        Renderer monitorRenderer = monitorObject.GetComponent<Renderer>();
        Renderer camcorderRenderer = camcorderDisplayObject.GetComponent<Renderer>();

        if (monitorRenderer == null || camcorderRenderer == null)
        {
            Debug.LogError("One or more objects do not have a Renderer component.");
            return;
        }

        if (!isActive)
        {
            // Activate the Camcorder
            camcorderCamera.enabled = true;
            if (!monitorRenderer.enabled)
            {
                monitorRenderer.enabled = true;
            }
            monitorRenderer.material.mainTexture = camcorderView;
            camcorderRenderer.material.mainTexture = camcorderView; // Also update the camcorder's display
            isActive = true;
            Debug.Log("Camcorder is now active.");
        }
        else
        {
            // Deactivate the Camcorder
            camcorderCamera.enabled = false;
            monitorRenderer.material.mainTexture = null;
            camcorderRenderer.material.mainTexture = null; // Also clear the camcorder's display
            isActive = false;
            Debug.Log("Camcorder is now inactive.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // When a collision occurs, set Use Gravity to true only once
        if (camcorderRigidbody != null && !gravityEnabled)
        {
            camcorderRigidbody.useGravity = true;
            gravityEnabled = true; // Set flag to indicate gravity has been enabled
            Debug.Log("Gravity enabled for the first time on collision with " + collision.gameObject.name);
        }
    }
}
