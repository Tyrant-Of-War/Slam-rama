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

    private void Start()
    {
        // Resizes the item based on what it is so it doesn't look dumb
        if (itemID == 1)
        {
            transform.localScale = Vector3.one * 3;
        }
        else if (itemID == 2)
        {
            transform.localScale = Vector3.one;
        }
        else if (itemID == 3)
        {
            transform.localScale = Vector3.one * 2;
        }
        else if (itemID == 4)
        {
            transform.localScale = Vector3.one * 0.75f;
        }
        else if (itemID == 5)
        {
            transform.localScale = Vector3.one * 3;

            transform.Rotate(90, 0, 0);
        }
        else if (itemID == 6)
        {
            transform.localScale = Vector3.one;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !other.isTrigger && other.GetComponent<PlayerMovement>().playerData.itemID == 0 && other.GetComponent<PlayerMovement>().playerData.isShielded != true)
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
