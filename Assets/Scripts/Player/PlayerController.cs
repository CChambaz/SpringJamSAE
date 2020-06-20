using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isDead = false;
    
    private Rigidbody rigid;
    private float currentSpeed = 0.0f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            animator.SetBool("dead", isDead);
        else
        {
            Move();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            isDead = true;
            SoundManager.soundManagerInstance.PlaySound(SoundManager.SoundList.DEATH, SoundManager.AudioMixerGroup.PLAYER);
        }
    }
}
