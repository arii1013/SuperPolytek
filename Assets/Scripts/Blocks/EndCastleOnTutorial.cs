using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCastleOnTutorial : MonoBehaviour
{
    private GameManager gameManager;
    
    public float nextXPosition;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameManager.MoveNextTutorial(nextXPosition);
        }
    }
}
