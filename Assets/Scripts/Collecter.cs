using UnityEngine;

//HANDLES COLLECTION OF KEYS

public class Collecter : MonoBehaviour
{
    [SerializeField] Transform keysInScene;//Contains all the keys in scene
    [SerializeField] Transform keysLeftUI;//Contains all left keys icons in UI
    [SerializeField] Transform keysFoundUI;//Contains all found keys icons in UI
    private GameObject[] keysLeftArray;//Array of gray keys in UI
    private GameObject[] keysFoundArray;//Array of golden keys in UI

    private int keyCounter = -1;//-1 as in arrays
    private int keysLeft;

    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] Result result;

    // AUDIO
    [SerializeField] AudioSource collectionSound;

    // Start is called before the first frame update
    void Start()
    {
        //Counts all keys left in scene
        keysLeft = keysInScene.childCount;

        //Creates arrays
        keysLeftArray = new GameObject[keysLeftUI.childCount];
        keysFoundArray = new GameObject[keysFoundUI.childCount];

        //Fills array of keys left icons and activates them
        for (int i = 0; i < keysLeftArray.Length; i++)
        {
            keysLeftArray[i] = keysLeftUI.GetChild(i).gameObject;
            keysLeftArray[i].SetActive(true);
        }
        //Fills array of keys found icons and deactivates them
        for (int i = 0; i < keysFoundArray.Length; i++)
        {
            keysFoundArray[i] = keysFoundUI.GetChild(i).gameObject;
            keysFoundArray[i].SetActive(false);
        }
    }

    //Collision trigger
    private void OnTriggerEnter(Collider other)
    {
        //With a key object
        if (other.gameObject.CompareTag("Key"))
        {
            //Destroys it
            Destroy(other.gameObject);

            keyCounter++;//Adds one key
            keysLeft--;//One key left less

            UpdateUI();//Changes left icon for found one
            CheckWin();

            // Audio
            collectionSound.Play(); // key clin clin!
        }
    }

    //Swaps key icons in UI (keyCounter > -1)
    private void UpdateUI()
    {
        keysLeftArray[keyCounter].SetActive(false);
        keysFoundArray[keyCounter].SetActive(true);
    }

    private void CheckWin()
    {
        //No keys left
        if (keysLeft <= 0)
        {
            pauseMenu.EndResult();//End game UI
            result.Victory();//Result
        }
    }
}
