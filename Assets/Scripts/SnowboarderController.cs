using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.EventSystems;

using Sirenix.OdinInspector;

public class SnowboarderController : MonoBehaviour
{
    public static event Action OnMoved;
    public static event Action OnBraked;
    public static event Action OnJumped;
    

    [Header("Movement Settings")]
    public float maxMoveSpeed = 15f;
    public float moveAcceleration = 10f;

    public float jumpForce = 15f;
    public float maxJumpHight = 15f;
    public float flipAcceleration = 50f;
    public float brakeDrag = 5f;
    public float normalDrag = 0.2f;
    public float airFallSpeed = 15f;
    public float groundSnapForce = 20f;

    public bool IsGrounded => isGrounded;
    [Header("Camera Settings")]
    public Camera mainCamera;
    public float normalFOV = 75f;
    public float maxSpeedFOV = 90f;

    [Header("Ground Detection")]
    public LayerMask groundLayerMask;

    [Header("UI")]
    public GameObject resetHintText; // Assign in Inspector

    private SwipeInput swipe;
    private Rigidbody rb;
    private bool isGrounded;
    private bool isAccelerating;
    private bool isBraking;
    private bool isFlipping;
    private bool isBackFlipping;
    private bool isJumping;

    private float currentFlipSpeed = 0f;
    private float currentBackFlipSpeed = 0f;

    private Vector3 slopeNormal;

    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.3f;
    public bool isTutorialRunning;
    private void Awake()
    {
        if(PlayerPrefs.GetInt("Tutorial",0) == 0)
        {
            isTutorialRunning = true;
        }
        else
        {
            isTutorialRunning = false;
        }    
    }

    void Start()
    {
        swipe = GetComponent<SwipeInput>();


        rb = GetComponent<Rigidbody>();
        rb.linearDamping = normalDrag;

        if (resetHintText != null)
            resetHintText.SetActive(false);
    }

    void Update()
    {
        CheckGround();
        HandleFlip();

        UpdateCameraFOV();
        CheckUpsideDownAndShowHint();
        HandleTouchInput();

        HandleJump();

        if (isAccelerating)
        {

        }


        if (isBraking)
        {


        }

        if (isTutorialRunning)
        {
            if (TutorialManager.CurrentPhase == TutorialPhase.MoveOnly || TutorialManager.CurrentPhase == TutorialPhase.AllEnabled)
            {
                if (rb.linearVelocity.magnitude > 4f)
                    OnMoved?.Invoke();
            }
        }
       


    }

    private void HandleJump()
    {
        if(isTutorialRunning)
            if (TutorialManager.CurrentPhase != TutorialPhase.JumpOnly && TutorialManager.CurrentPhase != TutorialPhase.AllEnabled)
            return;
        float swipeY = (float)swipe.GetSwipeY();
       // print(swipeY);

        if (swipeY <= 0.1f || !isGrounded || IsUpsideDown()) return;

       

        float y = 
            (jumpForce * jumpForce * rb.mass);

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, y, rb.linearVelocity.z);
       
        if(isTutorialRunning)
             OnJumped?.Invoke();

    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    // =========================
    // Public Command Methods
    // =========================

    public void StartAccelerate()
    {
        if(isTutorialRunning)
        {
            if (TutorialManager.CurrentPhase != TutorialPhase.MoveOnly && TutorialManager.CurrentPhase != TutorialPhase.AllEnabled)
                return;
            isAccelerating = true;
        }
        else
        {
            isAccelerating = true;
        }
        

        
    }


    public void StopAccelerate() => isAccelerating = false;

    public void StartBrake()
    {
        if(isTutorialRunning)
        {
            if (TutorialManager.CurrentPhase != TutorialPhase.BrakeOnly && TutorialManager.CurrentPhase != TutorialPhase.AllEnabled)
                return;

            isBraking = true;
            OnBraked?.Invoke();
        }   
        else
        {
            isBraking = true;
        }
       
    }


    public void StopBrake()
    {
        isBraking = false;
        rb.linearVelocity = Vector3.zero;
        rb.linearDamping = normalDrag;
    }

    public void TryJump()
    {
        if (isGrounded)
        {
            isJumping = true;
        }
        else
        {
            rb.AddForce(Vector3.down * airFallSpeed * 2f, ForceMode.Acceleration);
            

        }
    }
    public void StartBackFlip() => isBackFlipping = true;
    public void StopBackFlip() => isBackFlipping = false;

