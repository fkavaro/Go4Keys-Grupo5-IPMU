using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // AUDIO
    [SerializeField] AudioSource buttonSound;



    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        buttonSound.Play();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!!");
        Application.Quit();
        buttonSound.Play();
    }
}
