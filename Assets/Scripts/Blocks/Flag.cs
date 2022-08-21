using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private GameManager gameManager;
    
    // This
    private AudioSource endFlagAudio;
    public float xPosition;

    void Start()
    {
        endFlagAudio = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameManager.FlagDown(xPosition);
            endFlagAudio.Play();
        }
    }
}
