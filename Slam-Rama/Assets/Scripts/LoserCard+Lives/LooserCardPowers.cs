using UnityEngine;

public class LooserCardPowers : MonoBehaviour
{
    [SerializeField] LoserCardsUiManager playerListHolder;

    public enum PowerUpType { ItemMagnet, AttackRangeBuff, RecoveryJump, DamageBuff, DashDamage, StartWithPowerUp }
    // List of all possible power-ups (6 of them)
    public struct PowerUp
    {
        public PowerUpType Type { get; set; }

        public PowerUp(PowerUpType type)
        {
            Type = type;
        }
    }

    // List of all possible power-ups
    public PowerUp[] powerUps =
    {
    new PowerUp(PowerUpType.ItemMagnet),
    new PowerUp(PowerUpType.AttackRangeBuff),
    new PowerUp(PowerUpType.RecoveryJump),
    new PowerUp(PowerUpType.DamageBuff),
    new PowerUp(PowerUpType.DashDamage),
    new PowerUp(PowerUpType.StartWithPowerUp)
};
    //// An array that holds all the powerups to pick :3
    //// Can easily loop through when needed
    //public PowerUpType[] offeredPowerUps;

    //public void OfferPowerUps()
    //{
    //    // Randomly pick 3 power-ups to offer from the full list of power-ups
    //    offeredPowerUps = new PowerUpType[3];
    //    for (int i = 0; i < 3; i++)
    //    {
    //        offeredPowerUps[i] = (PowerUpType)Random.Range(0, 6); // Pick a random power-up
    //    }
    //    // These power-ups would then be shown to the player in the UI (handled elsewhere)
    //}





}
