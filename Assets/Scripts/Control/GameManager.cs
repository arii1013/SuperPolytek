using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public float leftTime;

    // This
    private AudioSource gameManagerAudio;
    private float currentTime;

    // PlayerContoller
    public bool isDead;

    // StatusBar
    public Text timeText, scoreText;
    public int score;

    // PauseMenu
    public AudioClip pauseClip;
    public GameObject PauseMenu_Canvas;
    public bool isPause;
    
    // Flag
    public bool isFlag;
    public bool isFlagRun;
    public bool isFlagEnd;

    // REF
    public BGMManager bgmManager;
    public Rigidbody2D playerRigidbody;
    public Transform playerTransform;
    public PlayerController playerController;

    void Start()
    {    
        // This
        currentTime = 0f;
            
        // StatusBar
        score = 0;
        isDead = false;

        // PauseMenu
        isPause = false;

        // Flag
        isFlag = false;
        isFlagRun = false;
        isFlagEnd = false;
        
        // REF
        gameManagerAudio = GetComponent<AudioSource>();
        bgmManager = FindObjectOfType<BGMManager>();
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerTransform = player.GetComponent<Transform>();
        playerController = player.GetComponent<PlayerController>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            continueGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            pauseGame();
        }
        // Pause, Restart 담당, 언제나 ON
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        if (leftTime > 0 & !isDead)
        {
            leftTime -= Time.deltaTime;
        }
        else if (leftTime <= 0 && !isDead && !isFlag && !isFlagRun)
        {
            bgmManager.bgmManagerAudio.Stop();
            PlayerDie();
        }
        timeText.text = "TIME\n" + (int) leftTime;
        scoreText.text = "SCORE\n" + (int) score;
        // 남은 시간과 점수를 출력해줌.
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////


        if (isDead && !isFlagEnd)
        {
            currentTime += Time.deltaTime;

            if (currentTime > 5f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    
    public void leftTimetoScore()
    {
        score += (int) leftTime;
        leftTime = 0f;
    }
    
    public void pauseGame()
    {
        isPause = true;
        
        bgmManager.bgmManagerAudio.Pause();
        
        Time.timeScale = 0f;

        gameManagerAudio.clip = pauseClip;
        gameManagerAudio.Play();
        
        PauseMenu_Canvas.SetActive(true);
    }

    public void continueGame()
    {
        isPause = false;
        
        PauseMenu_Canvas.SetActive(false);
        
        Time.timeScale = 1f;
        
        bgmManager.bgmManagerAudio.Play();
    }

    public void FlagDown(float xPosition)
    {
        isFlag = true;
        
        bgmManager.bgmManagerAudio.Stop();
        
        playerRigidbody.gravityScale = 0f;
        playerRigidbody.velocity = Vector2.zero;
        playerRigidbody.gravityScale = 0.01f;
        playerTransform.position = new Vector2(xPosition, playerTransform.position.y);
            
        Vector3 scale = playerTransform.localScale;
        scale.x = -Mathf.Abs(scale.x);
        playerTransform.localScale = scale;

        playerRigidbody.velocity = Vector2.down;
    }
    
    public void FlagRun()
    {
        player.transform.Translate(Vector2.right * 0.5f * Time.deltaTime);
    }

    public void PlayerDie()
    {
        isDead = true;

        playerRigidbody.velocity = Vector2.zero;
        playerController.playerAnimator.SetTrigger("Die");
        playerController.playerAudio.clip = playerController.deathClip;
        playerController.playerAudio.Play();
        Destroy(player, 5f);
    }

    public void MoveNextTutorial(float xPosition)
    {
        playerTransform.position = new Vector2(xPosition, playerTransform.position.y);
    }
}
