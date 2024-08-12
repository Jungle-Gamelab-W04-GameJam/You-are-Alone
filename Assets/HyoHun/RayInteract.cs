using StarterAssets;
using UnityEngine;

public class RayInteract : MonoBehaviour
{
    // Camera to cast the ray from
    private Camera playerCam;
    // Length of the ray
    [SerializeField] float distance = 20f;
    // Layer to check for the ray
    public LayerMask whatIsTarget;
    public LayerMask ignoreTrigger;

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
        if (_input.use)
        {
            UseItem();
            _input.use = false;
        }

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

                switch (rootObject.tag)
                {
                    case "Lever":
                        LeverController lever = rootObject.GetComponent<LeverController>();
                        if (lever != null)
                        {
                            lever.Use();
                        }
                        else
                        {
                            Debug.LogError("No Scripts in Lever!");
                        }
                        break;

                    case "JMLever":
                        JiminLeverController jiminLever = rootObject.GetComponent<JiminLeverController>();
                        if (jiminLever != null)
                        {
                            jiminLever.Use();
                        }
                        else
                        {
                            Debug.LogError("No Scripts in JMLever!");
                        }
                        break;

                    case "ShutLever":
                        ShutDownLever shutLever = rootObject.GetComponent<ShutDownLever>();
                        if (shutLever != null)
                        {
                            shutLever.Use();
                        }
                        else
                        {
                            Debug.LogError("No Scripts in Lever!");
                        }
                        break;

                    case "KeyPad":
                        KeypadManager keyPad = rootObject.GetComponent<KeypadManager>();
                        if (keyPad != null)
                        {
                            keyPad.ShowKeypad();
                        }
                        else
                        {
                            Debug.LogError("No Scripts in KeyPad!");
                        }
                        break;

                    case "Button":
                        ButtonController button = rootObject.GetComponent<ButtonController>();
                        if (button != null)
                        {
                            button.Use();
                        }
                        else
                        {
                            Stage7_ButtonController button7 = rootObject.GetComponent<Stage7_ButtonController>();
                            if (button7 != null)
                            {
                                button7.Use();
                            }
                        }
                        break;

                    case "LightButton":
                        LightButton lightButton = rootObject.GetComponent<LightButton>();
                        if (lightButton != null)
                        {
                            lightButton.Use();
                        }
                        break;


                    case "DestroyButton":
                        DestroyButton destroyButton = rootObject.GetComponent<DestroyButton>();
                        if (destroyButton != null)
                        {
                            destroyButton.Use();
                        }
                        break;


                    default:
                        Debug.Log("Unhandled item tag: " + holdingProp.tag + " / OnInteract() called");
                        break;
                }
            }
        }
        else if (!isZoomIn)
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

        // Ignore collision between the player and the held object
        Collider playerCollider = GetComponent<Collider>();
        if (playerCollider != null && holdingCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, holdingCollider, true);
        }

        // Ignore collision between the held object and other "Prop" objects
        /*
        int propLayer = LayerMask.NameToLayer("Prop");
        Physics.IgnoreLayerCollision(holdingProp.layer, propLayer, true);
        */

        Debug.Log("Picked up: " + holdingProp.name);
    }

    private void DropProp()
    {
        if (holdingProp != null)
        {
            // Re-enable collision between the held object and other "Prop" objects
            /*
            int propLayer = LayerMask.NameToLayer("Prop");
            Physics.IgnoreLayerCollision(holdingProp.layer, propLayer, false);
            */

            // Re-enable gravity
            if (holdingRb != null)
            {
                holdingRb.useGravity = true;
                holdingRb.velocity = Vector3.zero; // Reset velocity before dropping
            }

            // Re-enable collision between the player and the held object
            Collider playerCollider = GetComponent<Collider>();
            if (playerCollider != null && holdingCollider != null)
            {
                Physics.IgnoreCollision(playerCollider, holdingCollider, false);
            }
            
            holdingRb.constraints = RigidbodyConstraints.None;

            holdingRb = null;
            holdingCollider = null;
            holdingProp = null;
            moveTarget = null;

            Debug.Log("Dropped the object.");
        }
    }

    private void ThrowProp()
    {
        if (holdingProp == null) { return; }

        if (holdingRb != null)
        {
            // Re-enable collision between the held object and other "Prop" objects
            /*
            int propLayer = LayerMask.NameToLayer("Prop");
            Physics.IgnoreLayerCollision(holdingProp.layer, propLayer, false);
            */

            // Apply force in the direction the player is facing 
            Vector3 throwDirection = playerCam.transform.forward;
            holdingRb.velocity = Vector3.zero; // Reset velocity before throwing
            holdingRb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);

            // Re-enable gravity
            holdingRb.useGravity = true;

            Debug.Log("Threw the object.");
        }

        // Re-enable collision between the player and the held object
        Collider playerCollider = GetComponent<Collider>();
        if (playerCollider != null && holdingCollider != null)
        {
            Physics.IgnoreCollision(playerCollider, holdingCollider, false);
        }
        

        holdingRb.constraints = RigidbodyConstraints.None;
        holdingRb = null;
        holdingCollider = null;
        holdingProp = null;
        moveTarget = null;
    }

    private void UseItem()
    {

        if (holdingProp == null) { return; }

        switch (holdingProp.tag)
        {
            case "Spyglass":
                HandleSpyglass();
                break;
            case "Camcorder":
                HandleCamcorder();
                break;
            // �ٸ� �±׸� �߰��� �� �ֽ��ϴ�.
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
    /*
    private void MoveHoldingProp()
    {
    Vector3 desiredPosition = playerCam.transform.position + playerCam.transform.forward * targetDistance;
    Vector3 direction = desiredPosition - holdingRb.position;
    float distance = direction.magnitude;

    if (Physics.Raycast(holdingRb.position, direction, out RaycastHit hit, distance, ignoreTrigger))
    {
        // Adjust position to avoid collision
        desiredPosition = hit.point - direction.normalized * holdingCollider.bounds.extents.magnitude;
    }

    holdingRb.MovePosition(desiredPosition);
    }
    */
    private void MoveHoldingProp()
    {
        Vector3 desiredPosition = playerCam.transform.position + playerCam.transform.forward * targetDistance;
        Vector3 direction = desiredPosition - holdingRb.position;
        float distance = direction.magnitude;

        // 플레이어와 물체 사이의 거리를 계산
        float currentDistanceFromPlayer = Vector3.Distance(playerCam.transform.position, holdingRb.position);

        // 플레이어와 물체 사이의 거리가 지정된 거리보다 멀다면 DropProp() 호출
        if (currentDistanceFromPlayer > pickUpOffset + 1f)
        {
            DropProp();
            return; // 물체를 드랍했으므로 더 이상 위치를 업데이트할 필요가 없습니다.
        }

        if (Physics.Raycast(holdingRb.position, direction, out RaycastHit hit, distance, ignoreTrigger))
        {
            // 충돌을 피하기 위해 위치를 조정
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
            if (holdingProp != null)
                holdingProp.transform.rotation = Quaternion.LookRotation(playerForward);
        }
    }
}