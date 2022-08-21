 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeP_TypeMonster : MonoBehaviour
{
    private GameManager gameManager;
    
    // This
    public GameObject SafePrefab;
    private GameObject SafePMon;
    private AudioSource PMonAudio;
    private Animator PMonAnimator;
    private Vector3 basePosition;
    public float Range;
    private float currentTime, presetTime;
    private bool Left, Right, isSafePDead;

    // REF
    private Rigidbody2D playerRigidbody;
    private PlayerController playerController;
    private BGMManager bgmManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        
        // This
        PMonAudio = GetComponent<AudioSource>();
        PMonAnimator = GetComponent<Animator>();
        basePosition = transform.position;
        presetTime = 0.2f;
        currentTime = 0f;
        Left = true;
        Right = false;
        isSafePDead = false;
        
        // REF
        playerRigidbody = gameManager.player.GetComponent<Rigidbody2D>();
        playerController = gameManager.player.GetComponent<PlayerController>();
        bgmManager = FindObjectOfType<BGMManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSafePDead)
        {
            currentTime += Time.deltaTime;

            if (currentTime > presetTime)
            {
                SafePMon = Instantiate(SafePrefab);
                SafePMon.transform.position = basePosition;
                Destroy(gameObject);
            }
        }
        
        if (Left)
        {
            MovetoLeft();
        }
        
        else if (Right)
        {
            MovetoRight();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (gameManager.player.transform.position.y > transform.position.y)
            {
                PMonAudio.Play();
                PMonAnimator.SetTrigger("Die");

                isSafePDead = true;
            
                playerRigidbody.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
                playerController.jumpCount++;
                gameManager.score += 2;
            }
        }
    }

    private void MovetoLeft()
    {
        if (transform.position.x > basePosition.x - Range)
        {
            transform.Translate(Vector3.left * 0.6f * Time.deltaTime);
        }

        else
        {
            Left = false;
            Right = true;
        }
    }

    private void MovetoRight()
    {
        if (transform.position.x < basePosition.x + Range)
        {
            transform.Translate(Vector3.right * 0.7f * Time.deltaTime);
        }

        else
        {
            Left = true;
            Right = false;
        }
    }
}

