using UnityEngine;

public class HitStop : MonoBehaviour
{
    public static HitStop Instance;

    private float _stopDurationCurrent;
    private bool _isStopped;

    void Awake()
    {
        // Singleton Pattern: If one already exists, destroy this new one.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Freeze(float duration)
    {
        // Start the freeze
        _stopDurationCurrent = duration;
        _isStopped = true;

        // Force Time to 0 immediately
        Time.timeScale = 0f;
        Debug.Log("HitStop: Time Frozen!");
    }

    void Update()
    {
        // If we aren't stopped, do nothing.
        if (!_isStopped) return;

        // Count down using unscaled time (real world seconds)
        if (_stopDurationCurrent > 0)
        {
            _stopDurationCurrent -= Time.unscaledDeltaTime;
        }
        else
        {
            // Timer finished!
            _isStopped = false;
            Time.timeScale = 1f;
            Debug.Log("HitStop: Time Resumed.");
        }
    }
}