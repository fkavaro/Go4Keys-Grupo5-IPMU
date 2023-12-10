using UnityEngine;

//RESETS POSITION OF CHASER OF TARGET

public class ChaserResetter : MonoBehaviour
{
    //Persecution players
    [SerializeField] Transform target;
    [SerializeField] Transform chaser;

    //Distance
    [SerializeField] float safeDist = 10.0f;

    //Sounds
    [SerializeField] AudioSource policeSound;

    private void OnTriggerEnter(Collider other)
    {
        //Is triggered by target
        if (other.transform.CompareTag(target.transform.tag))
        { 
            ResetChaserPos();
        }
    }

    //Resets chaser position to a safe distance behind target
    private void ResetChaserPos()
    {
        chaser.localPosition = new Vector3(target.localPosition.x, chaser.localPosition.y, target.localPosition.z - safeDist);

        policeSound.Play();//Calls police

        Debug.Log("Position of chaser reset");
    }
}

////////////////////////////////////////////////////////////////////////////
///PSEUDOCODE
///
/// before first frame (start):
/// 
/// every frame (update):
///     check collision with the target
///         if so, move chaser behind target at certain distance
/// 
