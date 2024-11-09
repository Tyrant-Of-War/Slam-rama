using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // The min and max of the item delay
    [SerializeField] float delayMin;
    [SerializeField] float delayMax;

    // Used to set the delay between item spawns
    float delay;

    // The prefab for an item
    [SerializeField] GameObject item;

    // The level data of the level this item manager is in
    [SerializeField] LevelData levelData;

    // The most recently spawned item
    GameObject currentItem;

    // The max amount of items that can be spawned
    [SerializeField] int itemLimit;

    // Start is called before the first frame update
    void Start()
    {
        // Sets the initial delay
        delay = Random.Range(delayMin, delayMax);
    }

    // Update is called once per frame
    void Update()
    {
        if (levelData.itemAmount < itemLimit)
        {
            // Counts down the delay
            delay -= Time.deltaTime;

            if (delay < 0)
            {
                // Instantiates the item at a random x and z position with the level bounds
                currentItem = Instantiate(item, new Vector3(Random.Range(levelData.minNegativePosition.x, levelData.maxPositivePosition.x), 1f, Random.Range(levelData.minNegativePosition.z, levelData.maxPositivePosition.z)), Quaternion.identity);

                // Just sets a random ID for now
                currentItem.GetComponent<PickUp>().itemID = Random.Range(1, 5);

                // Gives the item the level data so the item amount can be updated when it is destroyed
                currentItem.GetComponent<PickUp>().levelData = levelData;

                // Increases the amount of items existing in the level data
                levelData.itemAmount++;

                // Generates a new random delay
                delay = Random.Range(delayMin, delayMax);
            }
        }
    }
}
