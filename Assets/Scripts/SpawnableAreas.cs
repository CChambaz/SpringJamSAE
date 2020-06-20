using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableAreas : MonoBehaviour
{
    //[SerializeField] private float spawnRateHurricane;
    //[SerializeField] private float spawnRateCollectible;
    [SerializeField] [Range(0f, 1f)] private float spawnRate;
    [SerializeField] [Range(0f, 1f)] private float spawnRateHurricanesForCollectibles;


    [SerializeField] private GameObject collectible;
    [SerializeField] private GameObject hurricane;

    private void Start()
    {
        float random = Random.Range(0f, 1f);

        if (random < spawnRate)
        {
            float random2 = Random.Range(0f, 1f);
            if (random2 < spawnRateHurricanesForCollectibles)
            {
                Instantiate(collectible, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(hurricane, transform.position, Quaternion.identity);
            }
        }


    }

}
