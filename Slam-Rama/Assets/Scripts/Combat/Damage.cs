using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Damage : MonoBehaviour
{
    // Used to tell if the player should be damaged everynow and again from fire
    public bool isIgnited;

    // Used to delay ticks from the fire
    float fireDelay;

    // For editing the damage value so it can be accessed by other scripts
    public PlayerData playerData;

    // Used to manage how long the play is stunned
    float stunTimer;

    // Used to stun the player
    Rigidbody playerRB;

    // The rumble script
    [SerializeField] Rumble rumble;

    [SerializeField] GameObject shield;

    public InGameUI gameUI;

    public AudioClip shieldBlock;

    public AudioClip shieldBreak;

    private void Start()
    {
        // Gets rigid body
        playerRB = GetComponent<Rigidbody>();

        // Sets the delay ready for if the player becomes ignited
        fireDelay = 0.5f;

        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        shield.SetActive(false);
    }

    private void Update()
    {
        // Checks if the player is in the stun state
        if (playerData.isStunned == true)
        {
            // Checks if the stun time has run out, unstuns if so
            if (stunTimer > 0)
            {
                // Counts down stun timer
                stunTimer = stunTimer - Time.deltaTime;  
            }
            else
            {
                playerData.isStunned = false;
            }
        }

        // Checks if the player lit
        if (isIgnited)
        {
            // Checks if anytime left in the delay does damage and resets if not
            if (fireDelay > 0)
            {
                fireDelay = fireDelay - Time.deltaTime;
            }
            else
            {
                DamagePlayer(3f);

                fireDelay = 0.5f;
            }
        }
    }

    // Is called when a player is found by the attack script of another player
    public void DamagePlayer(float damage)
    {
        // Checks if player has a sheild or not
        if (playerData.isShielded == true)
        {
            // Checks if damage is high enough to break shield
            if (damage > 3)
            {
                //plays the sound to show blocking

                // Adds the given damage to the player data
                playerData.damage += Mathf.RoundToInt((Mathf.Pow(playerData.damage, 2f) / 5000) + damage);

                rumble.SetRumble(damage / 10, damage / 5);

                if (!playerData.isStunned)
                {
                    // Sets the player to stunned
                    playerData.isStunned = true;
                    // Zeros out the players velocity before knockback is applied
                    playerRB.velocity = Vector3.zero;
                    // Calculates stun duration based on the damage taken
                    stunTimer = damage / 10;
                }
                
                // Removes the shield and plays the sound 
                playerData.isShielded = false;

                shield.SetActive(false);

                PlayerSoundManager.Instance.PlaySound(shieldBreak);
            }
            else
            {
                PlayerSoundManager.Instance.PlaySound(shieldBlock);
            }
        }
        else
        {
            // Adds the given damage to the player data
            playerData.damage += Mathf.RoundToInt((Mathf.Pow(playerData.damage, 2f) / 5000) + damage);

            rumble.SetRumble(damage / 10, damage / 5);

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

        // Updates the damage UI
        gameUI.UpdatePlayerDamage();
    }

    // Is called by the icicle powerup
    public void FreezePlayer()
    {
        // Sets the player to stunned
        playerData.isStunned = true;
        // Zeros out the players velocity before knockback is applied
        playerRB.velocity = Vector3.zero;
        // Calculates stun duration based on the damage taken
        stunTimer = 1.5f;
    }
}
