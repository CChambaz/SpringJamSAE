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

    public float propsZSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject hurricaneCollider;
    [SerializeField] private RotatingObject winRotatingCamera;
    [SerializeField] public Transform cameraWinPosition;
    [SerializeField] public bool isDead = false;
    [SerializeField] public bool isWin = false;
    
    private Rigidbody rigid;
    private float currentSpeed = 0.0f;
    private bool hasBeenMovedToWinPosition = false;
    
    public int score = 0;
    public int hurricanUsage = 0;
    public PlayerState playerState = PlayerState.NONE;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        GameManager.gameManagerInstance.player = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWin)
        {
            rigid.velocity = Vector3.zero;
            return;
        }

        if (isDead)
            animator.SetBool("dead", isDead);
        else
        {
            if (playerState != PlayerState.HURRICANE && hurricanUsage > 0 && Input.GetButtonDown("Fire2"))
            {
                hurricaneCollider.SetActive(true);
                playerState = PlayerState.HURRICANE;
                animator.SetTrigger("hurricane");
                SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.POWER_UP, SoundManager.AudioMixerGroup.PLAYER);
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
        hurricaneCollider.SetActive(false);
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
            if (score > 0 && other.GetComponent<Generator>().IsActive())
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    other.gameObject.GetComponentInParent<Generator>().PlayerHasInterracted();
                    score -= 1;
                }
            }
        }
    }

    public void StartDancing()
    {
        isWin = true;
        rigid.velocity = Vector3.zero;
        animator.SetBool("isWinning", true);

        if (!hasBeenMovedToWinPosition)
        {
            transform.position = GameManager.gameManagerInstance.winPosition;
            winRotatingCamera.isRotating = true;
            hasBeenMovedToWinPosition = true;
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
            
            SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.PICK_UP, SoundManager.AudioMixerGroup.PLAYER);
            Destroy(bonus.gameObject);
        }

    }
}
