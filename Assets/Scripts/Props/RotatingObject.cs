using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObject : MonoBehaviour
{
    [SerializeField] public Vector3 rotationSpeed;

    private Transform transform;
    
    // Start is called before the first frame update
    void Start()
    {
        GameManager.gameManagerInstance.RegisterRotatingObject(this);
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationSpeed);
    }
}
