using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooserCardPowers : MonoBehaviour
{
    // IDK
    public enum PowerUpType { ItemMagnet, AttackRangeBuff, RecoveryJump }
    public PowerUpType[] powerUps = { PowerUpType.ItemMagnet, PowerUpType.AttackRangeBuff, PowerUpType.RecoveryJump };
    public GameManager gameManager;
    public void GrantPowerUp()
    {
        PowerUpType selectedPowerUp = powerUps[Random.Range(0, powerUps.Length)];
        gameManager.ApplyPowerUp(selectedPowerUp);
    }
}
