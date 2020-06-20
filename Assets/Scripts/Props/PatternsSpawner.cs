using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternsSpawner : MonoBehaviour
{
    [SerializeField] private LevelGenerator levelGenerator;
    private List<Pattern> patternsList;
    private bool[] spawnZones;      //true = hasSpace
    [SerializeField] private const int mapSize = 48;

    void Start()
    {
        spawnZones = new bool[mapSize];
        for (int i = 0; i < mapSize; i++)
        {
            spawnZones[i] = true;
        }
    }
}
