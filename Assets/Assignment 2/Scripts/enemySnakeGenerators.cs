using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class enemySnakeGenerators : MonoBehaviour
{
    public GameObject AI;
    [System.Serializable]
    public class enemySnake
    {
        public Vector3 spawnLocation;
        public int snakeLength;
    }

    public List<enemySnake> enemies = new List<enemySnake>();
    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSnakes() 
    {
        for (int i = 0; i < enemies.Count; i++) {
            Instantiate(AI, enemies[i].spawnLocation, Quaternion.identity);
        }
    }

}
