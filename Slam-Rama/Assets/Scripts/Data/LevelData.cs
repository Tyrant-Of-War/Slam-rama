using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class LevelData : ScriptableObject
{
    [SerializeField] int ID;

    public int killHeight;

    // List of spawn locations for each level
    public List<Vector3> SpawnLocation = new List<Vector3>();
}
