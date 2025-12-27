using UnityEngine;

public class Spinner : MonoBehaviour
{
    void Update()
    {
        // Spins wildly. If Time.timeScale is 0, this WILL stop.
        transform.Rotate(0, 1000 * Time.deltaTime, 0);
    }
}