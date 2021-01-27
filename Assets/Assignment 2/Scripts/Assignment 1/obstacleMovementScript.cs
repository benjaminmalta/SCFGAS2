using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleMovementScript : MonoBehaviour
{
    // Start is called before the first frame update

    public int addTo;
    public int removeFrom;
    public bool toggleForX;
    public int speed;
    private Vector3 origin;
    Vector3 originPlus;
    Vector3 originMinus;

   

    void Start()
    {
        origin = this.transform.position;

        if (toggleForX)
        {
            originPlus = origin + new Vector3(addTo, 0, 0);
            originMinus = origin - new Vector3(removeFrom, 0, 0);
        }
        else 
        {
            originPlus = origin + new Vector3(0, addTo, 0);
            originMinus = origin - new Vector3(0, removeFrom, 0);
        }


        StartCoroutine(move());

    }

    private void Update()
    {        

    }


    IEnumerator move() 
    {

        while (true)
        {

            for (int i = 0; i < addTo; i++) {
                print("Moving towards: "+originPlus);
                this.transform.position = Vector3.MoveTowards(this.transform.position, originPlus, 1f);
                yield return new WaitForSeconds(1 / speed);
            }

            for (int i = 0; i < addTo; i++)
            {
                print("Moving towards: " + origin);
                this.transform.position = Vector3.MoveTowards(this.transform.position, origin, 1f);
                yield return new WaitForSeconds(1 / speed);
            }

            for (int i = 0; i < removeFrom; i++)
            {
                print("Moving towards: " + originMinus);
                this.transform.position = Vector3.MoveTowards(this.transform.position, originMinus, 1f);
                yield return new WaitForSeconds(1 / speed);
            }

            for (int i = 0; i < removeFrom; i++)
            {
                print("Moving towards: " + origin);
                this.transform.position = Vector3.MoveTowards(this.transform.position, origin, 1f);
                yield return new WaitForSeconds(1 / speed);
            }
        
        }



        
    }








}
