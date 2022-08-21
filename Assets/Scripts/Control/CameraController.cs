using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameManager gameManager;

    private GameObject player;
    public float yOffset;

    private Vector3 cameraPosition;

    void Start()
    {
        cameraPosition = new Vector3(0f, yOffset,-2.3f);
        gameManager = FindObjectOfType<GameManager>();
        player = gameManager.player;

    }

    void Update()
    {
        cameraPosition.x = player.transform.position.x;

        transform.position = cameraPosition;
    }
}
