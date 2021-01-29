using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameManager : MonoBehaviour
{
    public Text usernameText;
    public string username;

    private GameObject robotAI;
    private GameObject obstacleObject;
    public GameObject timer;

    public float time = 0;
    GameObject timerUI;

    List<Vector3> positionRecord = new List<Vector3> { };

    void Awake() {

        setUpSingleton();

    }

    private void setUpSingleton()
    {
        int numberOfGameManagers = FindObjectsOfType<GameManager>().Length;
        if (numberOfGameManagers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
           
        }
    }

    private void Start()
    {
        
       

        
        
        

    }

    void Update() 
    {
        if (SceneManager.GetActiveScene().name == "StartingScene")
        {
            username = usernameText.text;
        }

        if (timerUI == null) { 
            if (!(SceneManager.GetActiveScene().name == "StartingScene"))
            {                   


                timerUI = Instantiate(timer, new Vector3(0f, 0f), Quaternion.identity);
                timerUI.GetComponentInChildren<timerManager>().timerStarted = true;
            
            }
        }

        if (SceneManager.GetActiveScene().name == "Level1")
        {
            Camera.main.GetComponent<foodGenerator>().enabled = true;
            Camera.main.GetComponent<snakeGenerator>().enabled = true;

            //Camera.main.GetComponent<enemySnakeGenerators>().enabled = true;
        }

        if (SceneManager.GetActiveScene().name == "WinScene")
        {
            timerUI.GetComponentInChildren<timerManager>().timerPaused = true;
        }




    }

        public void startGame() 
    {
        SceneManager.LoadScene("Level1");
    }






    
    private void Task1() 
    {
        //The order of executution of these methods is intentional to 
        
        ObstacleGenerator(5);
        //Scan(); No need as the custom AIMoveScript automatically scans constantly 
        StartExample();

    }
    private void Task2()
    {
        AIGenerator(10);
        //Scan(); No need as the custom AIMoveScript automatically scans constantly 
        StartExample();

         
        /* TASK 2 KU 1
         *  Local avoidance is a built in A* Pathfinding feature only available on A* pathfinding pro. It is based 
         *  on RVO: Reciprocal Velocity Obstacles. This is based on two parts, the first being simulation code 
         *  which has very little to do with unity specific classes, while the second part has much more to do 
         *  with the unity interface.   
         */
    }


    public void endGame() 
    {


        SceneManager.LoadScene("DeathScene");
    }





    private void ObstacleGenerator(int obstacles) 
    {
        for (int i = 0; i < obstacles; i++)
        {
            AddObstacle();
        }
    }


    private void AIGenerator(int ai)
    {
        for (int i = 0; i < ai; i++)
        {
            AddAI();
        }
    }

    public void Scan() {
        GameObject.Find("AStarGrid").GetComponent<AstarPath>().Scan();
    }

    public void AddObstacle()
    {
        Vector3 randomPos = new Vector3(Random.Range(-49, 49), Random.Range(-49, 49), 0);
        Instantiate(obstacleObject, randomPos, Quaternion.identity);
        positionRecord.Add(randomPos);
        print("Adding Obstacle");

    }

    public void AddAI() 
    {
        print("Not Adding AI");
        Vector3 randomPos = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0);
        if (!positionRecord.Contains(randomPos))
        {
           Instantiate(robotAI, randomPos, Quaternion.identity);            
            print("Adding AI");
        }

    }

    public void StartExample()
    {
        GameObject[] allRobots = GameObject.FindGameObjectsWithTag("Robot");

        foreach (GameObject robots in allRobots) 
        {
            robots.GetComponentInChildren<customAIMoveScriptGrid>().enabled = true;
        }
    }


}
