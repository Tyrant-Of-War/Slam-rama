using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class Attack : MonoBehaviour
{
    public PlayerData playerData;

    // The attack hitbox
    BoxCollider attackHitbox;

    // Used to keep the player in an attacking state for a short time after attacking
    float attackTimer;

    // List of players in the attack hitbox
    List<GameObject> attackTargets = new List<GameObject>();

    // Used to animate the punch
    [SerializeField] Animator animator;

    private void Start()
    {
        // Gets the attack hitbox
        attackHitbox = GetComponent<BoxCollider>();
        // Gets the animator
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // Checks if the player is in the attacking state
        if (playerData.isAttacking)
        {
            // Checks if the timer for the attacking state is above 0
            if (attackTimer > 0)
            {
                // Counts the timer down
                attackTimer -= Time.deltaTime;
            }
            else // If the timer has run out the attacking state is set back to false
            {
                playerData.isAttacking = false;
            }
        }
    }

    // Is called when the player presses either attack input
    void OnAttack(InputValue inputValue)
    {
        // Checks if the player is not stunned or dead
        if (!playerData.isStunned && !playerData.isDead)
        {
            // Plays the punch animation
            animator.SetTrigger("punch");

            // Checks whether input was for a heavy or light attack
            if (inputValue.Get<float>() == 1)
            {
                // Calls the light attack function
                LightAttack();
            }
            else if (inputValue.Get<float>() == -1)
            {
                // Calls the heavy attack function
                HeavyAttack();
            }
        }
    }

    void LightAttack()
    {
        //Debug.Log("Light Attack");

        // Sets attacking to true
        playerData.isAttacking = true;

        // Sets the is attacking timer to 1f
        attackTimer = 0.5f;

        // Runs through all players in the attack hitbox and calls the knockback and damage functions
        for (int i = 0; i < attackTargets.Count; i++)
        {
            //Debug.Log(attackTargets[i].name);

            // Calls the damage function and feeds it the single point of damage to be applied by a light attack
            attackTargets[i].GetComponent<Damage>().DamagePlayer(1);
            // Calls the knockback function and feeds it a direction and multiplier
            attackTargets[i].GetComponent<Knockback>().RunKnockback(this.transform.forward, 0.01f);
        }
    }

    void HeavyAttack()
    {
        //Debug.Log("Heavy Attack");

        // Sets attacking to true
        playerData.isAttacking = true;

        // Sets the is attacking timer to 1f
        attackTimer = 1f;

        // Runs through all players in the attack hitbox and calls the knockback and damage functions
        for (int i = 0; i < attackTargets.Count; i++)
        {
            //Debug.Log(attackTargets[i].name);

            // Calls the damage function and feeds it the ten points of damage to be applied by a heavy attack
            attackTargets[i].GetComponent<Damage>().DamagePlayer(5);
            // Calls the knockback function and feeds it a direction and multiplier
            attackTargets[i].GetComponent<Knockback>().RunKnockback(this.transform.forward, 1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the collider is a player, is not a trigger, and is not the current players hitbox
        if (other.tag == "Player" && !other.isTrigger && other != GetComponent<CapsuleCollider>())
        {
            //Debug.Log("Collision Entry Detected");

            // Adds the collider game object to the target list
            attackTargets.Add(other.gameObject);

            //for (int i = 0; i < attackTargets.Count; i++)
            //{
            //    Debug.Log(attackTargets[i].name);
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Checks if the exiting player is in the list
        if (attackTargets.Contains(other.gameObject) && !other.isTrigger)
        {
            //Debug.Log(other.name + " Has Exited Collision");

            // Removes the player from the list
            attackTargets.Remove(other.gameObject);
        }
    }
}
