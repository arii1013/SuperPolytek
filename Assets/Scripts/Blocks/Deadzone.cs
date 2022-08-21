using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
    private GameManager gameManager;
    
    // REF
    private PlayerController playerControlller;
    private Rigidbody2D playerRigidbody;
    private BGMManager bgmManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        // REF
        playerControlller = gameManager.player.GetComponent<PlayerController>();
        playerRigidbody = gameManager.player.GetComponent<Rigidbody2D>();
        bgmManager = FindObjectOfType<BGMManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameManager.isDead = true;

            playerRigidbody.velocity = Vector2.zero;
            playerControlller.playerAnimator.SetTrigger("Die");
            playerControlller.playerAudio.clip = playerControlller.deathClip;
            playerControlller.playerAudio.Play();
            Destroy(gameManager.player, 5f);
            
            bgmManager.bgmManagerAudio.Stop();
        }
    }
}
