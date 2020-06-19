using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private float mapSize;
    [SerializeField] private Vector2 cylinderAmount;
    [SerializeField] private Vector2 cylinderOffset;
    [SerializeField] private Vector2 upperPlaformeOffset;
    [SerializeField] private PropsManager propsManager;
    [SerializeField] private GameObject cylinderPrefab;
    [SerializeField] private GameObject sidePrefab;
    [SerializeField] private GameObject upperPlatformePrefab;
    [SerializeField] private GameObject convoyerColliderPrefab;

    private Vector3 currentPosition = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void GenerateMap()
    {
        // Spawn the collider of the convoyer
        Vector3 convoyerCenter = new Vector3(0, 0, ((cylinderPrefab.transform.localScale.y + cylinderOffset.y) * cylinderAmount.y) / 4);
        GameObject convoyer = Instantiate(convoyerColliderPrefab, convoyerCenter, Quaternion.identity);
        convoyer.GetComponent<BoxCollider>().size = new Vector3(mapSize + 2 * cylinderPrefab.transform.localScale.y, cylinderPrefab.transform.localScale.y, ((cylinderPrefab.transform.localScale.y + cylinderOffset.y) * cylinderAmount.y) / 2);
        
        // Generate all the cylinder lines
        for (int j = 0; j < cylinderAmount.y; j++)
        {
            currentPosition.x -= mapSize / 2;
            
            // Generate a line of cylinder
            for (int i = 0; i <= cylinderAmount.x; i++)
            {
                Instantiate(cylinderPrefab, currentPosition, cylinderPrefab.transform.rotation).transform.SetParent(convoyer.transform);
                currentPosition.x += cylinderOffset.x;
            }
            
            currentPosition.x = 0;
            currentPosition.z += cylinderOffset.y;
        }

        // Apply upper platforme offset (x = z, y = y)
        currentPosition.z += upperPlaformeOffset.x + (upperPlatformePrefab.transform.localScale.z / 2.0f);
        currentPosition.y += upperPlaformeOffset.y;
        
        // Spawn the upper platforme
        GameObject upperPlatforme = Instantiate(upperPlatformePrefab, currentPosition, Quaternion.identity);
        Vector3 upperPlatformeScaleMultiplier = new Vector3(mapSize, 1, 1);
        upperPlatforme.transform.localScale = Vector3.Scale(upperPlatforme.transform.localScale, upperPlatformeScaleMultiplier);
        
        // Set props manager values
        propsManager.spawnXMin = -mapSize / 2;
        propsManager.spawnXMax = mapSize / 2;
        propsManager.spawnPosition = new Vector3(0, currentPosition.y + upperPlatformePrefab.transform.localScale.y, currentPosition.z);
    }
}
