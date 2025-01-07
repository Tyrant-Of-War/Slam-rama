using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryJump
{
    private Rigidbody playerRB;
    private float jumpForce;

    bool hasJumped = false;

    public AudioClip jumpSound;

    public RecoveryJump(Rigidbody rb, float jumpForce)
    {
        this.playerRB = rb;
        this.jumpForce = jumpForce;
    }

    //Allows for two jumps
    public bool ExecuteJump()
    {
        //plays the jump sound
        PlayerSoundManager.Instance.PlaySound(jumpSound);

        if (!hasJumped)
        {
            playerRB.AddForce(Vector3.up * jumpForce * 5, ForceMode.Impulse);

            hasJumped = true;

            return true;
        }

        return false;
    }
}
