using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayInteract : MonoBehaviour
{
    private Camera playerCam;
    [SerializeField] float distance = 20f;
    public LayerMask whatIsTarget;
    private Transform moveTarget;
    private float targetDistance;
    private StarterAssetsInputs _input;
    public GameObject holdingProp;
    [SerializeField] float pickUpOffset = 2f;
    [SerializeField] float throwForce = 10f;

    private void Start()
    {
        playerCam = Camera.main;
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (_input.interact)
        {
            OnInteract();
            _input.interact = false;
        }

        if (holdingProp != null)
        {
            Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            Vector3 rayDir = playerCam.transform.forward;
            Ray ray = new Ray(rayOrigin, rayDir);
            Debug.DrawRay(ray.origin, ray.direction * 20f, Color.green);
            moveTarget.position = ray.origin + ray.direction * targetDistance;

            if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼을 누르면 던지기
            {
                ThrowObject();
            }
        }
    }

    private void OnInteract()
    {
        if (holdingProp == null)
        {
            Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
            Vector3 rayDir = playerCam.transform.forward;
            Ray ray = new Ray(rayOrigin, rayDir);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance, whatIsTarget))
            {
                GameObject hitTarget = hit.collider.gameObject;
                moveTarget = hitTarget.transform;
                targetDistance = pickUpOffset;
                holdingProp = hitTarget;

                Rigidbody holdingRb = holdingProp.GetComponent<Rigidbody>();
                if (holdingRb != null)
                {
                    holdingRb.isKinematic = true;
                }

                InteractionButton interactionButton = holdingProp.GetComponent<InteractionButton>();
                if (interactionButton != null)
                {
                    interactionButton.OnPickUp();
                }

                Debug.Log("Picked up: " + holdingProp.name);
            }
        }
        else
        {
            DropObject();
        }
    }

    private void ThrowObject()
    {
        if (holdingProp != null)
        {
            Rigidbody holdingRb = holdingProp.GetComponent<Rigidbody>();
            if (holdingRb != null)
            {
                holdingRb.isKinematic = false;
                holdingRb.velocity = playerCam.transform.forward * throwForce;
            }

            InteractionButton interactionButton = holdingProp.GetComponent<InteractionButton>();
            if (interactionButton != null)
            {
                interactionButton.OnThrow();
            }

            holdingProp = null;
            moveTarget = null;
            Debug.Log("Threw the object.");
        }
    }

    private void DropObject()
    {
        Rigidbody holdingRb = holdingProp.GetComponent<Rigidbody>();
        if (holdingRb != null)
        {
            holdingRb.isKinematic = false;
            holdingRb.velocity = Vector3.zero;
        }

        InteractionButton interactionButton = holdingProp.GetComponent<InteractionButton>();
        if (interactionButton != null)
        {
            interactionButton.OnDrop();
        }

        holdingProp = null;
        moveTarget = null;
        Debug.Log("Dropped the object.");
    }
}