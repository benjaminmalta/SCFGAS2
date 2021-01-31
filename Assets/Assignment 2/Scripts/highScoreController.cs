using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighScore
{
    public string playername;
    public float playtime;

   
}


public class highScoreController : MonoBehaviour
{
    // Start is called before the first frame update
    HighScore myHighScores;
    GameManager gameManager;
    public string[] names;
    public float[] playtimes;

    List<HighScore> highScoreList;

    public TMP_Text contentText;

    void Start()
    {
        SaveList();
        
    }

    void SaveList() 
    {
        highScoreList = new List<HighScore> { };
        gameManager = FindObjectOfType<GameManager>();
        names = PlayerPrefsX.GetStringArray("PlayerNames", "",1);
        playtimes = PlayerPrefsX.GetFloatArray("PlayerTimes",0f,1);


        if (names[0] == "")
        {
            names[0] = gameManager.username;
            playtimes[0] = gameManager.time;


            PlayerPrefsX.SetStringArray("PlayerNames", names);
            PlayerPrefsX.SetFloatArray("PlayerTimes", playtimes);
        }
        else
        {
            myHighScores = new HighScore();
            for (int i = 0; i < names.Length; i++)
            {
                //Loading myHighScores with current saved playerPrefs
                myHighScores = new HighScore();
                myHighScores.playername = names[i];
                myHighScores.playtime = playtimes[i];
                highScoreList.Add(myHighScores);

            }
            //Adding new values to list
            myHighScores = new HighScore();
            myHighScores.playername = gameManager.username;
            myHighScores.playtime = gameManager.time;

            highScoreList.Add(myHighScores);

            //Resizing arrays to fit new data
            //print("BUG before resize" + names.Length);
            System.Array.Resize(ref names, names.Length + 1);
            //print("BUG after resize" + names.Length);
            System.Array.Resize(ref playtimes, playtimes.Length + 1);

            //Adding current vallues to array
            names[names.Length - 1] = gameManager.username;
            playtimes[playtimes.Length - 1] = gameManager.time;

            //Overwritting playerprefs with new data
            PlayerPrefsX.SetStringArray("PlayerNames", names);
            PlayerPrefsX.SetFloatArray("PlayerTimes", playtimes);

        }
        SortList();
        DisplayData();
    }
    
    void SortList() 
    {
        //sorting list by play time
        highScoreList.Sort((p1, p2) => p1.playtime.CompareTo(p2.playtime));
    }
    void DisplayData() 
    {
        //Displaying data in game slider
        for (int i = 0; i < highScoreList.Count; i++) 
        {


            float minutes = Mathf.FloorToInt(highScoreList[i].playtime / 60f);
            float seconds = Mathf.FloorToInt(highScoreList[i].playtime % 60f);

            contentText.text += "\n" + highScoreList[i].playername + " " + string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    
    }

    public void DebugBTN() 
    {
        print("BUG Names Length" + names.Length);
        print("BUG List Length" + highScoreList.Count);


    }




}
