using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]

public class LevelData : ScriptableObject
{
    [SerializeField] int ID;

    // The height at which the player is killed if they go below
    public int killHeight;

    // List of spawn locations for each level
    public List<Vector3> SpawnLocation = new List<Vector3>();

    // Essentially the positions of 2 corners in the level to give the area that items can spawn in
    public Vector3 maxPositivePosition;
    public Vector3 minNegativePosition;

    // The amount of items currently spawned in the level
    public int itemAmount;

    // The height at which items should spawn
    public int itemHeight;

    // Resets values that shouldn't carry through scenes
    private void OnEnable()
    {
        itemAmount = 0;
    }
}
