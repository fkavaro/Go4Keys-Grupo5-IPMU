using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] Transform obstacleChecker;
    [SerializeField] LayerMask damageLayer;
    [SerializeField] float lossPerSecond = 1f;
    [SerializeField] float lossPerCrash = 5f;
    [SerializeField] float recover = 15f;
    [SerializeField] StaminaBar staminaBar;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] Result result;

    // AUDIO
    [SerializeField] AudioSource energyDrinkSound;

    private float maxStamina = 100;
    private float stamina;

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        //Has enough stamina
        if (stamina > 0)
        {
            CheckObstacle();

            //Reduces stamina every second
            LoseStamina(lossPerSecond);
        }
        //Doesn't have any more stamina
        else
        {
            pauseMenu.EndResult();//End game UI
            result.Stamina();//Result
        }
    }

    private void CheckObstacle()
    {
        //Creates a sphere in checker that's triggered by an object of the damage layer (obstacle)
        if (Physics.CheckSphere(obstacleChecker.position, .3f, damageLayer))
        {
            //Loses stamina after hit
            LoseStamina(lossPerCrash);
        }
    }

    private void LoseStamina(float value)
    {
        stamina -= value * Time.deltaTime;
        staminaBar.SetStamina(stamina);
    }

    private void OnTriggerEnter(Collider other)//works same as onCollisionEnter
    {
        //If the object triggered is an energy drink
        if (other.gameObject.CompareTag("EnergyDrink"))
        {
            //Destroys it
            Destroy(other.gameObject);

            stamina += recover * Time.deltaTime;

            energyDrinkSound.Play();

            staminaBar.SetStamina(stamina);
        }
    }
}




