using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBox : MonoBehaviour
{
    private bool alreadyPlayedSound = false;
    
    private void OnCollisionEnter(Collision other)
    {
        if (!alreadyPlayedSound && other.gameObject.tag == "Convoyer")
        {
            SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.WOOD_IMPACT, SoundManager.AudioMixerGroup.ENVIRONMENT);
            alreadyPlayedSound = true;
        }
    }
}
