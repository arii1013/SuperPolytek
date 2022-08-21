using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.XR;

public class PlayerController : MonoBehaviour
{
    // This
    public AudioClip deathClip;
    public float xMForbidden;
    public float xPForbidden = 100f;
    private Rigidbody2D playerRigidbody;
    public Animator playerAnimator;
    public AudioSource playerAudio;
    public int jumpCount;
    private bool isJumping, isRunning;

    // REF
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();

        jumpCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // PauseMenu가 활성화되었다면 이 아래의 매서드 실행을 무시함.
        if (gameManager.isPause)
        {
            return;
        }

        else if (gameManager.isDead || gameManager.isFlag || gameManager.isFlagRun)
        {
            return;
        }

        // 오른쪽으로 달릴 때
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < xPForbidden)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x);
            transform.localScale = scale;
            transform.Translate(Vector3.right * 1.5f * Time.deltaTime);

            isRunning = true;
        }
        // 왼쪽으로 달릴 때
        else if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > xMForbidden)
        {
            Vector3 scale = transform.localScale;
            scale.x = -Mathf.Abs(scale.x);
            transform.localScale = scale;
            transform.Translate(Vector3.left * 1.5f * Time.deltaTime);

            isRunning = true;
        }
        // 가만히 있을 때
        else
        {
            {
                isRunning = false;
            }
        }
        // 플레이어의 무빙과 애니메이션을 제어
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && jumpCount < 2)
        {
            jumpCount++;
            
            playerRigidbody.AddForce(Vector2.up * 4f, ForceMode2D.Impulse);

            playerAudio.Play();
        }
        else if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow)) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity *= 0.55f;
        }
        // 점프와 2단 점프 구현
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        playerAnimator.SetBool("Run", isRunning);
        playerAnimator.SetBool("Jump", isJumping);
    }
    
    // 땅을 밟으면
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            jumpCount = 0;
        }
    }

    // 가만히 서 있을 때 모션
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f && playerRigidbody.velocity == Vector2.zero)
        {
            isRunning = false;
            isJumping = false;
        }
    }

    // 점프할 때 모션
    void OnCollisionExit2D(Collision2D collision)
    {
        isJumping = true;
        isRunning = false;
    }
}
