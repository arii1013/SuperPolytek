using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueOnPauseMenu : MonoBehaviour
{
    private GameManager gameManager;
    
    public void ContinueToGame()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        gameManager.continueGame();
    }
}
