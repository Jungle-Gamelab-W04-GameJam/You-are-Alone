using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPickUpScript : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;

    [SerializeField] float distance = 10f;
    [SerializeField] float throwForce = 10f;

    public GameObject holdingProp;
    private Rigidbody holdingRb;
    private Collider holdingCollider;

    private bool canDrop = true;

    private float rotationSensitivity = 1f;

    private int LayerNumber;

    private StarterAssetsInputs _input;
    private FirstPersonController fpController;

    public bool isZoomIn = false;

    public GameObject useNoticeText;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");
        _input = player.GetComponent<StarterAssetsInputs>();
        fpController = player.GetComponent<FirstPersonController>();

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
            if (holdingProp == null) //if currently not holding anything
            {
                //perform raycast to check if player is looking at object within pickuprange
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance))
                {
                    GameObject hitTarget = hit.collider.gameObject;
                    GameObject rootObject = hitTarget.transform.root.gameObject;

                    //make sure pickup tag is attached
                    if (hitTarget.layer == LayerMask.NameToLayer("Prop"))
                    {
                        //pass in object hit into the PickUpObject function
                        PickUpObject(rootObject);
                        _input.interact = false;
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

                            break;
                    }

                }
            }
            else
            {
                if (canDrop == true && !isZoomIn)
                {
                    StopClipping(); //prevents object from clipping through walls
                    DropObject();
                    _input.interact = false;
                }
            }
        }
        if (holdingProp != null) //if player is holding object
        {
            MoveObject(); //keep object position at holdPos

            RotateObject();

            if (_input.throwInput && canDrop == true && !isZoomIn) //Mous0 (leftclick) is used to throw, change this if you want another button to be used)
            {
                StopClipping();
                ThrowObject();
                _input.throwInput = false;
            }
        }

        if (holdingProp != null && (holdingProp.tag == "Spyglass"))
        {
            useNoticeText.SetActive(true);
        }
        else
        {
            useNoticeText.SetActive(false);
        }


    }

    void PickUpObject(GameObject pickUpObj)
    {
        if (pickUpObj.GetComponent<Rigidbody>()) //make sure the object has a RigidBody
        {
            holdingProp = pickUpObj; //assign heldObj to the object that was hit by the raycast (no longer == null)
            holdingRb = pickUpObj.GetComponent<Rigidbody>(); //assign Rigidbody
            holdingRb.isKinematic = true;
            holdingRb.transform.parent = holdPos.transform; //parent object to holdposition
            SetLayer(pickUpObj, LayerNumber); //change the object layer to the holdLayer
            //make sure object doesnt collide with player, it can cause weird bugs
            Physics.IgnoreCollision(holdingProp.GetComponent<Collider>(), player.GetComponent<Collider>(), true);

            // ¿ÀºêÁ§Æ®¸¦ µé ¶§ À±°û¼± Ç¥½Ã
            OutlineEffect outlineEffect = pickUpObj.GetComponent<OutlineEffect>();
            if (outlineEffect != null)
            {
                outlineEffect.ShowOutline(10f); // 10ÃÊ µ¿¾È À±°û¼± Ç¥½Ã
            }


        }
    }
    void DropObject()
    {
        //re-enable collision with player
        Physics.IgnoreCollision(holdingProp.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        SetLayer(holdingProp, 6); //object assigned back to prop layer
        holdingRb.isKinematic = false;
        holdingProp.transform.parent = null; //unparent object
        holdingProp = null; //undefine game object
    }
    void MoveObject()
    {
        //keep object position the same as the holdPosition position
        holdingProp.transform.position = holdPos.transform.position;
    }
    

    void RotateObject()
    {
        if (_input.use)//hold R key to rotate, change this to whatever key you want
        {
            canDrop = false; //make sure throwing can't occur during rotating

            //disable player being able to look around
            fpController.RotationSpeed = 0f;
            //mouseLookScript.lateralSensitivity = 0f;

            float XaxisRotation = Input.GetAxis("Mouse X") * rotationSensitivity;
            float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSensitivity;
            //rotate the object depending on mouse X-Y Axis
            holdingProp.transform.Rotate(Vector3.down, XaxisRotation);
            holdingProp.transform.Rotate(Vector3.right, YaxisRotation);
        }
        else
        {
            //re-enable player being able to look around
            fpController.RotationSpeed = 1f;
            //mouseLookScript.lateralSensitivity = originalvalue;
            canDrop = true;
        }
    }

    void ThrowObject()
    {
        //same as drop function, but add force to object before undefining it
        Physics.IgnoreCollision(holdingProp.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        SetLayer(holdingProp, 6);
        holdingRb.isKinematic = false;
        holdingProp.transform.parent = null;
        holdingRb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        holdingProp = null;
    }
    void StopClipping() //function only called when dropping/throwing
    {
        var clipRange = Vector3.Distance(holdingProp.transform.position, transform.position); //distance from holdPos to the camera
        //have to use RaycastAll as object blocks raycast in center screen
        //RaycastAll returns array of all colliders hit within the cliprange
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), clipRange);
        //if the array length is greater than 1, meaning it has hit more than just the object we are carrying
        if (hits.Length > 1)
        {
            //change object position to camera position 
            holdingProp.transform.position = transform.position + new Vector3(0f, -0.5f, 0f); //offset slightly downward to stop object dropping above player 
            //if your player is small, change the -0.5f to a smaller number (in magnitude) ie: -0.1f
        }
    }

    void SetLayer(GameObject obj, int layerNum)
    {
        if (obj == null) { return; }

        obj.layer = layerNum;

        foreach (Transform child in obj.transform)
        {
            if (child == null) continue;
            SetLayer(child.gameObject, layerNum);
        }
    }

    private void UseItem()
    {

        if (holdingProp == null) { return; }

        switch (holdingProp.tag)
        {
            case "Spyglass":
                HandleSpyglass();
                break;
            /*
            case "Camcorder":
                HandleCamcorder();
                break;
            */
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

}
