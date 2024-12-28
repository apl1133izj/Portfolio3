using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        Button gameClearButton = GetComponent<Button>();
        gameClearButton.onClick.AddListener(gameManager.ReStart);
        gameManager.gameOver = false;
    }
}
