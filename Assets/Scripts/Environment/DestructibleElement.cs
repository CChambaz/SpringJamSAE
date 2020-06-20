using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleElement : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(other.gameObject.GetComponent<PlayerController>().playerState == PlayerController.PlayerState.HURRICANE)
                Destroy(gameObject);
        }
    }
}
