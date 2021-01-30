using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HighScore
{
    public string playername;
    public float playtime;

    public HighScore(string pname,float ptime)
    {
        playername = pname;
        playtime = ptime;
    }
}


public class highScoreController : MonoBehaviour
{
    // Start is called before the first frame update
    List<HighScore> myHighScores;
    GameManager gameManager;

    public TMP_Text contentText;

    void Start()
    {
        LoadList();
        gameManager = FindObjectOfType<GameManager>();
        myHighScores = new List<HighScore>();

        myHighScores.Add(new HighScore(gameManager.username, gameManager.time));
        //myHighScores.Add(new HighScore("1", 1));
       
        SaveList();
        DisplayList();

    }

    void DisplayList()
    {
        foreach (HighScore s in myHighScores)
        {
            Debug.Log(s.playername + " " + s.playtime);
            contentText.text = (s.playername + " with time: " + s.playtime);

        }



    }

    void SaveList()
    {
        string[] names = new string[myHighScores.Count];

        float[] playertimes = new float[myHighScores.Count];

        int counter = 0;
        foreach (HighScore s in myHighScores)
        {
            names[counter] = s.playername;
            playertimes[counter] = s.playtime;
            counter++;
        }


        PlayerPrefsX.SetStringArray("PlayerNames",names);
        PlayerPrefsX.SetFloatArray("PlayerTimes", playertimes);

    }


    void LoadList()
    {
        string[] names;

        float[] playertimes;

        names = PlayerPrefsX.GetStringArray("PlayerNames");
        playertimes = PlayerPrefsX.GetFloatArray("PlayerTimes");

        
        for (int i = 0; i < names.Length; i++)
        {
            contentText.text += (names[0].ToString() + " with time: " + playertimes[0].ToString());
            print(names.Length);
            print(names[i].ToString() + " with time: " + playertimes[i].ToString());

        }


    }


}
