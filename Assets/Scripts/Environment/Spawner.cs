using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] patterns;
    [SerializeField] private GameObject spawnPosition;

    private void Start()
    {
        StartSpawing();
    }

    public void StartSpawing()
    {
        SpawnRandomPattern();
    }
    
    private void SpawnRandomPattern()
    {
        int patternID = Random.Range(0, patterns.Length);
        Instantiate(patterns[patternID], spawnPosition.transform.position, patterns[patternID].transform.rotation);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "PatternTrigger")
            SpawnRandomPattern();
    }
}
