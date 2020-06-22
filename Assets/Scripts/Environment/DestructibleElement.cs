using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleElement : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Hurrican")
        {
            SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.WOOD_DESTROY, SoundManager.AudioMixerGroup.ENVIRONMENT);
            Destroy(gameObject);
        }
    }
}
