
using Magic;
using UnityEditor.VersionControl;
using UnityEngine;

/*
    This file has a commented version with details about how each line works. 
    The commented version contains code that is easier and simpler to read. This file is minified.
*/


/// <summary>
/// Main script for third-person movement of the character in the game.
/// Make sure that the object that will receive this script (the player) 
/// has the Player tag and the Character Controller component.
/// </summary>
public class ThirdPersonController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Speed ​​at which the character moves. It is not affected by gravity or jumping.")]
    [SerializeField] private float _velocity = 5f;
    [Tooltip("This value is added to the speed value while the character is sprinting.")]
    [SerializeField] private float _sprintAdittion = 3.5f;
    [Tooltip("The higher the value, the higher the character will jump.")]
    [SerializeField] private float _jumpForce = 18f;
    [Tooltip("Stay in the air. The higher the value, the longer the character floats before falling.")]
    [SerializeField] private float _jumpTime = 0.85f;
    [Space]
    [Tooltip("Force that pulls the player down. Changing this value causes all movement, jumping and falling to be changed as well.")]
    [SerializeField] private float _gravity = 9.8f;

    float jumpElapsedTime = 0;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController cc;

    // Player states
    bool isJumping = false;
    bool isSprinting = false;
    bool isCrouching = false;

    // Inputs
    float inputHorizontal;
    float inputVertical;
    bool inputJump;
    bool inputCrouch;
    bool inputSprint;

    public bool IsBlocked = false;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        GameController.Instance.OnPush += TriggerPushAnimation;
    }


    void Update()
    {
        if (IsBlocked) return;
        // Input checkers
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputJump = Input.GetAxis("Jump") == 1f;
        inputSprint = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton1);
        inputCrouch = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton1);


        if ( inputCrouch )
            isCrouching = !isCrouching;

        //Animator
        if ( cc.isGrounded && animator != null )
        {
            // Calcular velocidad real (sin Y)
            Vector3 horizontalVelocity = cc.velocity;
            horizontalVelocity.y = 0;
            float speed = horizontalVelocity.magnitude;
            float inputMagnitude = new Vector2(inputHorizontal, inputVertical).magnitude;

            isSprinting = speed > 0.5f && inputSprint && inputMagnitude > 0.1f;
            float speedValue = 0f;

            if (inputMagnitude < 0.1f || speed < 0.2f)
            {
                speedValue = 0f; // Idle
            }
            else if (!isSprinting)
            {
                speedValue = 0.5f; // Walk
            }
            else
            {
                speedValue = 1f; // Run
            }
            animator.SetFloat("Speed", speedValue, 0.2f, Time.deltaTime);

        }


        //Jump
        if ( inputJump && cc.isGrounded )
        {
            isJumping = true;
            animator.SetTrigger("Jump");
        }

        HeadHittingDetect();

    }

    private void FixedUpdate()
    {
        // Direction base
        Vector3 direction = new Vector3(inputHorizontal, 0, inputVertical);
        if (direction.magnitude > 1f) direction.Normalize();
        // Sprinting velocity boost or crounching desacelerate
        float velocityAdittion = 0;
        if ( isSprinting )
            velocityAdittion = _sprintAdittion;
        if (isCrouching)
            velocityAdittion =  - (_velocity * 0.50f); // -50% velocity

        // Direction movement
        float directionX = inputHorizontal * (_velocity + velocityAdittion) * Time.deltaTime;
        float directionZ = inputVertical * (_velocity + velocityAdittion) * Time.deltaTime;
        float directionY = 0;
        float velocityModifier = 0f;
        float finalSpeed = _velocity + velocityModifier;
        Vector3 moveDirection = transform.TransformDirection(direction) * finalSpeed * Time.deltaTime;

        // Jump handler
        if ( isJumping )
        {
            // Apply inertia and smoothness when climbing the jump
            directionY = Mathf.SmoothStep(_jumpForce, _jumpForce * 0.30f, jumpElapsedTime / _jumpTime) * Time.deltaTime;

            // Jump timer
            jumpElapsedTime += Time.deltaTime;
            if (jumpElapsedTime >= _jumpTime)
            {
                isJumping = false;
                jumpElapsedTime = 0;
            }
        }

        // Add gravity to Y axis
        directionY = directionY - _gravity * Time.deltaTime;


        // --- Character rotation --- 

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Relate the front with the Z direction (depth) and right with X (lateral movement)
        forward = forward * directionZ;
        right = right * directionX;

        if (directionX != 0 || directionZ != 0)
        {
            float angle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }

        // --- End rotation ---

        
        Vector3 verticalDirection = Vector3.up * directionY;
        Vector3 horizontalDirection = forward + right;

        Vector3 moviment = verticalDirection + horizontalDirection;
        cc.Move( moviment );

    }

    private void TriggerPushAnimation()
    {
        animator.SetTrigger("Push");
    }

    //This function makes the character end his jump if he hits his head on something
    void HeadHittingDetect()
    {
        float headHitDistance = 1.1f;
        Vector3 ccCenter = transform.TransformPoint(cc.center);
        float hitCalc = cc.height / 2f * headHitDistance;

        // Uncomment this line to see the Ray drawed in your characters head
        // Debug.DrawRay(ccCenter, Vector3.up * headHeight, Color.red);

        if (Physics.Raycast(ccCenter, Vector3.up, hitCalc))
        {
            jumpElapsedTime = 0;
            isJumping = false;
        }
    }

    public void SetBlocked(bool value)
    {
        IsBlocked = value;
        //print($"[ThirdPersonController] Movement blocked: {(IsBlocked ? "YES" : "NO")}");
    }

}
