using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private GameObject detectionZone;
    [SerializeField] private int life;
    private Animator anim;
    
    private GameManager gameManager;

    void Start()
    {
        anim = GetComponent<Animator>();
        gameManager = GameManager.gameManagerInstance;
        gameManager.generators++;
        anim.Play("Idle");
    }

    public void PlayerHasInterracted()
    {
        life--;
        if (life == 1)
        {
            anim.Play("Warning");
        }

        if (life <= 0)
        {
            anim.Play("Destroy");
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
