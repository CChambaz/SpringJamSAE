using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        NONE,
        HURRICANE
    }

    [SerializeField] private float propsZSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isDead = false;
    
    private Rigidbody rigid;
    private float currentSpeed = 0.0f;

    private int score = 0;
    private int hurricanUsage = 0;
    public PlayerState playerState = PlayerState.NONE;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            animator.SetBool("dead", isDead);
        else
        {
            if (hurricanUsage > 0 && Input.GetButton("Fire2"))
            {
                playerState = PlayerState.HURRICANE;
                animator.SetTrigger("hurricane");
                hurricanUsage--;
            }
            
            Move();

            //Set the velocity of the
            rigid.velocity = new Vector3(rigid.velocity.x, rigid.velocity.y, rigid.velocity.z - propsZSpeed);
        }

    }

    void Move()
    {
        Vector2 movement = Vector2.zero;

        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        
        movement *= playerSpeed;
        rigid.velocity = new Vector3(movement.x, rigid.velocity.y, movement.y);
        animator.SetFloat("speed", movement.y);
    }

    private void EndHurricane()
    {
        playerState = PlayerState.NONE;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            isDead = true;
            SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.DEATH, SoundManager.AudioMixerGroup.PLAYER);
        }

    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Generator")
        {
            if (score > 0)
            {
                if (Input.GetButtonDown("GeneratorInterraction"))
                {
                    other.gameObject.GetComponentInParent<Generator>().PlayerHasInterracted();
                    score -= 1;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Item")
        {
            Bonus bonus = other.gameObject.GetComponent<Bonus>();

            switch (bonus.bonusType)
            {
                case Bonus.BonusType.OBJECTIVE:
                    score++;
                    break;
                case Bonus.BonusType.HURRICAN_BEER:
                    hurricanUsage++;
                    break;
            }
            
            Destroy(bonus.gameObject);
        }

    }
}
