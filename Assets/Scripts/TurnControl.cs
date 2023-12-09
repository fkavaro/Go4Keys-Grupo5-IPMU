using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//TURN IN INTERSECTIONS TURNPOINTS

public class TurnControl : MonoBehaviour
{
    //Positions
    Vector2 position2;//Position (X,Z) of this object 
    Vector2 turnPoint;//Position or center (X, Z) of turn area 
    
    //Restrictions
    bool canTurn = false;//Can make a turn
    bool left = false;//Has turned left
    bool right = false;//has turned right

    // Update is called once per frame
    void Update()
    {
        //Saves X,Z position of this object
        position2 = new Vector2(transform.position.x, transform.position.z);

        //Triggers collider of intersection and hasn't turned yet
        if (canTurn)
        {
            Turn();
        }
    }

    //Check trigger with an object
    private void OnTriggerEnter(Collider other)
    {
        //With an intersection
        if (other.gameObject.CompareTag("Intersection"))
        {
            Debug.Log("At intersection");

            //Can make a turn
            canTurn=true;

            //Saves intersection center
            turnPoint = new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.z) ;
        }

    }

    //Turn to last selected side before reaching turnpoint
    void Turn()
    {
        //The distance between this object and the turnpoint at least 0.1f
        if (Vector2.Distance(position2, turnPoint) <= 0.1f)
        {
            Debug.Log("In turn point");

            //Turn to LEFT
            if (left)
            {
                transform.Rotate(0f, -90f, 0f);
                left = false;

                Debug.Log("Has rotated to left");
            }

            //Turn to RIGHT
            if (right)
            {
                transform.Rotate(0f, 90f, 0f);
                right = false;

                Debug.Log("Has rotated to right");
            }

            //Has passed the turnpoint
            canTurn = false;

        }
        else
        {
            //A is pressed:Turn to LEFT
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log("A pressed");
                left = true;
                right = false;//The other side is neglected
            }
            //D is pressed: turn to RIGHT
            if (Input.GetKeyDown(KeyCode.D))
            {
                Debug.Log("D pressed");
                right = true;
                left = false;//The other side is neglected
            }
        }
    }
}

/////////////////////////////////////////////////////////////////////////////////////////////
///PSEUDOCODE
///
/// before first frame (start):
///     
/// every frame (update):
///     checks collision with turn area
///         if so, chooses side to turn before arriving to turn point (center of turn area)
///         rotates to that side at turn point