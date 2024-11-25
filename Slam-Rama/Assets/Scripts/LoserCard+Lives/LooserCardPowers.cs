using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooserCardPowers : MonoBehaviour
{
    public enum PowerUpType { ItemMagnet, AttackRangeBuff, RecoveryJump, DamageBuff, MovementBuff, DashDamage }
    // List of all possible power-ups (6 of them)
    public PowerUpType[] powerUps = { PowerUpType.ItemMagnet, PowerUpType.AttackRangeBuff, PowerUpType.RecoveryJump, PowerUpType.DamageBuff, PowerUpType.MovementBuff, PowerUpType.DashDamage };
    // An array that holds all the powerups to pick :3
    // Can easily loop through when needed
    public PowerUpType[] offeredPowerUps;
    // An array to hold the 3 powerups being shown
    public GameManager gameManager;

    public void OfferPowerUps()
    {
        // Randomly pick 3 power-ups to offer from the full list of power-ups
        offeredPowerUps = new PowerUpType[3];
        for (int i = 0; i < 3; i++)
        {
            offeredPowerUps[i] = (PowerUpType)Random.Range(0, 6); // Pick a random power-up
        }
        // These power-ups would then be shown to the player in the UI (handled elsewhere)
    }
    public void GrantPowerUp(PowerUpType chosenPowerUp)
    {
        // Apply the powerup the player picked
        gameManager.ApplyPowerUp(chosenPowerUp);
    }
}
