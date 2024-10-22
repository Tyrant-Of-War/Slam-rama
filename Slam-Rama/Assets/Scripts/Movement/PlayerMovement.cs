using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] PlayerData playerData;

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
    Dash playerDash;

    // Movement control flag
    bool ActiveMovement;

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
        if (movementData != Vector3.zero)
        {
            // Calculate the target rotation based on movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movementData);

            // Smoothly rotate the player towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }

        // If not dashing, apply normal movement
        if (ActiveMovement && playerGrounded && !playerData.isStunned)
        {
            playerRB.drag = groundDrag;
            speed = groundSpeed;
            playerRB.AddForce(movementData * speed * Time.fixedDeltaTime);
        }
        else if (ActiveMovement && !playerGrounded)
        {
            playerRB.drag = airDrag;
            speed = airSpeed;
        }

        // Apply gravity when in the air
        if (!playerGrounded)
        {
            playerRB.AddForce(Vector3.down * gravity * Time.fixedDeltaTime * 100, ForceMode.Force);
        }

        // Adjust player position for slope walking when grounded
        if (playerGrounded && Mathf.Abs(Vector3.Distance(playerRB.position, groundHit.point) - (height / 2)) > 0.05f)
        {
            playerRB.transform.SetPositionAndRotation(new Vector3(
                playerRB.position.x,
                groundHit.point.y + (height / 2 + 0.05f),
                playerRB.position.z),
                playerRB.transform.rotation
            );
        }
    }

    void OnMovement(InputValue stickInput)
    {
        // Capture input data and calculate movement direction
        stickData = stickInput.Get<Vector2>();
        movementData = new Vector3(stickData.x, 0f, stickData.y);

        // Enable movement
        ActiveMovement = true;
    }

    void OnOffMovement()
    {
        // Disable movement
        ActiveMovement = false;
    }

    void OnJump()
    {
        if (playerRB == null)
        {
            Debug.Log("The Player RB is Null!");
        }
        else
        {
            playerJump.ExecuteJump(playerGrounded);
        }
    }

    void OnDash()
    {
        playerDash.ExecuteDash(movementData, playerGrounded, stickData.x, stickData.y);
    }
}
