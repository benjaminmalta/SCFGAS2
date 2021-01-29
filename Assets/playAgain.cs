using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAgain : MonoBehaviour
{
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void restartGame() {
        gameManager.playAgain();
    }

    public void startGame()
    {
        gameManager.startGame();
    }
}
