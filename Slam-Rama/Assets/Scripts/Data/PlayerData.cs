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

    public GameObject PlayerObject;

    // The ID of the item the player is currently holding 0 is no item
    public int itemID;

    public Playercontrols inputActions;

    // Resets values that shouldn't carry through scenes
    private void OnEnable()
    {
        damage = 0;

        isStunned = false;

        playerY = 0;

        lives = 3;

        isInvincible = false;

        itemID = 0;
    }

    private void Awake()
    {
        damage = 0;

        isStunned = false;

        playerY = 0;

        lives = 3;

        isInvincible = false;

        itemID = 0;
    }
}
