using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public PlayerData playerData;

    // Input and movement variables
    Vector2 stickData;
    Vector3 movementData;

    // Jump force
    [SerializeField] float jumpForce;

    // Speed management variables
    [SerializeField] int speed;
    [SerializeField] int airSpeed;
    [SerializeField] int groundSpeed;

    // Drag management variables
    [SerializeField] int airDrag;
    [SerializeField] int groundDrag;

    // Ground check variables
    bool playerGrounded;
    float height;

    // Gravity settings
    [SerializeField] float gravity;

    // Dash cooldown settings
    [SerializeField] int dashCooldown;
    float dashAgainTime;

    // Rigidbody and ground detection
    Rigidbody playerRB;
    [SerializeField] LayerMask ground;
    RaycastHit groundHit;

    // Components for Jump and Dash
    Jump playerJump;
    public Dash playerDash;

    // Movement control flag
    bool ActiveMovement;

    Quaternion targetRotation;

    [SerializeField] Animator animator;

    void Start()
    {
        // Initialize components and variables
        playerRB = GetComponent<Rigidbody>();
        height = GetComponent<CapsuleCollider>().height;

        // Initialize the Jump and Dash components
        playerJump = new Jump(playerRB, jumpForce);
        playerDash = new Dash(playerRB, dashCooldown, speed * 5, this);  // Pass 'this' for coroutine use
    }

    void FixedUpdate()
    {
        // Ground check via raycast
        playerGrounded = Physics.Raycast(playerRB.transform.position, Vector3.down, out groundHit, (height / 2) + 0.1f, ground);

        // Only rotate if there's movement input
        if (movementData.magnitude > 0.1f)
        {
            // Calculate the target rotation based on movement direction
            targetRotation = Quaternion.LookRotation(movementData);

            // Smoothly rotate the player towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        else
        {
            transform.rotation = targetRotation;
        }

        

        // Why this here?
        playerRB.drag = groundDrag;

        // Checks if movement is being recorded, if the player is grounded, if the player is not stunned
        if (ActiveMovement && playerGrounded)
        {
            // Checks if player is currently attacking or not
            if (!playerData.isAttacking)
            {
                // Applies the movement data
                playerRB.AddForce(movementData * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
            }
            else if (playerData.isAttacking) 
            {
                // Applies the movement data halved if they are attacking
                playerRB.AddForce((movementData * speed * Time.fixedDeltaTime) * 0.25f, ForceMode.VelocityChange);
            }
            
        }
        else if (ActiveMovement && !playerGrounded)
        {
            // Switches to air drag values if player is not grounded
            playerRB.drag = airDrag;
            speed = airSpeed;
        }

        // Animates
        if (movementData.magnitude > 0.1f)
        {
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }

        // Apply gravity when in the air
        if (!playerGrounded)
        {
            playerRB.AddForce(Vector3.down * gravity * Time.fixedDeltaTime * 100, ForceMode.Force);
        }
    }

    // Is called when the player presses WASD or uses the left joystick
    void OnMovement(InputValue stickInput)
    {
        // Checks if player is stunned or dead
        if (!playerData.isStunned && !playerData.isDead) 
        {
            // Capture input data and calculate movement direction
            stickData = stickInput.Get<Vector2>();
            movementData = new Vector3(stickData.x, 0f, stickData.y);
        }
        else // Gives movement zero value if so
        {
            movementData = Vector3.zero;
        }
        

        // Enable movement
        ActiveMovement = true;
    }

    // Is called when movement input is no longer recorded
    void OnOffMovement()
    {
        // Disable movement
        ActiveMovement = false;
    }

    // Is called when space or A is pressed
    void OnJump()
    {
        // Checks if the player has a rigidbody
        if (playerRB == null)
        {
            //Debug.Log("The Player RB is Null!");
        }
        else // Calls jump function and animates if so
        {
            playerJump.ExecuteJump(playerGrounded);

            // Needs fixing will test
            animator.SetBool("jump", true);
            animator.SetBool("jump", false);
        }
    }

    // Is called when B or shift is pressed
    void OnDash()
    {
        // Calls dash function
        playerDash.ExecuteDash(movementData, playerGrounded, stickData.x, stickData.y);
    }

}
