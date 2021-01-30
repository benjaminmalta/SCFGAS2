using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleMoveWaypoints : MonoBehaviour
{

    public List<Transform> waypoints;
    GameManager gameManager;
    int currentWayPoint ;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        currentWayPoint = 0;
        StartCoroutine(Move());
    }


    IEnumerator Move()
    {

        while (true) { 
        if (this.transform.position != waypoints[currentWayPoint].position)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, waypoints[currentWayPoint].position, 1f);
            gameManager.Scan();
            yield return new WaitForSeconds(1);
        }
        else 
        {
            currentWayPoint++;            
        }
        
        if (currentWayPoint >= waypoints.Count) {
            currentWayPoint = 0;
            yield return new WaitForSeconds(1);
        }
        }
        

    }





    // Update is called once per frame
    void Update()
    {
        
    }
}
