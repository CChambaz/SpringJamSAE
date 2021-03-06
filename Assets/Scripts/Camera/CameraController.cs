﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Quaternion rotationOffset;
    private Transform target;
    private PlayerController player;
    [SerializeField] private float widthLimit = 8.0f; 

    [Header("Shaking effect")]
    [SerializeField] private bool isReady = true;
    private float positionYShake;
    private float positionYBord = 0.0f;
    private float positionZBord = 0.0f;
    [SerializeField] private float velocityShake;
    [SerializeField] private float distanceShake;

    [SerializeField] private float winMoveSpeed;
    [SerializeField] private float winRotationSpeed;
    
    //params Effect
    private const float Z_ROT = 0.3f;
    private const float MAX_Y = 0.8f;
    private const float MAX_Z = 2;
    private const float LIMIT_Z = 5;

    private void ShakingEffect(float power = 1.0f)
    {
        positionYShake = power * (distanceShake * Mathf.Sin(Time.time * velocityShake));
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            if (GameObject.Find("Ch17_nonPBR") != null)
            {
                target = GameObject.Find("Ch17_nonPBR").transform;
                player = target.gameObject.GetComponent<PlayerController>();
                transform.position = target.position + positionOffset;
                transform.rotation = rotationOffset;

            }
        }
        else
        {
            if (player.isWin)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.cameraWinPosition.position, winMoveSpeed);
                transform.rotation = Quaternion.Slerp(transform.rotation, player.cameraWinPosition.rotation, winRotationSpeed);
            }
            else if (!player.isDead)
            {
                float positionX = target.position.x;
                if (target.position.x > widthLimit)
                {
                    positionX = widthLimit;
                }

                if (target.position.x < -widthLimit)
                {
                    positionX = -widthLimit;
                }

                Vector3 newPosition = new Vector3(positionX, target.position.y);
                float power = 1.0f;

                if (target.position.z <= LIMIT_Z)
                {
                    power = 4.5f;
                    if (transform.rotation.x <= Z_ROT)
                    {
                        transform.Rotate(Vector3.right, 2.0f);
                    }
                        
                    if (positionYBord > MAX_Y)
                    {
                        positionYBord += -1 * Time.deltaTime * 2.0f;
                    }

                    newPosition += new Vector3(0, positionYBord + positionOffset.y, -LIMIT_Z);
                    positionZBord = 0.0f;
                }
                else
                {
                    float velocity_z = target.gameObject.GetComponent<Rigidbody>().velocity.z;
                    if (positionZBord <= LIMIT_Z && velocity_z > 0)
                    {
                        if(positionZBord < MAX_Z)
                            positionZBord += Time.deltaTime * 2.0f;
                    }
                    else
                    {
                        if (positionZBord > 0.0f)
                        {
                            positionZBord -= Time.deltaTime * 2.0f;
                        }
                    }

                    if (transform.rotation.x > 0.045f)
                    {
                        transform.Rotate(Vector3.left, 0.5f);
                    }

                    newPosition += new Vector3(0, positionYBord, target.position.z + positionZBord) + positionOffset;
                }


                if (isReady)
                {
                    ShakingEffect(power);
                    newPosition += new Vector3(0, positionYShake, 0);
                }

                transform.position = newPosition;
            }
            else
            {
                transform.LookAt(target);
            }
        }
    }
}
