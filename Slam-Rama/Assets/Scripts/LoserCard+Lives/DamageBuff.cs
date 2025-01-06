using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : MonoBehaviour
{
    // The multiplier applied to the damage
    public float multiplier = 1.5f;

    // Applies the multiplier to incoming damage
    public float ApplyMultiplier(float damage)
    {
        return damage * multiplier;
    }

    // Activates a temporary damage multiplier
    public void ActivateMultiplier(float newMultiplier, float duration)
    {
        multiplier = newMultiplier;
    }

}
