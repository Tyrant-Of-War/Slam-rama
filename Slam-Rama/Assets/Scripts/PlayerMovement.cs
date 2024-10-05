using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Used to get the vector 2 from the input data
    Vector2 stickData;

    // Used to add force to the players rigidbody
    Vector3 movementData;

    // Used to determine the force the player will jump with
    [SerializeField] float jumpForce;

    // Used to manage how fast the player will move
    [SerializeField] int speed;

    // Used to change the drag based on the players airborne status
    [SerializeField] int airDrag;
    [SerializeField] int groundDrag;

    // Used to change the speed of the player based on airborne status
    [SerializeField] int airSpeed;
    [SerializeField] int groundSpeed;

    // Used to check if the player is touching the ground
    bool playerGrounded;

    // Used to get the players height for accurate raycasting
    float height;

    // Used to create my own gravity
    [SerializeField] float gravity;

    // Used to limit the players dashing
    [SerializeField] int dashCooldown;

    // Used to store the time the player is able to dash again
    float dashAgainTime;

    // The players physics based "body"
    Rigidbody playerRB;

    // A mask for the layer that is ground
    [SerializeField] LayerMask ground;

    // Used to get the exact point the players raycast hits the ground at
    RaycastHit groundHit;


    // Start is called before the first frame update
    void Start()
    {
        // Assigns the players rigidbody
        playerRB = GetComponent<Rigidbody>();

        // Sets the player to defualt state of grounded
        playerGrounded = false;

        // Sets gravity to the defualt value
        //gravity = 9.8f;

        // Gets the height of the player
        height = GetComponent<CapsuleCollider>().height;

        dashAgainTime = 0;
    }

    // FixedUpdate is called dynamically for physics based operations
    void FixedUpdate()
    {

        // Applies force to the players rigidbody with extra speed than below as running is true
        playerRB.AddRelativeForce(movementData * (speed) * Time.fixedDeltaTime);

        // Shoots a raycasted line down from the player that detects ground
        playerGrounded = Physics.Raycast(playerRB.transform.position, Vector3.down, out groundHit, ((height / 2) + 0.1f), ground);

        // Changes the drag based on whether or not the player is airborne
        if (playerGrounded == true)
        {
            playerRB.drag = groundDrag;
            speed = groundSpeed;
        }
        else if (playerGrounded == false)
        {
            playerRB.drag = airDrag;
            speed = airSpeed;

            // My own gravity system which I am only using because gravity is turned off for the player so I can customise their jump arc more
            playerRB.AddForce(Vector3.down * gravity * Time.fixedDeltaTime * 100, ForceMode.Force);
        }

        // This checks if the players position is too high or too low based on the raycast and adjusts correctly but only plays when grounded as to not mess with jumping
        // This exists to allow the player to walk up slopes
        if (playerGrounded == true && (Vector3.Distance(playerRB.position, groundHit.point) < ((height / 2) + 0.05f) || Vector3.Distance(playerRB.position, groundHit.point) > ((height / 2) + 0.05f)))
        {
            playerRB.transform.SetPositionAndRotation(new Vector3(playerRB.position.x, groundHit.point.y + ((height / 2) + 0.05f), playerRB.position.z), playerRB.transform.rotation);
        }
    }

    void OnMovement(InputValue stickInput)
    {
        // Gets the vector 2 from the stick input
        stickData = stickInput.Get<Vector2>();

        // Puts the vector 2 into a vector 3
        movementData = new Vector3(stickData.x, 0f, stickData.y);
    }

    void OnJump()
    {
        if (playerGrounded == true && Time.timeScale == 1)
        { 
            // Zeros out the players up and down movement for a smooth jump
            playerRB.velocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);

            // Adds upward force to the player 
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // Look at if you need the delta time in here since its just one instance of force it's not like you do it in jump so its gotta be consistant
    void OnDash()
    {

        // Checks if the player is moving and they are not in the cooldown period
        if ((stickData.x != 0f || stickData.y != 0f) && Time.time > dashAgainTime && playerGrounded == true)
        {
            // Adds an impulse force in the players movement direction
            playerRB.AddRelativeForce(movementData * speed * Time.deltaTime * 10f, ForceMode.Impulse);

            // Sets the time the player can dash again
            dashAgainTime = Time.time + dashCooldown;
        }
    }
}
