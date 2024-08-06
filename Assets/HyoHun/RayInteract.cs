using StarterAssets;
using System.Collections;
using System.Collections.Generic;
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

        if (_input.throwInput)
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

                if (hitTarget.layer == LayerMask.NameToLayer("Prop")) // Check if it has the tag that allows picking up
                {
                    HoldProp(hitTarget.gameObject);
                }
                else if (hitTarget.tag == "Interactable")
                {
                    Debug.Log("Interacting");
                    // Call internal method of the hitTarget
                }
            }
        }
        else
        {
            // Drop the held object
            DropProp();
        }
    }

    private void HoldProp(GameObject hitTarget)
    {
        // Store the position of the collision object in moveTarget
        moveTarget = hitTarget.transform;
        // Store the distance to the object in targetDistance
        targetDistance = pickUpOffset;

        // Assign the object being held
        holdingProp = hitTarget;
        holdingRb = holdingProp.GetComponent<Rigidbody>();
        holdingCollider = holdingProp.GetComponent<Collider>();

        //add
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
        if (holdingProp == null) { return; }

        if(holdingRb != null)
        {

        }
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
}