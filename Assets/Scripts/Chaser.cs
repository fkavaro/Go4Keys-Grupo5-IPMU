using UnityEngine;

//CHASE A TARGET GETTING CLOSER AND JUMPING OBSTACLES AUTOMATICALLY

public class Chaser : MonoBehaviour
{
    //Target
    [SerializeField] Transform target;//Object to chase

    //Speeds
    [SerializeField] float chaseSpeed = 0.3f;//Related to its local position (parent)
    float chaseSpeedValue;//Initial chase speed
    [SerializeField] float jumpForce = 7.0f;

    //Trigger layer
    [SerializeField] LayerMask jumpsOver;//Will automatically jump over this layer
    [SerializeField] LayerMask jumpsOn;//Will jump if also touches this layer
    [SerializeField] float checkerRadious = 1f;//Radious of chaser checker

    //Checkers
    [SerializeField] Transform chaserChecker;//To jump automatically
    [SerializeField] Transform targetChecker;//To adjust chase speed

    //Booleans
    bool targetCaught = false;//Chaser hit target


    public Result result;

    void Start()
    {
        chaseSpeedValue = chaseSpeed;//Save initial chase speed
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
        // Check for obstacles in front of target
        if (Physics.CheckSphere(targetChecker.position, .3f, jumpsOver))
        {
            //Increment chase speed because player has stopped
            chaseSpeed += chaseSpeedValue / 2;
        }
        else
        {
            //Original chase speed
            chaseSpeed = chaseSpeedValue;
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
            && Physics.CheckSphere(chaserChecker.position, checkerRadious, jumpsOn))
        {
            Jump();
        }
    }

    //Moves the player in Y axis to jump height
    private void Jump()
    {
        transform.Translate(jumpForce * Time.deltaTime * transform.up);//Transform.up to go upwards
    }

    private void OnCollisionEnter(Collision collision)
    {
        //The object collisioned has the same tag as the target
        if (collision.transform.CompareTag(target.transform.tag))
        {
            targetCaught = true;
            result.YouLost();

            Debug.Log("Chaser caught target");

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