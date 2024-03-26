using UnityEngine;
using UnityEngine.Events;

public class TimerDestroyObject : MonoBehaviour
{
    public float destroyTime = 3.0f; // Set the time after which the GameObject will be destroyed
    public UnityEvent timerDestroy;
    void Start()
    {
        // Invoke the DestroyObject method after the specified destroyTime
        Invoke("DestroyObject", destroyTime);
    }

    void DestroyObject()
    {
       timerDestroy.Invoke();
    }
}