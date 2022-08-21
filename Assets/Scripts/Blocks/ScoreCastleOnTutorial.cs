using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreCastleOnTutorial : MonoBehaviour
{
    private GameManager gameManager;
    
    public float nextXPosition;
    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && gameManager.score >= 10)
        {
            gameManager.MoveNextTutorial(nextXPosition);
        }
    }
}
