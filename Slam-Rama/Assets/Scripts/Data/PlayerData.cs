using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField] int ID;

    // Damage accumulated from attacks used for multiplying knockback
    public int damage;

    // Used by the damage script to tell the movement script this player is currently stunned
    public bool isStunned;

    //Used to check player pos for killzone purpose
    public float playerY;

    // Used by the game manager to set the colour of the player
    public Material playerMaterial;

    //Used to set player lives at the start of the round
    public int lives = 3;

    //Holds players active powerup
    public List<string> powerUps = new List<string>();

    // Resets values that shouldn't carry through scenes
    private void OnEnable()
    {
        damage = 0;

        isStunned = false;

        playerY = 0;
    }
}
