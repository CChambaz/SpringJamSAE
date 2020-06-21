using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBag : MonoBehaviour
{
    private bool alreadyPlayedSound = false;
    
    private void OnCollisionEnter(Collision other)
    {
        if (!alreadyPlayedSound && other.gameObject.tag == "Convoyer")
        {
            SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.TRASH_IMPACT, SoundManager.AudioMixerGroup.ENVIRONMENT);
            alreadyPlayedSound = true;
        }
        else if (other.gameObject.tag == "Player")
        {
            SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.TRASH_IMPACT, SoundManager.AudioMixerGroup.ENVIRONMENT);
            
            if(other.transform.GetComponent<PlayerController>().playerState == PlayerController.PlayerState.HURRICANE)
                Destroy(gameObject);
        }
    }
}
