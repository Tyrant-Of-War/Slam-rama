using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public PlayerData playerData;

    // Used to manage how long the play is stunned
    float stunTimer;

    private void Update()
    {
        if (playerData.isStunned == true)
        {
            stunTimer = stunTimer - Time.deltaTime;

            if (stunTimer < 0)
            {
                playerData.isStunned = false;
            }
        }
    }

    public void damagePlayer(int damage)
    {
        playerData.damage = playerData.damage + damage;
        playerData.isStunned = true;
        stunTimer = damage / 10;
    }
}
