using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject detectionZone;
    public void PlayerHasInterracted()
    {
        Destroy(detectionZone);
        Debug.Log("dead");
    }
}
