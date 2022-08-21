using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager;
    private AudioSource coinAudio;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        coinAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameManager.score++;

            coinAudio.Play();

            Destroy(gameObject, 0.15f);
        }
    }
    
    
}
