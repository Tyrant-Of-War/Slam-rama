using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // The ID of the item
    public int itemID;

    // The audio sound of the item pickup
    public AudioSource itemPickup;

    // The manager that spawned the item
    public LevelData levelData;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.isTrigger && other.GetComponent<PlayerMovement>().playerData.itemID == 0)
        {
            // Gives the ID to the player data
            other.GetComponent<PlayerMovement>().playerData.itemID = itemID;

            // Updates the amount of items in existance
            levelData.itemAmount--;

            //play the sound
            itemPickup.Play();

            // Destroys the object
            Destroy(gameObject);
        }
    }
}
