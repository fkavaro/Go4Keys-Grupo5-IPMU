using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collecter : MonoBehaviour
{

    // VARIABLES
    int keyCounter = 0;
    [SerializeField] Transform keysParent;
    int totalKeys;
    int keysleft;
    public GameObject[] keysUIGray;
    public GameObject[] keysUIYellow;

    public Result result;

    // AUDIO
    [SerializeField] AudioSource collectionSound;




    // Start is called before the first frame update
    void Start()
    {
        totalKeys = keysParent.childCount;
        keysleft = totalKeys;
        UpdateUI();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
            //Destroys it
            Destroy(other.gameObject);

            keyCounter++;
            keysleft--;

            UpdateUI();
            CheckWin();

            // Audio
            collectionSound.Play(); // key clin clin!
        }
    }


    private void UpdateUI()
    {
        // Desactiva todas las llaves en la interfaz
        foreach (GameObject keyUI in keysUIYellow)
        {
            keyUI.SetActive(false);
        }
        foreach (GameObject keyUIy in keysUIGray)
        {
            keyUIy.SetActive(false);
        }


        // Activa las llaves correspondientes al contador
        for (int i = 0; i < keyCounter; i++)
        {
            keysUIYellow[i].SetActive(true);
        }

        for (int o = 2; o >= keyCounter; o--)
        {
            keysUIGray[o].SetActive(true);
        }
    }

    private void CheckWin()
    {
        if (keyCounter == totalKeys)
        {
            // Salir a la pantalla de has ganado
            Debug.Log("¡Has ganado!");

            result.YouWon();

        }
    }
}
