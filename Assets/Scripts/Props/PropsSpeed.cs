using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsSpeed : MonoBehaviour
{
    public float zSpeed = 3;
    [SerializeField] private Rigidbody propRigidbody;
    [SerializeField] private float deleteY = 10f;

    private void Start()
    {
        propRigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        propRigidbody.velocity = new Vector3(propRigidbody.velocity.x, propRigidbody.velocity.y, -zSpeed);

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
