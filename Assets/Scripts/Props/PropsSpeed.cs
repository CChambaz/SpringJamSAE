using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsSpeed : MonoBehaviour
{
    public float zSpeed = 3;
    [SerializeField] private Rigidbody propRigidbody;
    void Update()
    {
        propRigidbody.velocity = new Vector3(propRigidbody.velocity.x, propRigidbody.velocity.y, -zSpeed);
    }
}
