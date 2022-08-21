using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCastle : MonoBehaviour
{
    private GameManager gameManager;

    // This
    private float currentTime;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.isFlagEnd)
        {
            currentTime += Time.deltaTime;

            if (currentTime > 5f)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && gameManager.isFlagRun)
        {
            gameManager.isFlagRun = false;
            gameManager.isFlagEnd = true;
            
            gameManager.player.SetActive(false);
        }
    }
}
