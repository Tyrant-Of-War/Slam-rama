using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashDamage : MonoBehaviour
{
    // Used to access the is dashing variable
    [SerializeField] private PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); //calls the player movement script 
    }
    private void OnTriggerEnter(Collider other) //when colider enter 
    {
        if (playerMovement.playerDash.isDashing && other.gameObject.CompareTag("Player") && !other.isTrigger) //if the player is dashing and collides with another object with the player tag...
        {
            other.gameObject.GetComponent<Damage>().damagePlayer(3); //deal 3 points of damage
            other.gameObject.GetComponent<Knockback>().RunKnockback(transform.forward,0.5f); //move them the other way (knockback)
        }
    }
}

