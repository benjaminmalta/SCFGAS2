using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerManager : MonoBehaviour
{

    public bool timerStarted;
    public bool timerPaused = false;

    float timerValue=0f;

    Text timerText;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        timerValue = gameManager.time;
        //the text component attached to THIS object
        timerText = GetComponent<Text>();
        StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        while(!timerPaused)
        { 
            if (timerStarted)
            {
                //measure the time
                timerValue++;

                float minutes = timerValue / 60f;
                float seconds = timerValue % 60f;

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                gameManager.time = timerValue;

                //code that is running every second
                yield return new WaitForSeconds(1f);
            }
            else
            {
                //don't measure the time
                timerValue = 0f;
                timerText.text = string.Format("{0:00}:{1:00}", 0f, 0f);
                yield return null;

            }
            
        }

        if (timerPaused) 
        {
            float minutes = timerValue / 60f;
            float seconds = timerValue % 60f;
            timerText.color = Color.yellow;
            
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        }

    }

    

    
    

    
}
