using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject detectionZone;
    [SerializeField] private int life;
    
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.gameManagerInstance;
        gameManager.generators++;
    }

    public void PlayerHasInterracted()
    {
        life--;

        if (life <= 0)
        {
            SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.GENE_DESTROY, SoundManager.AudioMixerGroup.ENVIRONMENT);
            gameManager.generators--;
            Destroy(detectionZone);
        }
        else
        {
            SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.GENE_DAMAGE, SoundManager.AudioMixerGroup.ENVIRONMENT);
        }
    }
}
