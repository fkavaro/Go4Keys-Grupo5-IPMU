using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject victoryAdvice;
    [SerializeField] GameObject caughtAdvice;
    [SerializeField] GameObject staminaAdvice;

    //Sounds
    [SerializeField] AudioSource victorySound;
    [SerializeField] AudioSource lossSound;

    // Start is called before the first frame update
    void Start()
    {
        victoryAdvice.SetActive(false);
        caughtAdvice.SetActive(false);
        staminaAdvice.SetActive(false);
    }

    public void Victory()
    {
        victorySound.Play();

        victoryAdvice.SetActive(true);
    }
    public void Caught()
    {
        lossSound.Play();

        caughtAdvice.SetActive(true);
    }

    public void Stamina()
    {
        lossSound.Play();

        staminaAdvice.SetActive(true);
    }


}
