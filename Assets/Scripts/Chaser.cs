using UnityEngine;

//CHASE A TARGET GETTING CLOSER AND JUMPING OBSTACLES AUTOMATICALLY

public class Chaser : MonoBehaviour
{
    //Target
    [SerializeField] Transform target;//Object to chase

    //Speeds
    [SerializeField] float chaseSpeed = 0.1f;//Related to its parent speed
    private float originalSpeed;//Initial chase speed
    private float incrementedSpeed;//Speed when player has stopped
    [SerializeField] float jumpForce = 7.0f;

    //Trigger layer
    [SerializeField] LayerMask jumpsOver;//Will automatically jump over this layer
    [SerializeField] LayerMask jumpsOn;//Will jump if also touches this layer
    private float checkerRadious = .5f;//Radious of chaser checker

    //Checkers
    [SerializeField] Transform chaserChecker;//To jump automatically
    [SerializeField] Transform targetChecker;//To adjust chase speed

    //Booleans
    bool targetCaught = false;//Chaser hit target
    bool targetRunning = false;

    //Classes of UI
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] Result result;

    //Empty box for chaser's rigid body
    Rigidbody chaser;

    void Start()
    {
        originalSpeed = chaseSpeed;//Save initial chase speed
        incrementedSpeed = chaseSpeed * 30;

        //Save chaser's rigid body in emptybox
        chaser = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {
        UpdateChaseSpeed();

        if (!targetCaught)
        {
            ChaseTarget();
            EncounterObstacle();
        }
    }

    //Changes chase speed according to target movement
    private void UpdateChaseSpeed()
    {
        //Obstacle in front of target (has stopped)
        if (Physics.CheckSphere(targetChecker.position, .4f, jumpsOver)) //Same radious as EndlessRunner.CheckObstacle
        {
            Debug.Log("Chaser speed increased");

            //Target has stopped
            targetRunning = false;

            //Increment chase speed because player has stopped
            chaseSpeed = incrementedSpeed;
        }
        //No obstacle in front (target still running)
        else
        {
            //Original chase speed
            chaseSpeed = originalSpeed;

            //Target is running
            targetRunning = true;
        }
    }

    //The chaser will get nearer to target
    void ChaseTarget()
    {
        //Updates position replicating the target position in X
        transform.localPosition = new Vector3(target.localPosition.x, transform.localPosition.y, transform.localPosition.z);

        //Moves object forward the orientation it's facing 
        transform.Translate(chaseSpeed * Time.deltaTime * transform.forward, Space.World);
    }

    private void EncounterObstacle()
    {
        // Check for obstacles and jump if it's also touching ground
        if (Physics.CheckSphere(chaserChecker.position, checkerRadious, jumpsOver)
            && Physics.CheckSphere(chaserChecker.position, checkerRadious, jumpsOn)) {
            //Jumps if target is running
            if (targetRunning) {

                Debug.Log("Chaser jumped");

                Jump();
            }
        }
    }

    //Moves the chaser in Y axis to jump height
    private void Jump()
    {
        //Maintains velocities in x and z axis but increments the y with jumpforce
        chaser.velocity = new Vector3(chaser.velocity.x, jumpForce, chaser.velocity.z);

        Debug.Log("Chaser jumped");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //The object collisioned has the same tag as the target
        if (collision.transform.CompareTag(target.transform.tag))
        {
            Debug.Log("Chaser caught target");

            targetCaught = true;

            pauseMenu.EndResult();//End game UI
            result.Caught();//Result
        }
    }
}

////////////////////////////////////////////////////////////////////////////
///PSEUDOCODE
///
/// before first frame (start):
/// 
/// every frame (update):
///     check if target is moving
///         if so, maintain speed
///         if not so, increment speed
///     if target isn't caught
///         change chaser position on X axis as the target
///         move chaser forward at certain speed, getting closer to target
///         chaser jumps if encounters an obstacle
///         check if chaser collides with target
///             if so, game over