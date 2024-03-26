using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private UnityEvent unityEvent;

    public void Start()
    {
        
    }
    public void Interact()
    {
        unityEvent.Invoke();
        // Implement interaction logic here
        Debug.Log("Interacted with " + gameObject.name);
    }
}