    public void StartFlip() => isFlipping = true;
    public void StopFlip() => isFlipping = false;

    // =========================
    // Core Movement
    // =========================

    private void HandleMovement()
    {
        if (isGrounded)
        {
            rb.linearDamping = isBraking ? brakeDrag : normalDrag;

            if (isAccelerating)
            {
                Vector3 moveDir = Vector3.ProjectOnPlane(transform.forward, slopeNormal).normalized;

                rb.AddForce(moveDir * maxMoveSpeed * PowerUpHandler.SpeedMulti, ForceMode.Acceleration);
            }

            if (isBraking)
            {
                Vector3 brakeDir = -Vector3.ProjectOnPlane(transform.forward, slopeNormal).normalized;
                rb.AddForce(brakeDir * maxMoveSpeed, ForceMode.Acceleration);
            }

            //if (isJumping)
            //{
            //    isJumping = false;
            //}
        }
        else
        {
            rb.AddForce(Vector3.down * airFallSpeed, ForceMode.Acceleration);

            if (isBraking && isGrounded)
            {
                rb.AddForce(Vector3.down * airFallSpeed * 5, ForceMode.Acceleration);
            }
        }
    }

    private void HandleFlip()
    {
        if (swipe.IsSwiping)
        {
             if(isTutorialRunning)
            {
                if (swipe.IsSwiping && (TutorialManager.CurrentPhase == TutorialPhase.FlipOnly || TutorialManager.CurrentPhase == TutorialPhase.AllEnabled))
                    rb.AddTorque(Vector3.right * flipAcceleration * swipe.GetSwipeX(), ForceMode.Impulse);
            }
             else
            {
                rb.AddTorque(Vector3.right * flipAcceleration * swipe.GetSwipeX(), ForceMode.Impulse);
            }
           
            


        }
    }

    private void CheckGround()
    {
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, 3.25f, groundLayerMask))
        {
            isGrounded = true;
            slopeNormal = hit.normal;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void UpdateCameraFOV()
    {
        float targetFOV = normalFOV;

        if (isAccelerating)
        {
            float speedFactor = rb.linearVelocity.magnitude / maxMoveSpeed;
            targetFOV = Mathf.Lerp(normalFOV, maxSpeedFOV, speedFactor / 3f);
        }

        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFOV, Time.deltaTime * 5f);
    }

    // =========================
    // Upside Down Logic & UI
    // =========================
    public bool IsUpsideDown()
    {
        float dot = Vector3.Dot(transform.up, Vector3.up);
        bool isUpsideDown = dot < 0f;

        //Debug.Log($"[UpsideDownCheck] Dot: {dot}, UpsideDown: {isUpsideDown}");

        return isUpsideDown && isGrounded;
    }
    [SerializeField] PhysicsMaterial basePlayer, down;
    [SerializeField] Collider collider;

    private void CheckUpsideDownAndShowHint()
    {
        bool isUpsideDown = IsUpsideDown();

        collider.material = isUpsideDown ? down : basePlayer;


        if (resetHintText != null)
            resetHintText.SetActive(isUpsideDown);
    }

    private void HandleTouchInput()
    {

        if ((Input.touchCount > 0) && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Debug.Log("[Touch] Tap Detected");
            float currentTime = Time.time;
            float delta = currentTime - lastTapTime;

            if (delta <= doubleTapThreshold)
            {
                Debug.Log("[Touch] Double Tap Detected");

                if (IsUpsideDown())
                {
                    rb.isKinematic = true;
                    transform.DORotate(Vector3.zero, 0.5f);
                    transform.DOMoveY(transform.position.y + 5, 0.5f).OnComplete(() => rb.isKinematic = false);
                }
                else
                {
                    Debug.Log("[Reset] Double tap detected but player is not upside down.");
                }
            }
            else
            {
               // Debug.Log("[Touch] Single tap - waiting for double tap...");
            }

            lastTapTime = currentTime;
        }
    }

    [Button]
    public void ResetRotation()
    {
        if (IsUpsideDown())
        {
            rb.isKinematic = true;
            transform.DORotate(Vector3.zero, 0.5f);
            transform.DOMoveY(transform.position.y + 5, 0.5f).OnComplete(() => rb.isKinematic = false);
        }
        else
        {
            Debug.Log("[Reset] Double tap detected but player is not upside down.");
        }
    }
    public float GetHeightAboveGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity))
        {
            return hit.distance;
        }
        return 0f;
    }
}
