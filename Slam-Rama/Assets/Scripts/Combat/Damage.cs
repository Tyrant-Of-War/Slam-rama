using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    // For editing the damage value so it can be accessed by other scripts
    public PlayerData playerData;

    // Used to manage how long the play is stunned
    float stunTimer;

    // Used to stun the player
    Rigidbody playerRB; 

    private void Start()
    {
        // Gets rigid body
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Checks if the player is in the stun state
        if (playerData.isStunned == true)
        {
            // Checks if the stun time has run out, unstuns if so
            if (stunTimer < 0)
            {
                playerData.isStunned = false;
            }
            else
            {
                // Counts down stun timer
                stunTimer = stunTimer - Time.deltaTime;
            }
        }
    }

    // Is called when a player is found by the attack script of another player
    public void DamagePlayer(int damage)
    {
        // Adds the given damage to the player data
        playerData.damage += Mathf.RoundToInt((Mathf.Pow(playerData.damage, 2f) / 5000) + damage);

        if (!playerData.isStunned)
        {
            // Sets the player to stunned
            playerData.isStunned = true;
            // Zeros out the players velocity before knockback is applied
            playerRB.velocity = Vector3.zero;
            // Calculates stun duration based on the damage taken
            stunTimer = damage / 10;
        }
    }

    // Is called by the icicle powerup
    public void FreezePlayer()
    {
        // Sets the player to stunned
        playerData.isStunned = true;
        // Zeros out the players velocity before knockback is applied
        playerRB.velocity = Vector3.zero;
        // Calculates stun duration based on the damage taken
        stunTimer = 3f;
    }
}
