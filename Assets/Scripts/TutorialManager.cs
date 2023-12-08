using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//HANDLES TUTOIRIAL POP-UPS

public class TutorialManager : MonoBehaviour
{
    [SerializeField] Transform popUpsParent;//Its children are the popUps
    private GameObject[] popUps;//Array of popups
    private int current = 0;//Current popup
    [SerializeField] float waitTime = 3.0f;//Possible time between popups

    // Start is called before the first frame update
    void Start()
    {
        //Creates array the size of number of children of popUpsParent
        popUps = new GameObject[popUpsParent.childCount];

        //Fills the array with each gameObject
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i] = popUpsParent.GetChild(i).gameObject;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Toggling each popup after another
        for (int i = 0; i < popUps.Length; i++)
        {
            //Activates current popup
            if (i == current)
            {
                popUps[i].SetActive(true);
            }
            //Deactiates the rest
            else
            {
                popUps[i].SetActive(false);
            }
        }

        //Move left 'A'
        if (current == 0)
        {
            //Next popup when key pressed
            if (Input.GetKeyDown(KeyCode.A))
            {
                current++;
            }
        }
        //Move right 'D'
        else if (current == 1)
        {
            //Next popup when key pressed
            if (Input.GetKeyDown(KeyCode.D))
            {
                current++;
            }
        }


        //Jump 'Space' to avoid obstacles
        else if (current == 2)
        {
            //Next popup when key pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                current++;
            }
        }

        //Stamina reduces over time and when hitting obstacle, restores with energetics
        else if (current == 3)
        {
            //Pauses simulation
            //PauseGame();

            //Next popup when time passed and resets time
            if (waitTime <= 0)
            {
                current++;

                waitTime = 3.0f;
            }
            //Reduces wait time
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        //Keys
        else if (current == 4)
        {
            //Next popup when time passed and resets time
            if (waitTime <= 0)
            {
                current++;

                waitTime = 3.0f;
            }
            //Reduces wait time
            else
            {
                waitTime -= Time.deltaTime;
            }

            //Resumes simulation
            //ResumeGame();
        }

        //Police

        //Intersections

        //Don't get caught!!
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
