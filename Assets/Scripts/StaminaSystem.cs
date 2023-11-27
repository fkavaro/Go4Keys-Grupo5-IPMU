using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StaminaSystem : MonoBehaviour
{


    [SerializeField] Transform obstacleChecker;
    [SerializeField] LayerMask damageLayer;

    // AUDIO
    [SerializeField] AudioSource energyDrinkSound;

    public float maxStamina = 100;
    public float stamina;

    public bool useFixedUpdate;
    public float changePerSecond = 1f;

    public StaminaBar staminaBar;



    bool crash = false;


    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
    }

    // Update is called once per frame
    void Update()
    {
        CheckObstacle();


        if (!useFixedUpdate)
        {
            stamina -= changePerSecond * Time.deltaTime;
            staminaBar.SetStamina(stamina);
        }

    }




    private void CheckObstacle()
    {
        //Creates a sphere in checker that's triggered by an object of the stop layer (obstacle)
        if (Physics.CheckSphere(obstacleChecker.position, .3f, damageLayer))
        {
            if (!crash)
            {
                LoseStamina(5f);
                crash = true;
            }

        }
        else
        {
            crash = false;
        }

    }

    private void LoseStamina(float value)
    {
        stamina = stamina - value;
        staminaBar.SetStamina(stamina);
    }

    private void OnTriggerEnter(Collider other)//works same as onCollisionEnter
    {
        //If the object triggered is an energy drink
        if (other.gameObject.CompareTag("EnergyDrink"))
        {

            //Destroys it
            Destroy(other.gameObject);
            stamina = stamina + 5f;
            energyDrinkSound.Play();
            staminaBar.SetStamina(stamina);

        }
    }



    private void FixedUpdate()
    {
        stamina -= changePerSecond * Time.deltaTime;
        staminaBar.SetStamina(stamina);
    }

}




