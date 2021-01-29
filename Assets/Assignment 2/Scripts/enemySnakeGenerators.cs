using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class enemySnakeGenerators : MonoBehaviour
{
    public GameObject AI;
    public GameObject breadcrumbBox;
    public int snakelength;

    GameObject enemyBox, pathParent;
    public Transform spawnLocation;
    List<positionRecord> pastPositions;
    int positionorder = 0;

    

    bool firstrun = true;
    // Start is called before the first frame update
    void Start()
    {
        
        SpawnSnakes();




        pathParent = new GameObject();

        pathParent.transform.position = new Vector3(0f, 0f);

        pathParent.name = "Path Parent";
    }

    // Update is called once per frame
    void Update()
    {
        //clearTail();

        print(pastPositions.Count);


    }

    public void SpawnSnakes() 
    {
        
        enemyBox = Instantiate(AI, spawnLocation.position, Quaternion.identity);
        enemyBox.GetComponent<SpriteRenderer>().color = Color.red;
        enemyBox.name = "AISnake";

        pastPositions = new List<positionRecord>();

        drawTail(snakelength);

    }



    bool boxExists(Vector3 positionToCheck)
    {
        //foreach position in the list

        foreach (positionRecord p in pastPositions)
        {

            if (p.Position == positionToCheck)
            {
                Debug.Log(p.Position + "Actually was a past position");
                if (p.BreadcrumbBox != null)
                {
                    Debug.Log(p.Position + "Actually has a red box already");
                    //this breaks the foreach so I don't need to keep checking
                    return true;
                }
            }
        }

        return false;
    }

    public void savePosition()
    {
        positionRecord currentBoxPos = new positionRecord();

        currentBoxPos.Position = enemyBox.transform.position;
        positionorder++;
        currentBoxPos.PositionOrder = positionorder;

        if (!boxExists(enemyBox.transform.position))
        {
            currentBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, enemyBox.transform.position, Quaternion.identity);

            currentBoxPos.BreadcrumbBox.transform.SetParent(pathParent.transform);

            currentBoxPos.BreadcrumbBox.name = positionorder.ToString();

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.red;

            currentBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().sortingOrder = -1;
        }

        pastPositions.Add(currentBoxPos);
        Debug.Log("Have made this many moves: " + pastPositions.Count);

    }


    public void drawTail(int length)
    {
        clearTail();
        print("AT drawtail " + pastPositions.Count);
        if (pastPositions.Count > length)
        {
            print("AT drawtail IF");
            //nope
            //I do have enough positions in the past positions list
            //the first block behind the player
            int tailStartIndex = pastPositions.Count - 2;
            int tailEndIndex = tailStartIndex - length;


            //if length = 4, this should give me the last 4 blocks
            for (int snakeblocks = tailStartIndex; snakeblocks > tailEndIndex; snakeblocks--)
            {
                //prints the past position and its order in the list
                //Debug.Log(pastPositions[snakeblocks].Position + " " + pastPositions[snakeblocks].PositionOrder);

                //Debug.Log(snakeblocks);

                pastPositions[snakeblocks].BreadcrumbBox = Instantiate(breadcrumbBox, pastPositions[snakeblocks].Position, Quaternion.identity);
                //pastPositions[snakeblocks].BreadcrumbBox.GetComponent<SpriteRenderer>().color = snakeColor;

            }

        }

        if (firstrun)
        {

            //I don't have enough positions in the past positions list
            for (int count = length; count > 0; count--)
            {
                positionRecord fakeBoxPos = new positionRecord();
                float ycoord = spawnLocation.position.y - count;
                fakeBoxPos.Position = new Vector3(spawnLocation.position.x, ycoord);
                // Debug.Log(new Vector3(0f, ycoord));
                //fakeBoxPos.BreadcrumbBox = Instantiate(breadcrumbBox, fakeBoxPos.Position, Quaternion.identity);
                //fakeBoxPos.BreadcrumbBox.GetComponent<SpriteRenderer>().color = Color.yellow;
                pastPositions.Add(fakeBoxPos);




            }
            firstrun = false;
            drawTail(length);
            //Debug.Log("Not long enough yet");
        }

    }

    void clearTail()
    {
        cleanList();
        foreach (positionRecord p in pastPositions)
        {
            //Debug.Log("Destroy tail" + pastPositions.Count);
            Destroy(p.BreadcrumbBox);
        }
    }

    void cleanList()
    {
        for (int counter = pastPositions.Count - 1; counter > pastPositions.Count; counter--)
        {
            pastPositions[counter] = null;
        }
    }



}
