using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour
{
    public GameObject wonResultUI;
    public GameObject lostResultUI;


    public void YouWon()
    {

        wonResultUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void YouLost()
    {
        lostResultUI.SetActive(true);
        Time.timeScale = 0f;

    }

}
