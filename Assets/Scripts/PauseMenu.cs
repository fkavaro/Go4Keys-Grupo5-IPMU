using System;
using UnityEngine;
using UnityEngine.SceneManagement;

//HANDLES PAUSE MENU

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseButton;
    public GameObject resumeButton;
    [SerializeField] GameObject replayButton;
    [SerializeField] GameObject pauseMenuUI;
    private bool gameInPause = false;
    private bool gameEnded = false;

    //Sounds
    [SerializeField] AudioSource pauseSound;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;//Normal time

        pauseButton.SetActive(true);//Shows pause button
        resumeButton.SetActive(false);//Hides resume button
        replayButton.SetActive(false);//Hides replay button
        pauseMenuUI.SetActive(false);//Hides pause menu UI
    }

    // Update is called once per frame
    void Update()
    {
        //'Esc' pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseSound.Play();

            //Game hasn't finished
            if (!gameEnded)
            {
                //Game paused
                if (gameInPause)
                {
                    Resume();
                }
                //Game unpaused
                else
                {
                    Pause();
                }
            }
            //Game has finished
            else
            {
                //Replays game
                ReplayGame();
            }
            
        }
    }

    //End result UI (shows replay game button as well)
    public void EndResult()
    {
        gameEnded = true;

        pauseMenuUI.SetActive(true);
        resumeButton.SetActive(false);
        pauseButton.SetActive(false);
        replayButton.SetActive(true);

        Time.timeScale = 0f;
    }

    //Resumes simulation
    public void Resume()
    {
        gameInPause = false;

        pauseMenuUI.SetActive(false);
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
        replayButton.SetActive(false);

        Time.timeScale = 1f;
    }

    //Stops simulation
    public void Pause()
    {        
        gameInPause = true;

        pauseMenuUI.SetActive(true);
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
        replayButton.SetActive(false);

        Time.timeScale = 0f;
    }

    //Replays game
    public void ReplayGame()
    {
        Time.timeScale = 1f;//Resumes simulation

        //Get the current active scene
        Scene currentScene = SceneManager.GetActiveScene();

        //Reload the current scene
        SceneManager.LoadScene(currentScene.name);
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
