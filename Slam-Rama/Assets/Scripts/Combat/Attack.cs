using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Attack : MonoBehaviour
{
    // The attack hitbox
    BoxCollider attackHitbox;

    // List of players in the attack hitbox
    List<GameObject> attackTargets = new List<GameObject>();

    [SerializeField] Animator animator;

    private void Start()
    {
        // Gets the attack hitbox
        attackHitbox = GetComponent<BoxCollider>();
        animator = GetComponentInChildren<Animator>();
    }

    void OnAttack(InputValue inputValue)
    {
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

    void LightAttack()
    {
        //Debug.Log("Light Attack");

        // Runs through all players in the attack hitbox and calls the knockback function, then removes that player from the list
        for (int i = 0; i < attackTargets.Count; i++)
        {
            //Debug.Log(attackTargets[i].name);

            // Calls the knockback function and feeds it a direction and multiplier
            attackTargets[i].GetComponent<Damage>().damagePlayer(1);
            attackTargets[i].GetComponent<Knockback>().RunKnockback(this.transform.forward, 0.01f);
        }
    }

    void HeavyAttack()
    {
        //Debug.Log("Heavy Attack");

        // Runs through all players in the attack hitbox and calls the knockback function, then removes that player from the list
        for (int i = 0; i < attackTargets.Count; i++)
        {
            //Debug.Log(attackTargets[i].name);

            // Calls the knockback function and feeds it a direction and multiplier
            attackTargets[i].GetComponent<Damage>().damagePlayer(10);
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
