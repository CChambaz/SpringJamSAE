using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    public enum BonusType
    {
        OBJECTIVE,
        HURRICAN_BEER
    }

    [SerializeField] public BonusType bonusType;
}
