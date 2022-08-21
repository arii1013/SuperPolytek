using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_TypeMonster : MonoBehaviour
{
    private GameManager gameManager;
    
    // This
    private AudioSource PMonAudio;
    private Animator PMonAnimator;
    private float basePosition;
    public float Range;
    private bool Left, Right;

    // REF
    public GameObject player;
    private Rigidbody2D playerRigidbody;
    private PlayerController playerController;
    private BGMManager bgmManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        // This
        PMonAudio = GetComponent<AudioSource>();
        PMonAnimator = GetComponent<Animator>();
        basePosition = transform.position.x;
        Left = true;
        Right = false;
        
        // REF
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerController = player.GetComponent<PlayerController>();
        bgmManager = FindObjectOfType<BGMManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Left)
        {
            MovetoLeft();
        }
        
        else if (Right)
        {
            MovetoRight();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (player.transform.position.y > transform.position.y)
            {
                PMonAudio.Play();
                PMonAnimator.SetTrigger("Die");
                Destroy(gameObject, 0.2f);
            
                playerRigidbody.AddForce(Vector2.up * 6f, ForceMode2D.Impulse);
                playerController.jumpCount++;
                gameManager.score += 2;
            }

            else
            {
                gameManager.isDead = true;

                playerRigidbody.velocity = Vector2.zero;
                playerController.playerAnimator.SetTrigger("Die");
                playerController.playerAudio.clip = playerController.deathClip;
                playerController.playerAudio.Play();
                Destroy(player, 5f);
                
                PMonAnimator.SetTrigger("Die");
                Destroy(gameObject, 0.2f);
                
                bgmManager.bgmManagerAudio.Stop();
            }
        }
        
        else if (other.tag == "Dead")
        {
            Destroy(gameObject);
        }
    }

    private void MovetoLeft()
    {
        if (transform.position.x > basePosition - Range)
        {
            transform.Translate(Vector3.left * 0.6f * Time.deltaTime);
        }

        else
        {
            Left = false;
            Right = true;
        }
    }

    private void MovetoRight()
    {
        if (transform.position.x < basePosition + Range)
        {
            transform.Translate(Vector3.right * 0.7f * Time.deltaTime);
        }

        else
        {
            Left = true;
            Right = false;
        }
    }
}

