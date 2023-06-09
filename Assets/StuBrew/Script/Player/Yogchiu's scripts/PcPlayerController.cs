using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using NaughtyAttributes;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PcPlayerController : MonoBehaviour
{
    public float MouseSpeed = 2f;
    private Rigidbody body;

    [Tooltip("The tracked headCamera object")]
    public Camera headCamera;
    [Tooltip("The object that represents the forward direction movement, usually should be set as the camera or a tracked controller")]
    public Transform forwardFollow;

    public bool useMovement = true;
    [EnableIf("useMovement"), FormerlySerializedAs("moveSpeed")]
    [Tooltip("Movement speed when isGrounded")]
    public float maxMoveSpeed = 1.5f;
    [EnableIf("useMovement")]
    [Tooltip("Movement acceleration when isGrounded")]
    public float moveAcceleration = 10f;
    [EnableIf("useMovement")]

    public bool useGrounding = true;
    [EnableIf("useGrounding"), Tooltip("Maximum height that the body can step up onto"), Min(0)]
    public float maxStepHeight = 0.3f;
    [EnableIf("useGrounding"), Tooltip("Maximum angle the player can walk on"), Min(0)]
    public float maxStepAngle = 30f;
    [EnableIf("useGrounding"), Tooltip("The layers that count as ground")]
    public LayerMask groundLayerMask;
    [EnableIf("useGrounding"), Tooltip("Movement acceleration when isGrounded")]
    public float groundedDrag = 4f;
    [Tooltip("Movement acceleration when grounding is disabled")]
    public float flyingDrag = 4f;

    [Tooltip("Platforms will move the player with them. A platform is an object with the Transform component on it")]
    public bool allowPlatforms = true;
    [EnableIf("useGrounding"), Tooltip("The layers that platforming will be enabled on, will not work with layers that the HandPlayer can't collide with")]
    public LayerMask platformingLayerMask = ~0;

    Vector3 moveDirection;
    float turningAxis;
    bool isGrounded = false;

    float movementDeadzone = 0.1f;
    float turnDeadzone = 0.4f;

    RaycastHit newClosestHit;
    float highestPoint;
    bool tempDisableGrounding = false;
    public CapsuleCollider bodyCapsule;

    [Tooltip("Collider for crouching")]
    public CapsuleCollider crouchCapsule;
    public float normalCameraYValue = 1.32f;
    public float crouchCameraYValue = 0.9f;
    private bool isCrouch = false;

    float groundedOffset = 0.1f;

    //Crosshair
    [Tooltip("Crosshair Normal")]
    [SerializeField] GameObject crossHairNorm;
    [Tooltip("Crosshair Interactable")]
    [SerializeField] GameObject crossHairHit;


    [Space(10)]
    [Header("Interactable")]
    //controllable Objects
    GameObject controllable;
    GameObject pickupable;
    float holdDist = 0.5f;


    PlayerInput playerInput;
    InputActionMap IActionMap;
    InputActionMap MovementActionMap;
    InputActionMap UiActionMap;
    bool isInteracting = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        CrosshairHit(false);
        if (!playerInput)
            playerInput = GetComponent<PlayerInput>();
        IActionMap = playerInput.actions.FindActionMap("Interactable");

        MovementActionMap = playerInput.actions.FindActionMap("GenericMovement");
        MovementActionMap.Enable();

        UiActionMap = playerInput.actions.FindActionMap("UI");
        UiActionMap.Enable();
        headCamera.transform.localPosition = new Vector3(headCamera.transform.localPosition.x, normalCameraYValue, headCamera.transform.localPosition.z);
    }

    protected virtual void FixedUpdate()
    {

        if (useMovement)
        {
            UpdateRigidbody();
            Ground();
            HoldPickupable();
        }
    }

    protected virtual void UpdateRigidbody()
    {
        var move = AlterDirection(moveDirection);
        var yVel = body.velocity.y;

        //1. Moves velocity towards desired push direction (autohand)
        /*
        if (pushAxis != Vector3.zero)
        {
            body.velocity = Vector3.MoveTowards(body.velocity, pushAxis, pushingAcceleration * Time.fixedDeltaTime);
            body.velocity *= Mathf.Clamp01(1 - pushingDrag * Time.fixedDeltaTime);
        }
        */

        //2. Moves velocity towards desired climb direction (autohand)
        /*
        if (climbAxis != Vector3.zero)
        {
            body.velocity = Vector3.MoveTowards(body.velocity, climbAxis, climbingAcceleration * Time.fixedDeltaTime);
            body.velocity *= Mathf.Clamp01(1 - climbingDrag * Time.fixedDeltaTime);
        }
        */
        //3. Moves velocity towards desired movement direction
        if (move != Vector3.zero && CanInputMove())
        {

            var newVel = Vector3.MoveTowards(body.velocity, move * maxMoveSpeed, moveAcceleration * Time.fixedDeltaTime);
            if (newVel.magnitude > maxMoveSpeed)
                newVel = newVel.normalized * maxMoveSpeed;
            body.velocity = newVel;
        }

        //5. Checks if gravity should be turned off (autohand)
        /*
        if (IsClimbing() || pushAxis.y > 0) 
            body.useGravity = false;
        */

        //4. This creates extra drag when grounded to simulate foot strength, or if flying greats drag in every direction when not moving (autohand)
        if (move.magnitude <= movementDeadzone && isGrounded)
            body.velocity *= (Mathf.Clamp01(1 - groundedDrag * Time.fixedDeltaTime));
        else if (!useGrounding)
            body.velocity *= (Mathf.Clamp01(1 - flyingDrag * Time.fixedDeltaTime));

        //6. This will keep velocity if consistent when moving while falling  (autohand)
        if (body.useGravity)
            body.velocity = new Vector3(body.velocity.x, yVel, body.velocity.z);

        //SyncBodyHead();

        Crouch(isCrouch);
    }

    protected virtual void Ground()
    {

        isGrounded = false;
        newClosestHit = new RaycastHit();

        //if (!tempDisableGrounding && useGrounding && !IsClimbing() && !(pushAxis.y > 0))
        if (!tempDisableGrounding && useGrounding)
        {
            highestPoint = -1;

            float stepAngle;
            float dist;
            float scale = transform.lossyScale.x > transform.lossyScale.z ? transform.lossyScale.x : transform.lossyScale.z;

            var maxStepHeight = this.maxStepHeight;
            /*
            maxStepHeight *= climbAxis.y > 0 ? climbUpStepHeightMultiplier : 1;
            maxStepHeight *= pushAxis.y > 0 ? pushUpStepHeightMultiplier : 1;
            */
            maxStepHeight *= scale;

            var point1 = scale * bodyCapsule.center + transform.position + scale * bodyCapsule.height / 2 * -Vector3.up + (maxStepHeight + scale * bodyCapsule.radius * 2) * Vector3.up;
            var point2 = scale * bodyCapsule.center + transform.position + (scale * bodyCapsule.height / 2f + groundedOffset) * -Vector3.up;
            var radius = scale * bodyCapsule.radius * 2 + Physics.defaultContactOffset * 2;
            var groundHits = Physics.SphereCastAll(point1, radius, -Vector3.up, Vector3.Distance(point1, point2) + scale * bodyCapsule.radius * 4, groundLayerMask, QueryTriggerInteraction.Ignore);

            for (int i = 0; i < groundHits.Length; i++)
            {
                var hit = groundHits[i];

                if (hit.collider != bodyCapsule)
                {
                    if (hit.point.y >= point2.y && hit.point.y <= point2.y + maxStepHeight)
                    {
                        stepAngle = Vector3.Angle(hit.normal, Vector3.up);
                        dist = hit.point.y - transform.position.y;

                        if (stepAngle < maxStepAngle && dist > highestPoint)
                        {
                            isGrounded = true;
                            highestPoint = dist;
                            newClosestHit = hit;
                        }
                    }
                }
            }

            if (isGrounded)
            {
                body.velocity = new Vector3(body.velocity.x, 0, body.velocity.z);
                body.position = new Vector3(body.position.x, newClosestHit.point.y, body.position.z);
                transform.position = body.position;
            }

            body.useGravity = !isGrounded;
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    protected virtual bool CanInputMove()
    {
        //return (allowClimbingMovement || !IsClimbing());
        return true;
    }

    Vector3 AlterDirection(Vector3 moveAxis)
    {
        if (useGrounding)
            return Quaternion.AngleAxis(forwardFollow.eulerAngles.y, Vector3.up) * (new Vector3(moveAxis.x, moveAxis.y, moveAxis.z));
        else
            return forwardFollow.rotation * (new Vector3(moveAxis.x, moveAxis.y, moveAxis.z));
    }

    void CrosshairHit(bool hit)
    {
        crossHairNorm.SetActive(!hit);
        crossHairHit.SetActive(hit);
    }

    void ToggleInteractiveInput()
    {
        if (isInteracting)
        {
            IActionMap.Disable();
            MovementActionMap.Enable();
            isInteracting = false;
        }
        else
        {
            IActionMap.Enable();
            MovementActionMap.Disable();
            isInteracting = true;
        }
    }

    void DisableInteractiveInput()
    {
        if (isInteracting)
        {
            IActionMap.Disable();
            MovementActionMap.Enable();
            isInteracting = false;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Vector2 deltaTurn = context.ReadValue<Vector2>();
        //up down
        headCamera.transform.Rotate(Vector3.right * -deltaTurn.y * 0.1f * MouseSpeed);
        //left right
        transform.Rotate(Vector3.up * deltaTurn.x * 0.05f * MouseSpeed);

        Vector3 yRotation = headCamera.transform.localEulerAngles;
        yRotation.z = 0;
        yRotation.y = 0;
        headCamera.transform.localRotation = Quaternion.Euler(yRotation);

        //check ray
        RaycastHit hit;
        CrosshairHit(false);
        if (controllable)
        {
            Highlightable obj = controllable.GetComponent<Highlightable>();
            obj.SetHighLight(false);
        }
        if (Physics.Raycast(headCamera.transform.position, headCamera.transform.forward, out hit, 1.0f))
        {
            if (hit.transform.tag == "Interactable_PC" || hit.transform.tag == "Pickupable_PC")
            {
                CrosshairHit(true);
                controllable = hit.transform.gameObject;
                Highlightable obj = controllable.GetComponent<Highlightable>();
                obj.SetHighLight(true);
            }

        }
        else 
        {
            controllable = null;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        bool useDeadzone = false;
        bool useRelativeDirection = false;
        Vector2 axis = context.ReadValue<Vector2>();
        moveDirection.x = (!useDeadzone || Mathf.Abs(axis.x) > movementDeadzone) ? axis.x : 0;
        moveDirection.z = (!useDeadzone || Mathf.Abs(axis.y) > movementDeadzone) ? axis.y : 0;
        if (useRelativeDirection)
            moveDirection = transform.rotation * moveDirection;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (controllable)
        {
            ToggleInteractiveInput();
            if (controllable.transform.tag == "Pickupable_PC")
            {
                if (pickupable)
                {
                    int LayerIgnoreRaycast = LayerMask.NameToLayer("Grabbable");
                    SetGameLayerRecursive(pickupable, LayerIgnoreRaycast);
                    Rigidbody rb = pickupable.GetComponent<Rigidbody>();
                    rb.isKinematic = false;
                    holdDist = 0.5f;
                    pickupable = null;
                }
                else
                {
                    pickupable = controllable.transform.gameObject;
                    Rigidbody rb = pickupable.GetComponent<Rigidbody>();
                    rb.isKinematic = true;
                    int LayerIgnoreRaycast = LayerMask.NameToLayer("NoCollide");
                    if (pickupable.TryGetComponent(out PickUpPC pickupableObject))
                    {
                        holdDist = pickupableObject.GetHoldDist();
                    }
                    SetGameLayerRecursive(pickupable, LayerIgnoreRaycast);
                }
            }
        }
        else
        {
            DisableInteractiveInput();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
            body.AddForce(Vector3.up * Mathf.Sqrt(0.8f * -Physics.gravity.y), ForceMode.VelocityChange);
        //body.AddForce(Vector3.up * Mathf.Sqrt(0.5f * -Physics.gravity.y), ForceMode.VelocityChange);
    }

    public void OnIRotateLeft(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (controllable)
        {
            if (controllable.TryGetComponent(out IRange rotatableObject))
            {
                rotatableObject.OnLeft();
            }
        }
    }
    public void OnIRotateRight(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (controllable)
        {
            if (controllable.TryGetComponent(out IRange rotatableObject))
            {
                rotatableObject.OnRight();
            }
        }
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (controllable)
        {
            if (controllable.TryGetComponent(out IClickable clickableObject))
            {
                clickableObject.OnClick();
            }
        }
    }
    public void OnScroll(InputAction.CallbackContext context)
    {
        if (pickupable)
        {
            pickupable.transform.RotateAround(pickupable.transform.position, headCamera.transform.right, (context.ReadValue<float>()==0?0: context.ReadValue<float>() >1 ? 1: -1) * 5f);
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
            isCrouch = true;
        if (context.canceled)
            isCrouch = false;
    }

    void HoldPickupable(){
        if(pickupable){
            MovementActionMap.Enable();
            pickupable.transform.position = headCamera.transform.position + headCamera.transform.forward * holdDist;
        }
    }

    void Crouch(bool isCrouch = false)
    {
        if (isCrouch)
        {
            bodyCapsule.enabled = false;
            crouchCapsule.enabled = true;
            headCamera.transform.localPosition = new Vector3(headCamera.transform.localPosition.x, crouchCameraYValue, headCamera.transform.localPosition.z);
        }
        else
        {
            bodyCapsule.enabled = true;
            crouchCapsule.enabled = false;
            headCamera.transform.localPosition = new Vector3(headCamera.transform.localPosition.x, normalCameraYValue, headCamera.transform.localPosition.z);
        }
    }

    private void SetGameLayerRecursive(GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        foreach (Transform child in gameObject.transform)
        {
            SetGameLayerRecursive(child.gameObject, layer);
        }
    }
}
