using System.Collections;
using UnityEngine;

public class Knockout : MonoBehaviour
{
    // Used to check the players current y position
    public PlayerData playerData;

    // Used to check the kill hieght of the current level
    public LevelData levelData;

    // Used to check if the respawn coroutine has finished
    bool isDone;
    public InGameUI gameUI;
    private void Start()
    {
        // Sets default true
        isDone = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if a coroutine is currently going 
        if (isDone)
        {
            // Checks if the player has gone below the kill height of the level
            if (playerData.playerY < levelData.killHeight)
            {
                // Checks if the player has lives to use
                if (playerData.lives > 0)
                {

                    // Updates the player data values
                    playerData.falls++;
                    playerData.lives--;
                    playerData.damage = 0;
                    gameUI.ReduceLife(playerData.ID);

                    // Moves the player to the bottom of the map and freezes their moving while they are being repspawned
                    transform.position = new Vector3(0, -2.5f, 0);
                    GetComponent<PlayerMovement>().enabled = false;
                    GetComponent<CapsuleCollider>().enabled = false;
                    isDone = false;
                    StartCoroutine(respawn());

                }
                else if (playerData.lives <= 0)
                {
                    // Updates player data
                    playerData.falls++;
                    playerData.damage = 0;
                    playerData.isDead = true;

                    // Moves the player to the bottom of the map and freezes their moving until the next round
                    transform.position = new Vector3(0, -2.5f, 0);
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            }
        }
    }

    IEnumerator respawn()
    {
        // Waits for 3 seconds to do what it wants to do
        yield return new WaitForSeconds(3);
        //turn off ridgedbody so transof might work
        this.GetComponent<Rigidbody>().transform.position = levelData.SpawnLocation[Random.Range(0, levelData.SpawnLocation.Count)];
        // Unfreezes the player constraints but making sure to keep the rotation frozen
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
        // Lets update know its done respawning
        isDone = true;
    }
}
