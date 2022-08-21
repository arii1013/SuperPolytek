using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadzoneOnTutorial : MonoBehaviour
{
    // GameManager
    private GameManager gameManager;
    
    // This
    public float xRespawn, yRespawn;

    // REF
    private Transform playerTransform;
    
    void Start()
    {
        // GameManager
        gameManager = FindObjectOfType<GameManager>();
        
        // REF
        playerTransform = gameManager.player.transform;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerTransform.position = new Vector2(xRespawn, yRespawn);
        }
    }
}
