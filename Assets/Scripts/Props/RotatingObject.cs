using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField] public Vector3 rotationSpeed;
    [SerializeField] public bool hasToBeRegistered = true;
    [SerializeField] public bool isRotating = true;

    private Transform transform;
    
    // Start is called before the first frame update
    void Start()
    {
        if(hasToBeRegistered)
            GameManager.gameManagerInstance.RegisterRotatingObject(this);
        
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isRotating)
            transform.Rotate(rotationSpeed);
    }

    private void OnDestroy()
    {
        if(hasToBeRegistered)
            GameManager.gameManagerInstance.UnregisterRotatingObject(this);
    }
}
