using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryJump : MonoBehaviour
{
    private Rigidbody playerRB;
    private float jumpForce;
    private int maxJumps = 2;
    private int currentJumps = 0;

    public RecoveryJump(Rigidbody rb, float jumpForce)
    {
        this.playerRB = rb;
        this.jumpForce = jumpForce;
    }

    // Resets the jump count
    public void JumpManager(bool isGrounded)
    {
        if (currentJumps < maxJumps)
        {
            currentJumps++;
        }
        if (isGrounded)
        {
            currentJumps = 0;
        }
    }
    //Allows for two jumps
    public void ExecuteJump()
    {
        if (currentJumps < maxJumps)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            currentJumps++;
        }
    }
}
