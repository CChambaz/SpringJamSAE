using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalBeam : MonoBehaviour
{
    private bool alreadyPlayedSound = false;
    
    private void OnCollisionEnter(Collision other)
    {
        if (!alreadyPlayedSound && other.gameObject.tag == "Convoyer")
        {
            SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.STEEL_IMPACT, SoundManager.AudioMixerGroup.ENVIRONMENT);
            alreadyPlayedSound = true;
        }
    }
}
