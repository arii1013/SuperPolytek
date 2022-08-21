using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlock : MonoBehaviour
{
    private GameManager gameManager;
    
    // This
    public int minCoins, maxCoins;
    public AudioClip brick;
    private AudioSource itemAudio;
    private int coins;
    private Animator itemAnimator;

    void Start()
    {
        // This
        coins = Random.Range(minCoins, maxCoins);
        
        // REF
        itemAudio = GetComponent<AudioSource>();
        itemAnimator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y == 1f)
        {
            coins--;
            itemAudio.Play();

            if (coins >= 0)
            {
                itemAnimator.SetTrigger("Heading");
                itemAnimator.SetTrigger("Normalize");
                gameManager.score++;

                if (coins == 0)
                {
                    itemAudio.clip = brick;

                    itemAnimator.SetTrigger("Last");
                    itemAnimator.SetTrigger("Bricklize");
                }
            }
        }
    }
}
