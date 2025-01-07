using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour
{
    // The time for which the fire should exist
    [SerializeField] float oilDuration;

    // The collider for the physical object
    BoxCollider boxCollider;

    // The list of all players ignited so they can be extinguished when this object is destroyed
    List<PlayerData> slipperyPlayers = new List<PlayerData>();

    public AudioSource oilSpill;

    // Start is called before the first frame update
    void Start()
    {
        // Get colliders
        boxCollider = GetComponent<BoxCollider>();

        // Sets the bomb to the correct rotation
        transform.Rotate(-90, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if there is still time left on the fire duration, destorys the bomb if not
        if (oilDuration > 0)
        {
            oilDuration = oilDuration - Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the collider is a player, is not a trigger, and is not the current players hitbox
        if (other.tag == "Player" && !other.isTrigger)
        {
            //Debug.Log("Collision Entry Detected");

            // Sets the player to the ignited state
            other.GetComponent<PlayerMovement>().playerData.isSlippery = true;

            //Plays the oil spill sound
            oilSpill.Play();

            // Adds the player to list of players ignited
            slipperyPlayers.Add(other.GetComponent<PlayerMovement>().playerData);

            //for (int i = 0; i < attackTargets.Count; i++)
            //{
            //    Debug.Log(attackTargets[i].name);
            //}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Checks if the exiting player is in the list
        if (other.tag == "Player" && !other.isTrigger)
        {
            //Debug.Log(other.name + " Has Exited Collision");

            // Extinguishes the player
            other.GetComponent<PlayerMovement>().playerData.isSlippery = false;

            // Adds the player to list of players ignited
            slipperyPlayers.Remove(other.GetComponent<PlayerMovement>().playerData);
        }
    }

    private void OnDestroy()
    {
        // Goes through and extinguishes all players still in the flames
        for (int i = 0; i < slipperyPlayers.Count; i++)
        {
            slipperyPlayers[i].isSlippery = false;
        }

        Destroy(transform.GetChild(0).gameObject);
    }
}
