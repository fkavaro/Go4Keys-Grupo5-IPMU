using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject resumeButton;
    [SerializeField] GameObject pauseMenuUI;
    private bool GameInPause = false;

    // Start is called before the first frame update
    void Start()
    {
        pauseButton.SetActive(true);//Shows pause button
        resumeButton.SetActive(false);//Hides resume button
        pauseMenuUI.SetActive(false);//Hides pause menu UI
    }

    // Update is called once per frame
    void Update()
    {
        //'Esc' pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameInPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //Resumes simulation
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        GameInPause = false;
    }

    //Stops simulation
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
        GameInPause = true;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;//Resumes simulation
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);//Loads main menu scene
    }
    public void QuitGame()
    {
        Debug.Log("QUIT!!");
        Application.Quit();
    }

}
