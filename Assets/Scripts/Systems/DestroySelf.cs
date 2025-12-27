using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float lifetime = 2f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}