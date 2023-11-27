using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Audio;

//HANDLES KEYBOARD INPUTS TO MOVE PLAYER BETWEEN RAILS

public class RailControl : MonoBehaviour
{

    //Speed
    [SerializeField] float sideSpeed = 6.0f;//Changing of rail

    //Rails of movement
    [SerializeField] Transform railsParent;//Its children are the diferrent rails
    [SerializeField] float[] rails;//Array of X position of each rail
    private Vector3 currentRail;//Vector of current rail position
    public int currentIndex = 1;//Index of current rail (in array)

    // Start is called before the first frame update
    void Start()
    {
        //Creates array the size of number of children of railsParent
        rails = new float[railsParent.childCount];

        //Fills the array with the positions
        for (int i = 0; i < rails.Length; i++)
        {
            rails[i] = railsParent.GetChild(i).position.x;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Input();

        UpdateCurrentRail();

        MoveTowardsCurrentRail();
    }

    //Handles inputs (move to left or right rail, and jump)
    void Input()
    {
        //A key pressed: change to left rail
        if (UnityEngine.Input.GetKeyDown(KeyCode.A))
        {
            //Not in the leftMost rail
            if (currentIndex > 0)
            {
                //Move one rail to left
                currentIndex--;
            }
        }

        //D key pressed: change to right rail
        else if (UnityEngine.Input.GetKeyDown(KeyCode.D))
        {
            //Not in the rightMost rail
            if (currentIndex < rails.Length - 1)
            {
                //Move one rail to right
                currentIndex++;
            }
        }
    }

    private void UpdateCurrentRail()
    {
        //Updates current rail vector with X position of the rail and Y and Z positions of this object
        currentRail = new Vector3(rails[currentIndex], transform.localPosition.y, transform.localPosition.z);
    }

    private void MoveTowardsCurrentRail()
    {
        //Moves to current rail at sideSpeed (according to local position = parent position)
        //localposition adapts the axis of this object after the parent has rotated
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, currentRail, sideSpeed * Time.deltaTime);
    }
}

////////////////////////////////////////////////////////////////////////////
///PSEUDOCODE
///
/// before first frame (start):
///     fill array with position in X axis of each waypoint
///     
/// every frame (update):
///     check if input to change rail is received
///     updates current rail
///     moves towards current rail position (X axis)
///     
