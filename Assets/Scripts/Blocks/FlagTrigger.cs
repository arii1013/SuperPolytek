using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagTrigger : MonoBehaviour
{
    private GameManager gameManager;
    
    // REF
    private Transform playerTransform;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        // REF
        playerTransform = gameManager.player.transform;
    }

    void Update()
    {
        if (gameManager.isFlagRun)
        {
            gameManager.FlagRun();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && gameManager.isFlag)
        {
            gameManager.isFlagRun = true;
            gameManager.isFlag = false;

            gameManager.playerRigidbody.gravityScale = 1f;
            
            Vector3 scale = playerTransform.localScale;
            scale.x = Mathf.Abs(scale.x);
            playerTransform.localScale = scale;
            
            gameManager.leftTimetoScore();
        }
    }
}
