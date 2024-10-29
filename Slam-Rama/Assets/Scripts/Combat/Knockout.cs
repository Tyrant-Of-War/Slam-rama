using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockout : MonoBehaviour
{
    public PlayerData playerData;

    public LevelData levelData;

    // Update is called once per frame
    void Update()
    {
        if (playerData.playerY > levelData.killHeight)
        {
            playerData.falls++;
            playerData.lives--;
            respawn();
        }
    }

    void respawn()
    {
        this.transform.position = levelData.SpawnLocation[Random.Range(0, levelData.SpawnLocation.Count)];
    }
}
