using UnityEngine;

public class Jump
{
    private Rigidbody playerRB;
    private float jumpForce;

    public Jump(Rigidbody rb, float jumpForce)
    {
        this.playerRB = rb;
        this.jumpForce = jumpForce;
    }

    public void ExecuteJump(bool isGrounded)
    {
        if (isGrounded)
        {
            // Adds upward force to the player
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
