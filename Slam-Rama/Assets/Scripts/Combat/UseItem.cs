using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    // Player data for checking the held item
    public PlayerData playerData;

    // The list of all power up prefab
    [SerializeField] List<GameObject> powerUps = new List<GameObject>();

    // The visual for the shield powerup
    [SerializeField] GameObject shield;

    // Is called when the player presses the set use item input
    void OnUse()
    {
        // Checks if the player has an item
        if (playerData.itemID == 1) // Bomb
        {
            Instantiate(powerUps[0], transform.position + transform.forward, Quaternion.identity).GetComponent<Bomb>().direction = transform.forward;
        }
        else if (playerData.itemID == 2) // Potions
        {
            playerData.damage = playerData.damage / 2;
        }
        else if (playerData.itemID == 3) // Icicle
        {
            Instantiate(powerUps[1], transform.position + transform.forward, Quaternion.LookRotation(transform.forward, transform.up));
        }
        else if (playerData.itemID == 4) // Molotov
        {
            Instantiate(powerUps[2], transform.position + transform.forward, Quaternion.identity).GetComponent<Molotov>().direction = transform.forward;
        }
        else if (playerData.itemID == 5) // Shield
        {
            shield.SetActive(true);

            playerData.isShielded = true;
        }
        else if (playerData.itemID == 6) // Oil Spill
        {
            Instantiate(powerUps[3], transform.position, Quaternion.identity);
        }

        //Debug.Log("Item Used: " +  playerData.itemID);

        // Clears the players item slot
        playerData.itemID = 0;

        GetComponent<Knockout>().gameUI.UpdatePowerUp();
    }
}
