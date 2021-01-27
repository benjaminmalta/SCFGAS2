using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KU4PatrolBehaviourScript : MonoBehaviour
{

    public List<Transform> waypoints;
   
    private int index = 0;


    //the object that we are using to generate the path
    Seeker seeker;

    //path to follow stores the path
    Path pathToFollow;


    //a reference from the UI to the green box
    //private Transform target;

    //a reference to PointGraphObject
    GameObject graphParent;


    //the node of the graph that is going to correspond with the green box
    GameObject targetNode;

    // Start is called before the first frame update
    void Start()
    {


        Debug.Log(this.name);

        //the instance of the seeker attached to this game object
        seeker = GetComponent<Seeker>();


        //node target by name
        targetNode = GameObject.Find("TargetNode");

        //find the parent node of the point graph
        //graphParent = GameObject.Find("PointGraphObject");
        graphParent = GameObject.Find("AStarGrid");
        //we scan the graph to generate it in memory
        graphParent.GetComponent<AstarPath>().Scan();
        ;
        //generate the initial path
        pathToFollow = seeker.StartPath(transform.position, waypoints[index].position);


        StartCoroutine(moveTowardsEnemy(this.transform));
        //update the graph as soon as you can.  Runs indefinitely
        StartCoroutine(updateGraph());

        //move the red robot towards the green enemy



    }

    // Update is called once per frame
    void Update()
    {
       
        if (this.transform.position == waypoints[index].position) 
        {
            index++;
            if (index > 9) {
                index = 0;
            }

            pathToFollow = seeker.StartPath(transform.position, waypoints[index].position);

        }


    }




    IEnumerator updateGraph()
    {
        while (true)
        {

            //   targetNode.transform.position = target.position;
            graphParent.GetComponent<AstarPath>().Scan();


            yield return null;

        }

    }




    IEnumerator moveTowardsEnemy(Transform t)
    {

        while (true)
        {

            List<Vector3> posns = pathToFollow.vectorPath;
            Debug.Log("Positions Count: " + posns.Count);

            for (int counter = 0; counter < posns.Count; counter++)
            {
                // Debug.Log("Distance: " + Vector3.Distance(t.position, posns[counter]));
                if (posns[counter] != null)
                {
                    while (Vector3.Distance(t.position, posns[counter]) >= 0.5f)
                    {
                        t.position = Vector3.MoveTowards(t.position, posns[counter], 1f);
                        //since the enemy is moving, I need to make sure that I am following him
                        pathToFollow = seeker.StartPath(t.position, waypoints[index].position);
                        //wait until the path is generated
                        yield return seeker.IsDone();
                        //if the path is different, update the path that I need to follow
                        posns = pathToFollow.vectorPath;

                        //  Debug.Log("@:" + t.position + " " + target.position + " " + posns[counter]);
                        yield return new WaitForSeconds(0.2f);
                    }

                }
                //keep looking for a path because if we have arrived the enemy will anyway move away
                //This code allows us to keep chasing
                pathToFollow = seeker.StartPath(t.position, waypoints[index].position);
                yield return seeker.IsDone();
                posns = pathToFollow.vectorPath;
                //yield return null;

            }
            yield return null;
        }
    }

   


}
