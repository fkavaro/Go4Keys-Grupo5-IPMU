using UnityEngine;

//HANDLES TUTOIRIAL POP-UPS

public class TutorialManager : MonoBehaviour
{
    [SerializeField] bool showTutorial = false;//Learn tutorial
    [SerializeField] Transform popUpsParent;//Its children are the popUps
    private GameObject[] popUps;//Array of popUps
    private int current = 0;//Current popUp

    [SerializeField] Transform checker;//Detects intersection
    [SerializeField] float checkerRadious = 1f;
    [SerializeField] LayerMask triggerMask;//Intersection
    private bool atIntersection = false;
    
    [SerializeField] float showTime = 4f;//Time certain popUps will be shown

    // Start is called before the first frame update
    void Start()
    {
        //Want to learn the tutorial
        if (showTutorial)
        {
            popUpsParent.gameObject.SetActive(true);


            //Creates array the size of number of children of popUpsParent
            popUps = new GameObject[popUpsParent.childCount];

            //Fills the array with each gameObject
            for (int i = 0; i < popUps.Length; i++)
            {
                popUps[i] = popUpsParent.GetChild(i).gameObject;

                //Deactivates all popUps
                popUps[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Want to learn the tutorial
        if (showTutorial)
        {
            CheckIntersection();

            switch (current)
            {
                //Move left 'A'
                case 0:
                    popUps[current].SetActive(true);

                    //When 'A' pressed
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        popUps[current].SetActive(false);

                        //Next popUp
                        current = 1;
                    }
                    break;

                //Move right 'D'
                case 1:
                    popUps[current].SetActive(true);

                    //When 'D' pressed
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        popUps[current].SetActive(false);

                        //Next popUp
                        current = 2;
                    }
                    break;

                //Jump 'Space'
                case 2:
                    popUps[current].SetActive(true);

                    //When 'Space' pressed
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        popUps[current].SetActive(false);

                        //Next popUp
                        current = 3;
                    }
                    break;

                //Stamina
                case 3:
                    popUps[current].SetActive(true);

                    //Pauses simulation so that player can read the popUp
                    PauseGame();

                    //When any key down
                    if (Input.anyKeyDown)
                    {
                        popUps[current].SetActive(false);

                        //Next popUp
                        current = 4;
                    }
                    break;

                //Keys
                case 4:
                    popUps[current].SetActive(true);

                    //Simulation is still paused

                    //When any key down
                    if (Input.anyKeyDown)
                    {
                        popUps[current].SetActive(false);

                        //Next popUp
                        current = 5;
                    }
                    break;

                //Telephone
                case 5:
                    popUps[current].SetActive(true);

                    //Simulation is still paused

                    //When any key down
                    if (Input.anyKeyDown)
                    {
                        popUps[current].SetActive(false);

                        //Next popUp
                        current = 6;

                        //Resumes simulation
                        ResumeGame();
                    }
                    break;

                //Intersection
                case 6:
                    //In intersection
                    if (atIntersection)
                    {
                        popUps[current].SetActive(true);

                        //When 'A' or 'D' pressed
                        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                        {
                            popUps[current].SetActive(false);

                            //Next popUp
                            current++;
                        }
                    }
                    break;

                //Don't get caught
                default:
                    popUps[current].SetActive(true);

                    //Timer ends
                    if (showTime <= 0)
                    {
                        popUps[current].SetActive(false);
                    }
                    else
                    {
                        //Reduces timer
                        showTime -= Time.deltaTime;
                    }

                    break;
            }
        }
    }

    private void CheckIntersection()
    {
        //Creates a sphere with certain radious at checker position that will get triggered by certain mask
        if (Physics.CheckSphere(checker.position,checkerRadious, triggerMask))
        {
            atIntersection= true;
        }
    }

    void PauseGame()
    {
        // Set the time scale to 0 to pause the game
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        // Set the time scale back to 1 to resume the game
        Time.timeScale = 1f;
    }
}
