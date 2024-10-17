using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField] int ID;

    // Damage accumulated from attacks used for multiplying knockback
    public int damage;

    // Used by the damage script to tell the movement script this player is currently stunned
    public bool isStunned;

    // Resets values that shouldn't carry through scenes
    private void OnEnable()
    {
        damage = 0;
    }
}
