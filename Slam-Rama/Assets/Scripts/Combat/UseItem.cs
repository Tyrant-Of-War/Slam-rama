using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseItem : MonoBehaviour
{
    // Player data for checking the held item
    public PlayerData playerData;

    void OnUse()
    {
        // Checks if the player has an item
        if (playerData.itemID != 0)
        {
            Debug.Log("Item Used: " +  playerData.itemID);

            // Clears the players item slot
            playerData.itemID = 0;
        }
    }
}
