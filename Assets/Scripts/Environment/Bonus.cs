using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bonus : MonoBehaviour
{
    public enum BonusType
    {
        OBJECTIVE,
        HURRICAN_BEER
    }

    [SerializeField] public BonusType bonusType;
    [SerializeField] private GameObject[] objectiveModels;
    [SerializeField] private Vector3[] objectiveModelRotations;

    private void Start()
    {
        if (bonusType == BonusType.HURRICAN_BEER)
            return;
        
        RotatingObject rotatingObject = GetComponentInChildren<RotatingObject>();

        int id = Random.Range(0, objectiveModels.Length);
        Instantiate(objectiveModels[id], Vector3.zero, Quaternion.Euler(objectiveModelRotations[id]),
            rotatingObject.transform).transform.localPosition = Vector3.zero;
    }
}
