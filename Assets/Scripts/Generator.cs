using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject detectionZone;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameManager.generators++;
    }

    public void PlayerHasInterracted()
    {
        gameManager.generators--;
        Destroy(detectionZone);
    }
}
