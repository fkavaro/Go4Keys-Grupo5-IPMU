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

    //Sounds
    [SerializeField] AudioSource turningSound;

    // Update is called once per frame
    void Update()
    {
        //Saves X,Z position of this object
        position2 = new Vector2(transform.position.x, transform.position.z);

        //Has entered an intersection
        if (canTurn)
        {
            Turn();
        }
    }

    //Enters trigger area
    private void OnTriggerEnter(Collider other)
    {
        //With an intersection
        if (other.gameObject.CompareTag("Intersection"))
        {
            Debug.Log("At intersection");

            //Saves intersection center (X,Z)
            turnPoint = new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.z) ;

            Debug.Log(turnPoint);

            //Can make a turn
            canTurn = true;
        }

    }

    //Exits trigger area
    private void OnTriggerExit(Collider other)
    {
        //With an intersection
        if (other.gameObject.CompareTag("Intersection"))
        {
            Debug.Log("No longer at intersection");

            //Resets values
            canTurn = false;
            left = false;
            right = false;
        }
    }

    //Turn to last selected side before reaching turnpoint
    void Turn()
    {
        //The distance between this object and the turnpoint at least 0.4f
        if (Vector2.Distance(position2, turnPoint) <= 0.4f)
        {
            Debug.Log("At turn point");

            //Turn to LEFT
            if (left)
            {
                //Centers object to intersection
                position2 = turnPoint;

                //Rotates object
                transform.Rotate(0f, -90f, 0f);

                //Can't make any more turn 
                canTurn = false;

                turningSound.Play();

                Debug.Log("Has rotated to left");
            }

            //Turn to RIGHT
            else if (right)
            {
                //Centers object to intersection
                position2 = turnPoint;

                //Rotates object
                transform.Rotate(0f, 90f, 0f);

                //Can't make any more turn 
                canTurn = false;

                turningSound.Play();

                Debug.Log("Has rotated to right");
            }

            //Has passed the turnpoint and continued forward
            else
            {
                Debug.Log("Has continued forward");

                canTurn = false;
            }

        }
        //Not in turn point
        else
        {
            //Hasn't made a turn
            if (canTurn)
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
}

/////////////////////////////////////////////////////////////////////////////////////////////
///PSEUDOCODE
///
/// before first frame (start):
///     
/// every frame (update):
///     updates position (X,Z) of this object
///     checks collision with turn area
///         if so, chooses side to turn before arriving to turn point (center of turn area)
///         rotates to that side at turn point