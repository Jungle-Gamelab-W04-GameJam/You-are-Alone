using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class RayInteract : MonoBehaviour
{
    // Camera to cast the ray from
    private Camera playerCam;
    // Length of the ray
    [SerializeField] float distance = 20f;
    // Layer to check for the ray
    public LayerMask whatIsTarget;

    // Position information to update when interacting with an object
    private Transform moveTarget;
    // Distance value to maintain when interacting with an object
    private float targetDistance;

    // Input handler
    private StarterAssetsInputs _input;

    // Object being held
    public GameObject holdingProp;

    [SerializeField] float pickUpOffset = 2f;
    [SerializeField] float throwForce = 10f;

    private Rigidbody holdingRb;
    private Collider holdingCollider;

    public bool isZoomIn = false;

    private void Start()
    {
        // Assign the main camera for raycasting
        playerCam = Camera.main;
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (_input.interact)
        {
            OnInteract();
            _input.interact = false; // Reset interact state after input is processed
        }

        if (_input.throwInput && !isZoomIn)
        {
            ThrowProp();
            _input.throwInput = false; // Reset throw input state after input is processed
        }

        if(_input.use)
        {
            UseItem();
            _input.use = false;
        }

    }

    private void FixedUpdate()
    {   //Calling from FixedUpdate for Physical Conflict Detection
        if (holdingProp != null)
        {
            MoveHoldingProp();
            UpdateHoldingPropRotation();
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
                // Store the hit game object in hitTarget
                GameObject hitTarget = hit.collider.gameObject;
                GameObject rootObject = hitTarget.transform.root.gameObject;

                if (hitTarget.layer == LayerMask.NameToLayer("Prop")) // Check if it has the tag that allows picking up
                {
                    HoldProp(rootObject);
                }
                else if (hitTarget.tag == "Interactable")
                {
                    Debug.Log("Interacting");

                    // Call internal method of the hitTarget
                }
            }
        }
        else if(!isZoomIn)
        {
            // Drop the held object
            DropProp();
        }
    }

    private void HoldProp(GameObject rootObject)
    {
        // Store the position of the collision object in moveTarget
        moveTarget = rootObject.transform;
        // Store the distance to the object in targetDistance
        targetDistance = pickUpOffset;

        // Assign the object being held
        holdingProp = rootObject;
        holdingRb = holdingProp.GetComponent<Rigidbody>();
        holdingCollider = holdingProp.GetComponent<Collider>();

        //holdingProp.transform.rotation = Quaternion.identity;
        holdingRb.constraints = RigidbodyConstraints.FreezeRotation;

        // Disable gravity while holding
        if (holdingRb != null)
        {
            holdingRb.useGravity = false;
        }

        Debug.Log("Picked up: " + holdingProp.name);
    }

    private void DropProp()
    {
        if (holdingProp != null)
        {
            // Re-enable gravity
            if (holdingRb != null)
            {
                holdingRb.useGravity = true;
                holdingRb.velocity = Vector3.zero; // Reset velocity before dropping
            }

            holdingRb.constraints = RigidbodyConstraints.None;
            holdingProp = null;
            moveTarget = null;

            Debug.Log("Dropped the object.");
        }
    }

    private void ThrowProp()
    {
        if (holdingProp == null){return;}

        if (holdingRb != null)
        {
            // Apply force in the direction the player is facing
            Vector3 throwDirection = playerCam.transform.forward;
            holdingRb.velocity = Vector3.zero; // Reset velocity before throwing
            holdingRb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);

            // Re-enable gravity
            holdingRb.useGravity = true;

            Debug.Log("Threw the object.");
        }

        holdingRb.constraints = RigidbodyConstraints.None;
        holdingProp = null;
        moveTarget = null;
    }

    private void UseItem()
    {
        Debug.Log("Use Item 호출");
        if (holdingProp == null) { return; }

        // 아이템을 들고 있을 때
        switch (holdingProp.tag)
        {
            case "Spyglass":
                HandleSpyglass();
                break;
            case "Camcorder":
                HandleCamcorder();
                break;
            // 다른 태그를 추가할 수 있습니다.
            default:
                Debug.Log("Unhandled item tag: " + holdingProp.tag);
                break;
        }
    }

    private void HandleSpyglass()
    {
        Spyglass spyGlass = holdingProp.GetComponent<Spyglass>();

        if (isZoomIn == false)
        {
            spyGlass.ZoomIn();
            isZoomIn = !isZoomIn;
        }
        else
        {
            spyGlass.ZoomOut();
            isZoomIn = !isZoomIn;
        }
    }

    private void HandleCamcorder()
    {
        CamcorderInteraction camcorder = holdingProp.GetComponent<CamcorderInteraction>();
        camcorder.InteractWithCamcorder();
    }


    private void MoveHoldingProp()
    {
        Vector3 desiredPosition = playerCam.transform.position + playerCam.transform.forward * targetDistance;
        Vector3 direction = desiredPosition - holdingRb.position;
        float distance = direction.magnitude;

        if (Physics.Raycast(holdingRb.position, direction, out RaycastHit hit, distance))
        {
            // Adjust position to avoid collision
            desiredPosition = hit.point - direction.normalized * holdingCollider.bounds.extents.magnitude;
        }

        holdingRb.MovePosition(desiredPosition);
    }
    private void UpdateHoldingPropRotation()
    {
        // Get the player's forward direction
        Vector3 playerForward = playerCam.transform.forward;
        // Ignore the y component to keep the object level with the ground
        playerForward.y = 0;
        if (playerForward != Vector3.zero)
        {
            // Set the object's rotation to face the same direction as the player
            holdingProp.transform.rotation = Quaternion.LookRotation(playerForward);
        }
    }
}