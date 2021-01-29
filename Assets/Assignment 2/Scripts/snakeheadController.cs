using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class snakeheadController : MonoBehaviour
{
    snakeGenerator mysnakegenerator;
    foodGenerator myfoodgenerator,myfoodgenerator2;
    Transform targetLocation;
   

    public Vector3 findClosestFood()
    {
        if (myfoodgenerator.allTheFood.Count > 0)
        {
            List<positionRecord> sortedFoods = myfoodgenerator.allTheFood.OrderBy(
        x => Vector3.Distance(this.transform.position, x.Position)
       ).ToList();
            return sortedFoods[0].Position;
        }
        return new Vector3(0f, 0f);
    }

    public IEnumerator automoveCoroutine()
    {
        while(true)
        {


            yield return null;
        }
        
    }



    private void Start()
    {
        mysnakegenerator = Camera.main.GetComponent<snakeGenerator>();
        myfoodgenerator = Camera.main.GetComponent<foodGenerator>();

        targetLocation = GameObject.Find("Target").GetComponent<Transform>();
        
        

    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collided with: " + collision.transform.tag);
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (collision.transform.tag == "targetnode") 
            {
                if (mysnakegenerator.snakelength >= 6) 
                { 
                SceneManager.LoadScene("Level2");
                }
            }

            if (collision.transform.tag == "Walls")
            {
                SceneManager.LoadScene("DeathScene");
            }

        }

        
    }



    void checkBounds()
    {
        if ((transform.position.x < -(Camera.main.orthographicSize)))
        {
            transform.position = new Vector3(-transform.position.x - 1,transform.position.y);
        }

        if ((transform.position.x > (Camera.main.orthographicSize)))
        {
            transform.position = new Vector3(-transform.position.x + 1, transform.position.y);
        }

        if ((transform.position.y < -(Camera.main.orthographicSize)))
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y -1);
        }

        if ((transform.position.y > (Camera.main.orthographicSize)))
        {
            transform.position = new Vector3(transform.position.x, -transform.position.y +1);
        }


    }

    // Update is called once per frame
    void Update()
    {
        /*if (this.transform.position == targetLocation.position)
        {
            //print("Snake Reached Target");

        }*/

        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            //Debug.LogWarning("Closest food" + findClosestFood());
            transform.position -= new Vector3(1f,0);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
            Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength));

            if (mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength))
            {
                print("DIE!");
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //Debug.LogWarning("Closest food" + findClosestFood());
            transform.position += new Vector3(1f, 0);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
            Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength));

            if (mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength))
            {
                print("DIE!");
            }

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //Debug.LogWarning("Closest food" + findClosestFood());
            transform.position += new Vector3(0, 1f);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
            Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength));

            if (mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength))
            {
                print("DIE!");
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Debug.LogWarning("Closest food" + findClosestFood());
            transform.position -= new Vector3(0, 1f);
            checkBounds();
            myfoodgenerator.eatFood(this.transform.position);
            Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength));

            if (mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength))
            {
                print("DIE!");
            }
        }

        

        //Debug.Log(mysnakegenerator.hitTail(this.transform.position, mysnakegenerator.snakelength)); 
    }
}
