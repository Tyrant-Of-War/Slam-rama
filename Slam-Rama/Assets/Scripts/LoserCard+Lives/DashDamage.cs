using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DashDamage : MonoBehaviour
{
    // Used to access the is dashing variable
    [SerializeField] private PlayerMovement playerMovement;

    bool dashDamageOn;

    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
    {
        if (playerMovement.playerData.loserCardID == 5)
        {
            dashDamageOn = true;
        }
        else
        {
            dashDamageOn = false;
        }
    }

    private void OnTriggerEnter(Collider other) //when colider enter 
    {
        if (playerMovement.playerDash.isDashing && other.gameObject.CompareTag("Player") && !other.isTrigger) //if the player is dashing and collides with another object with the player tag...
        {
            other.gameObject.GetComponent<Damage>().DamagePlayer(3); //deal 3 points of damage
            other.gameObject.GetComponent<Knockback>().RunKnockback(transform.forward, 0.5f); //move them the other way (knockback)
        }
    }
}

