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
        
        playerMovement = GetComponent<PlayerMovement>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (playerMovement.playerDash.isDashing && other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            other.gameObject.GetComponent<Damage>().damagePlayer(3);
            other.gameObject.GetComponent<Knockback>().RunKnockback(transform.forward,0.5f);
        }
    }
}

