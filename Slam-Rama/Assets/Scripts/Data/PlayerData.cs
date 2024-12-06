using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public int ID;

    // Damage accumulated from attacks used for multiplying knockback
    public int damage;

    // Used by the damage script to tell the movement script this player is currently stunned
    public bool isStunned;

    // Used by the game manager to set the colour of the player
    public Material playerMaterial;

    // Used to set player lives at the start of the round
    public int livesMax;

    // The actual lives value that is edited during runtime
    public int lives;

    // The amount of times a player has been knocked out
    public int falls;

    // The amount of times this player has knocked out another
    public int knockouts;

    // The game object of the player that uses all of this data
    public GameObject PlayerObject;

    // The ID of the item the player is currently holding 0 is no item
    public int itemID;

    // The input control scheme for the player
    public Playercontrols inputActions;

    // Used to tell if the player is out of the game
    public bool isDead;

    // Used to tell if the player is currently attacking
    public bool isAttacking;

    // Resets everything
    public void ResetData()
    {
        damage = 1;

        isStunned = false;

        lives = livesMax;

        falls = 0;

        knockouts = 0;

        PlayerObject = null;

        itemID = 0;

        isDead = false;

        isAttacking = false;
    }

    // Resets stuff between rounds
    public void ResetRoundData()
    {
        damage = 1;

        isStunned = false;

        lives = livesMax;

        itemID = 0;

        isDead = false;

        isAttacking = false;
    }

    // Resets things between lives
    public void ResetLifeData()
    {
        damage = 1;

        isStunned = false;

        isDead = false;

        isAttacking = false;
    }
}
