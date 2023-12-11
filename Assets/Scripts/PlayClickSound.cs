using UnityEngine;

//HANDLES BUTTON CLICK SOUND

public class PlayClickSound : MonoBehaviour
{
    // AUDIO
    [SerializeField] AudioSource buttonSound;

    public void Play()
    {
        buttonSound.Play();
    }
}
