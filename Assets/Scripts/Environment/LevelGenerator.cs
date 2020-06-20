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
    [SerializeField] private GameObject deadZonePrefab;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject particlePrefab;

    private Vector3 currentPosition = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
        
        // Play sounds
        SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.FIRE, SoundManager.AudioMixerGroup.CONSTANT_NOISE, 0.0f, true);
        SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.CONVEYORBELT, SoundManager.AudioMixerGroup.CONSTANT_NOISE, 0.0f, true);
    }

    // Update is called once per frame
    void GenerateMap()
    {
        // Spawn the dead zone
        GameObject deadZone = Instantiate(deadZonePrefab, new Vector3(0, 5, 0), Quaternion.identity);
        deadZone.transform.localScale = new Vector3(mapSize + 2 * cylinderPrefab.transform.localScale.y, 10, 0.25f);
        
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
        
        // Player spawn
        convoyerCenter.y += 1;
        Instantiate(playerPrefab, convoyerCenter, Quaternion.identity);
        //Todo Link player to camera

        // Spawn the particles
        /*convoyerCenter.y -= 10;
        ParticleSystem particle = Instantiate(particlePrefab, convoyerCenter, particlePrefab.transform.rotation)
            .GetComponent<ParticleSystem>();

        var shape = particle.shape;
        shape.scale = new Vector3(mapSize + 2 * cylinderPrefab.transform.localScale.y, ((cylinderPrefab.transform.localScale.y + cylinderOffset.y) * cylinderAmount.y), cylinderPrefab.transform.localScale.y);*/
    }
}
