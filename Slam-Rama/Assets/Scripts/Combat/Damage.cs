using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public PlayerData playerData;

    // Used to manage how long the play is stunned
    float stunTimer;

    Rigidbody playerRB; 

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (playerData.isStunned == true)
        {
            if (stunTimer < 0)
            {
                playerData.isStunned = false;
            }
            else
            {
                stunTimer = stunTimer - Time.deltaTime;
            }
        }
    }

    public void damagePlayer(int damage)
    {
        playerData.damage = playerData.damage + damage;
        playerData.isStunned = true;
        playerRB.velocity = Vector3.zero;
        stunTimer = damage / 10;
    }
}
