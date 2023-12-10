using UnityEngine;

//MOVES PARENT FORWARD IF THERE ISN'T AN OBSTACLE IN FRONT AND HANDLES INPUT TO JUMP

public class EndlessRunner : MonoBehaviour
{
    //Empty box for player's rigid body
    Rigidbody player;

    //Parent of this object
    Transform parent;

    //Speeds
    [SerializeField] float forwardSpeed = 5.0f;//Moving forward
    private float forwardValue;
    [SerializeField] float jumpForce = 8.0f;//Jumping

    //Checkers of layer in that position
    [SerializeField] Transform groundChecker;
    [SerializeField] Transform obstacleChecker;

    //Layers
    [SerializeField] LayerMask jumpLayer;//Only jump when touching this layer
    [SerializeField] LayerMask stopLayer;//Stops when touching this layer

    //Audio
    [SerializeField] AudioSource jumpSound;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game has started!");

        //Save player's rigid body in emptybox
        player = transform.GetComponent<Rigidbody>();

        parent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        MoveForward();
        Input();
    }

    //Move forward automatically if possible (no obstacle in front)
    private void MoveForward()
    {
        //Will update forward speed accordingly
        CheckObstacle();

        //Move the object at forward speed to the orientation it's facing
        parent.Translate(forwardValue * Time.deltaTime * Vector3.forward);

    }

    //Check for obstacles in front
    private void CheckObstacle()
    {
        //Creates a sphere in checker that's triggered by an object of the stop layer (obstacle) 0.3
        if (Physics.CheckSphere(obstacleChecker.position, .4f, stopLayer))
        {
            Debug.Log("Player has stopped");

            //Stop moving
            forwardValue = 0.0f;
        }
        else
        {
            //Resume moving
            forwardValue = forwardSpeed;
        }
    }

    private void Input()
    {
        //Space key pressed 
        if (UnityEngine.Input.GetKeyDown(KeyCode.Space) && CheckGround())//Jumps if Space is pressed AND it's on floor
        {
            //Calls jump function
            Jump();
        }
    }

    //Moves the player in Y axis to jump height
    public void Jump()
    {
        //Maintains velocities in x and z axis but increments the y with jumpforce
        player.velocity = new Vector3(player.velocity.x, jumpForce, player.velocity.z);
        jumpSound.Play();

        Debug.Log("Jump");
    }

    //Check it's touching ground with checker
    bool CheckGround()
    {
        //Creates a small sphere in feet position that detects the floor layer
        return Physics.CheckSphere(groundChecker.position, .3f, jumpLayer);
    }
}

////////////////////////////////////////////////////////////////////////////
///PSEUDOCODE
///
/// before first frame (start):
///     get rigid body
/// every frame (update):
///     check if there's an obstacle in front
///         if so, stop
///         it not so, move parent forwards
///     jump if key pressed and on floor
/// 