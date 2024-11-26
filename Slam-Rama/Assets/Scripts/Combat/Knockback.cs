using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    // Uses the damage value to proportionally scale force applied
    public PlayerData playerData;

    // Used to apply the knockback
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the rigidbody
        rb = GetComponent<Rigidbody>();
    }

    // Is called when the attack script of a player finds this player in its list
    public void RunKnockback(Vector3 direction, float knockback)
    {
        //Debug.Log("Force Applied");

        // Applies force in the fed direction, with the fed knockback amount, multiplied by the damage the player has accrued
        rb.AddForce((playerData.damage * knockback) * direction, ForceMode.Impulse);
    }

    // Called by the bomb powerup
    public void explodeKnockback(float knockback, Vector3 explosionPosition, float explosionRadius)
    {
        // Applies an explosion force with the fed values
        rb.AddExplosionForce(knockback * playerData.damage, explosionPosition, explosionRadius);
    }
}
