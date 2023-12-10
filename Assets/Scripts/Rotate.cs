using UnityEngine;

//MAKES AUTOMATIC ROTATION

public class Rotate : MonoBehaviour
{
    [SerializeField] float speedX;
    [SerializeField] float speedY;
    [SerializeField] float speedZ;

    // Update is called once per frame
    void Update()
    {
        //360 degrees * speed * time independent from framerate
        //speed = 1 is a whole rotation
        transform.Rotate(360 * speedX * Time.deltaTime, 360 * speedY * Time.deltaTime, 360 * speedZ * Time.deltaTime);
    }
}
