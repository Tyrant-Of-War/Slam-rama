using System.Collections;
using UnityEngine;

public class Knockout : MonoBehaviour
{
    //// Used to check the players current y position
    //public PlayerData playerData;

    //// Used to check the kill hieght of the current level
    //public LevelData levelData;

    //// Used to check if the respawn coroutine has finished
    //bool isDone;

    //public InGameUI gameUI;

    //private void Start()
    //{
    //    // Sets default true
    //    isDone = true;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    // Checks if a coroutine is currently going 
    //    if (isDone)
    //    {
    //        // Checks if the player has gone below the kill height of the level
    //        if (transform.position.y < levelData.killHeight)
    //        {
    //            // Checks if the player has lives to use
    //            if (playerData.lives > 0)
    //            {

    //                // Updates the player data values
    //                playerData.falls++;
    //                playerData.lives--;
    //                playerData.damage = 0;
    //                gameUI.ReduceLife(playerData.ID);

    //                // Moves the player to the bottom of the map and freezes their moving while they are being repspawned
    //                transform.position = new Vector3(0, -2.5f, 0);
    //                GetComponent<PlayerMovement>().enabled = false;
    //                GetComponent<CapsuleCollider>().enabled = false;
    //                isDone = false;
    //                StartCoroutine(respawn());

    //            }
    //            else if (playerData.lives <= 0)
    //            {
    //                // Updates player data
    //                playerData.falls++;
    //                playerData.damage = 0;
    //                playerData.isDead = true;

    //                // Moves the player to the bottom of the map and freezes their moving until the next round
    //                transform.position = new Vector3(0, -2.5f, 0);
    //                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    //            }
    //        }
    //    }
    //}

    //IEnumerator respawn()
    //{
    //    // Waits for 3 seconds to do what it wants to do
    //    yield return new WaitForSeconds(3);
    //    //turn off ridgedbody so transof might work
    //    this.GetComponent<Rigidbody>().transform.position = levelData.SpawnLocation[Random.Range(0, levelData.SpawnLocation.Count)];
    //    // Unfreezes the player constraints but making sure to keep the rotation frozen
    //    GetComponent<PlayerMovement>().enabled = true;
    //    GetComponent<CapsuleCollider>().enabled = true;
    //    // Lets update know its done respawning
    //    isDone = true;
    //}

    // Used to check the players current y position
    public PlayerData playerData;

    // Used to check the kill hieght of the current level
    public LevelData levelData;

    // Used to check if the respawn coroutine has finished
    bool isRespawning;

    // Used to count down how long the player should respawning for
    float respawnTimer;

    public InGameUI gameUI;

    // Sets the knockout sound
    public AudioSource knockoutSound;

    // Sets the final knockout sound
    public AudioSource finalKnockoutAudio;

    // Sets the respawn sound
    public AudioSource respawnSound;

    private void Start()
    {
        // Sets default true
        isRespawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if a coroutine is currently going 
        if (!isRespawning)
        {
            // Checks if the player has gone below the kill height of the level
            if (transform.position.y < levelData.killHeight)
            {
                //play the knockout sound
                knockoutSound.Play();

                // Checks if the player has lives to use
                if (playerData.lives > 0)
                {

                    // Updates the player data values
                    playerData.falls++;
                    playerData.lives--;
                    playerData.damage = 0;
                    gameUI.ReduceLife(playerData.ID);

                    // Moves the player to the bottom of the map and freezes their moving while they are being repspawned
                    GetComponent<Rigidbody>().position = new Vector3(0, -2.5f, 0);
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GetComponent<PlayerMovement>().enabled = false;
                    GetComponent<CapsuleCollider>().enabled = false;

                    // Reset some of the player data ready for when the player respawns
                    playerData.ResetLifeData();

                    // Sets them to the respawning state
                    isRespawning = true;

                    // Sets the respawn timer to 3 seconds
                    respawnTimer = 3f;

                }
                else if (playerData.lives <= 0)
                {
                    //play the final knockout sound
                    finalKnockoutAudio.Play();

                    // Updates player data
                    playerData.falls++;
                    playerData.damage = 1;
                    playerData.isDead = true;

                    // Moves the player to the bottom of the map and freezes their moving until the next round
                    GetComponent<Rigidbody>().position = new Vector3(0, -2.5f, 0);
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                    GetComponent<PlayerMovement>().enabled = false;
                    GetComponent<CapsuleCollider>().enabled = false;
                }
            }
        }
        else if (isRespawning)
        {
            if (respawnTimer > 0) 
            {
                respawnTimer = respawnTimer - Time.deltaTime;
            }
            else
            {
                respawnTimer = 0f;

                // Sets the players position to a random one of the spawn locations in level data
                GetComponent<Rigidbody>().position = levelData.SpawnLocation[Random.Range(0, levelData.SpawnLocation.Count)];
                GetComponent<Rigidbody>().velocity = Vector3.zero;

                // Plays the respawning audio 
                respawnSound.Play();

                // Unfreezes the player movement
                GetComponent<PlayerMovement>().enabled = true;
                GetComponent<CapsuleCollider>().enabled = true;

                // Sets them to no longer repsawnig
                isRespawning = false;
            }
        }
    }

    //IEnumerator respawn()
    //{
    //    // Waits for 3 seconds to do what it wants to do
    //    yield return new WaitForSeconds(3);
    //    //turn off ridgedbody so transof might work
    //    this.GetComponent<Rigidbody>().transform.position = levelData.SpawnLocation[Random.Range(0, levelData.SpawnLocation.Count)];
    //    // Unfreezes the player constraints but making sure to keep the rotation frozen
    //    GetComponent<PlayerMovement>().enabled = true;
    //    GetComponent<CapsuleCollider>().enabled = true;
    //    // Lets update know its done respawning
    //    isDone = true;
    //}
}
