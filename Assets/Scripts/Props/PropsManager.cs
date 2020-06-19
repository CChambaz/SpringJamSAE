using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsManager : MonoBehaviour
{

    public float propsSpeed = 3f;

    [SerializeField] private Vector3 spawnPosition;

    [SerializeField] private float spawnXMin;
    [SerializeField] private float spawnXMax;

    [SerializeField] private GameObject woodenBox;
    [SerializeField] private GameObject metalBeam;

    [SerializeField] private float spawnTime;
    private float currentSpawnTime = 0;


    // Update is called once per frame
    void Update()
    {
        currentSpawnTime += Time.deltaTime;
        if (currentSpawnTime >= spawnTime)
        {
            float random = Random.Range(0, 2);
            if (random == 0)
            {
                Instantiate(woodenBox, new Vector3(Random.Range(spawnXMin, spawnXMax), spawnPosition.y, spawnPosition.z), Quaternion.identity);
            }
            else
            {
                Instantiate(metalBeam, new Vector3(Random.Range(spawnXMin, spawnXMax), spawnPosition.y, spawnPosition.z), Quaternion.identity);
            }

            currentSpawnTime = 0;
        }
    }
}
