using System.Collections;
using UnityEngine;

public class Knockout : MonoBehaviour
{
    public PlayerData playerData;

    public LevelData levelData;
    bool once = true;

    int lives;

    private void Start()
    {
        lives = playerData.lives;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerData.playerY < levelData.killHeight)
        {

            if (once && lives > 0)
            {
                playerData.falls++;
                lives--;
                playerData.damage = 0;
                playerData.PlayerObject.transform.position = new Vector3(0, -2.5f, 0);
                playerData.PlayerObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                once = false;
                StartCoroutine(respawn());


            }
            else if (lives <= 0)
            {
                gameObject.SetActive(false);
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
