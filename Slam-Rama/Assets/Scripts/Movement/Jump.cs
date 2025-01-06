using UnityEngine;

public class Jump
{
    // For applying the force during jump
    private Rigidbody playerRB;

    // The multiplier determining how powerful the jump should be
    private float jumpForce;

    public AudioSource jumpSound;

    // A class for the jump attributes
    public Jump(Rigidbody rb, float jumpForce)
    {
        this.playerRB = rb;
        this.jumpForce = jumpForce;
    }

    // Is called by player movement when jump input is detected
    public void ExecuteJump(bool isGrounded)
    {
        // Checks if the player is on the ground
        if (isGrounded)
        {
            // Adds upward force to the player
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //playerRB.AddRelativeForce(Vector3.forward * jumpForce / 1.5f, ForceMode.Impulse);
            //Plays the sound
            jumpSound.Play();
        }
    }
}
