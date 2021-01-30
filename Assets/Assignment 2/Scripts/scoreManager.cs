using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScore
{
    public string username;
    public float time;

}



public class scoreManager : MonoBehaviour
{
    GameManager gameManager;
    public string[] usernames;
    public float[]  times;
    playerScore playerScore;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
