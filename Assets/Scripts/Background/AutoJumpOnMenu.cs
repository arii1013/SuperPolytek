using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoJumpOnMenu : MonoBehaviour
{
    // This
    private float presetTime;
    private float currentTime;
    private float jumpPower;
    private bool isRunning;
    private bool isJumping;
    private Rigidbody2D autoJumpRigidbody;
    private AudioSource autoJumpAudio;
    private Animator autoJumpAnimator;

    void Start()
    {
        // This
        presetTime = Random.Range(1.5f, 7f);
        jumpPower = Random.Range(2f, 5f);
        autoJumpRigidbody = GetComponent<Rigidbody2D>();
        autoJumpAudio = GetComponent<AudioSource>();
        autoJumpAnimator = GetComponent<Animator>();
    }
    
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > presetTime)
        {
            currentTime = 0f;

            autoJumpAudio.Play();
            
            autoJumpRigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            
            presetTime = Random.Range(1.5f, 4f);
            jumpPower = Random.Range(1.5f, 4f);
        }
        
        autoJumpAnimator.SetBool("Run", isRunning);
        autoJumpAnimator.SetBool("Jump", isJumping);
    }
    
    // 가만히 서 있을 때 모션
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f && autoJumpRigidbody.velocity == Vector2.zero)
        {
            isRunning = false;
            isJumping = false;
        }
    }

    // 점프할 때 모션
    void OnCollisionExit2D(Collision2D collision)
    {
        isJumping = true;
        isRunning = false;
    }
}
