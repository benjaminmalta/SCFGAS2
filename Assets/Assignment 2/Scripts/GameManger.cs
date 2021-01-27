using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public GameObject robotAI;
    public GameObject obstacleObject;
    
    List<Vector3> positionRecord = new List<Vector3> { };

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "Task1")
        {
            Task1();
        }
        if (SceneManager.GetActiveScene().name == "Task2")
        {
            Task2();
        }
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
