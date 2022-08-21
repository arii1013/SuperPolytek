using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C_TypeMonster : MonoBehaviour
{
    private GameManager gameManager;
    
    // This
    private AudioSource TMonAudio;
    private Animator TMonAnimator;
    private bool isChasing;
    private Vector3 chase;
    private float distance;
    
    // REF
    private Rigidbody2D playerRigidbody;
    private PlayerController playerController;
    private BGMManager bgmManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        // This
        TMonAudio = GetComponent<AudioSource>();
        TMonAnimator = GetComponent<Animator>();
        isChasing = false;
        
        // REF
        playerRigidbody = gameManager.player.GetComponent<Rigidbody2D>();
        playerController = gameManager.player.GetComponent<PlayerController>();
        bgmManager = FindObjectOfType<BGMManager>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(gameManager.player.transform.position, transform.position);

        if (isChasing)
        {
            transform.Translate(chase * Time.deltaTime);
            return;
        }
        
        if (distance < 3f)
        {
            if (gameManager.player.transform.position.x < transform.position.x)
            {
                chase = Vector3.left * 0.7f;
                isChasing = true;
            }
            
            else if (gameManager.player.transform.position.x > transform.position.x)
            {
                chase = Vector3.right * 0.7f;
                isChasing = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (gameManager.player.transform.position.y > transform.position.y)
            {
                TMonAudio.Play();
                TMonAnimator.SetTrigger("Die");
                Destroy(gameObject, 0.1f);
            
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
                Destroy(gameManager.player, 5f);
                
                TMonAnimator.SetTrigger("Die");
                Destroy(gameObject, 0.2f);

                bgmManager.bgmManagerAudio.Stop();
            }
        }
        
        else if (other.tag == "Dead")
        {
            Destroy(gameObject);
        }
    }
}
