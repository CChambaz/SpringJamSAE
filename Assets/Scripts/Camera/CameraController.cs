using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Quaternion rotationOffset;
    private Transform target;

    [Header("Shaking effect")]
    [SerializeField] private bool isReady = true;
    private float positionYShake;
    private float positionYBord = 0.0f;
    private float positionZBord = 0.0f;
    [SerializeField] private float velocityShake;
    [SerializeField] private float distanceShake;

    //params Effect
    private const float Z_ROT_1 = 0.1f;
    private const float MAX_Y_1 = 0.8f;

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
            if (GameObject.Find("Player(Clone)") != null)
            {
                target = GameObject.Find("Player(Clone)").transform;
                transform.position = target.position + positionOffset;
                transform.rotation = rotationOffset;

            }
        }
        else
        {
            if (!target.gameObject.GetComponent<PlayerController>().isDead)
            {
                Vector3 newPosition = new Vector3(target.position.x, target.position.y);
                float power = 1.0f;

                if (target.position.z <= 6)
                {
                    power = 4.5f;
                    if (transform.rotation.x <= Z_ROT_1)
                    {
                        transform.Rotate(Vector3.right, 1.5f);
                    }

                    if (positionYBord > MAX_Y_1)
                    {
                        positionYBord += -1 * Time.deltaTime * 2.5f;
                    }

                    newPosition += new Vector3(0, positionYBord + positionOffset.y, -4);
                    positionZBord = 0.0f;
                }
                else
                {
                    float velocity_z = target.gameObject.GetComponent<Rigidbody>().velocity.z;
                    if (positionZBord <= 6 && velocity_z > 0)
                    {
                        positionZBord += Time.deltaTime * 2.0f;
                    }
                    else
                    {
                        if (positionZBord > 0.0f)
                            positionZBord -= Time.deltaTime * 2.0f;
                    }

                    if (transform.rotation.x > 0.0f)
                    {
                        transform.Rotate(Vector3.left, 0.2f);
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
