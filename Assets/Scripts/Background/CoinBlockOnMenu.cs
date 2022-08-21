using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBlockOnMenu : MonoBehaviour
{
    public AudioClip brickClip;

    private int coins;
    private AudioSource itemAudio;
    private Animator itemAnimator;

    void Start()
    {
        itemAudio = GetComponent<AudioSource>();
        itemAnimator = GetComponent<Animator>();

        coins = 3;
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

                if (coins == 0)
                {
                    itemAudio.clip = brickClip;

                    itemAnimator.SetTrigger("Last");
                    itemAnimator.SetTrigger("Bricklize");
                }
            }
        }
    }
}