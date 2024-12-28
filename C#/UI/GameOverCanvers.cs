using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverCanvers : MonoBehaviour
{
    GameManager gameManager;
    public GameObject gameOverUIGameObject;

    void Update()
    {
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager.gameOver)
        {
            gameOverUIGameObject.SetActive(true);
        }
    }
}
