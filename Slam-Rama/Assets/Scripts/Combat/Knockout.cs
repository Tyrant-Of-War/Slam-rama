using System.Collections;
using UnityEngine;

public class Knockout : MonoBehaviour
{
    public PlayerData playerData;

    public LevelData levelData;
    bool once = true;

    // Update is called once per frame
    void Update()
    {
        if (playerData.playerY < levelData.killHeight)
        {
            playerData.falls++;
            playerData.lives--;
            playerData.PlayerObject.transform.position = new Vector3(0, -2.5f, 0);
            playerData.PlayerObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            if (once)
            {
                once = false;
                StartCoroutine(respawn());

            }
        }
    }

    IEnumerator respawn()
    {
        yield return new WaitForSeconds(3);
        playerData.PlayerObject.transform.position = levelData.SpawnLocation[Random.Range(0, levelData.SpawnLocation.Count)];
        playerData.PlayerObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
        once = true;
    }
}
