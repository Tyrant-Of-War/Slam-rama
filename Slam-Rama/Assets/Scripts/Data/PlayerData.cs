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

    //Used to check player pos for killzone purpose
    public float playerY;

    // Used by the game manager to set the colour of the player
    public Material playerMaterial;

    // Used to set player lives at the start of the round
    public int livesMax;

    // The actual lives value that is edited during runtime
    public int lives;

    // Used to tell if the player is currently invunerable
    public bool isInvincible;

    // Holds players active powerup
    public List<string> powerUps = new List<string>();

    // The amount of times a player has been knocked out
    public int falls;

    // The amount of times this player has knocked out another
    public int knockouts;

    // Tracks the first player who loses all lives
    public PlayerData firstPlayerOut = null;

    // Tracks the last player standing in each round
    public PlayerData lastPlayerStanding = null;

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

    // Resets values that shouldn't carry through scenes
    private void OnEnable()
    {
        damage = 1;

        isStunned = false;

        playerY = 0;

        lives = livesMax;

        isInvincible = false;

        itemID = 1;

        isDead = false;

        isAttacking = false;
    }

    private void Awake()
    {
        damage = 1;

        isStunned = false;

        playerY = 0;

        lives = livesMax;

        isInvincible = false;

        itemID = 1;

        isDead = false;

        isAttacking = false;
    }
    public void ResetData()
    {
        damage = 1;

        isStunned = false;

        playerY = 0;

        lives = livesMax;

        isInvincible = false;

        itemID = 1;

        isDead = false;

        isAttacking = false;
    }
}
