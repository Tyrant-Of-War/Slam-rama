using UnityEngine;

public class Knockout : MonoBehaviour
{
    public PlayerData playerData;

    public LevelData levelData;

    // Update is called once per frame
    void Update()
    {
        if (playerData.playerY < levelData.killHeight)
        {
            playerData.falls++;
            playerData.lives--;
            playerData.PlayerObject.transform.position = new Vector3(0, -2.5f, 0);
            playerData.PlayerObject.GetComponent<PlayerMovement>().enabled = false;
            Invoke("respawn", 3);
        }
    }

    void respawn()
    {
        playerData.PlayerObject.transform.position = levelData.SpawnLocation[Random.Range(0, levelData.SpawnLocation.Count)];
        playerData.PlayerObject.GetComponent<PlayerMovement>().enabled = true;
    }
}
