using UnityEngine;
using UnityEngine.Events;

public class LimitedInteractable : MonoBehaviour
{
    public KeyCode interactKeyCode;
    public int maxInteractions = 4; // Maximum number of interactions allowed
    private int interactionCount = 0; // Counter for interactions
    public UnityEvent interactEvent;
    public UnityEvent interactEvent2;
    public UnityEvent unityEvent;
    public UnityEvent unityEvent2;

    void Update()
    {
        if (Input.GetKeyDown(interactKeyCode))
        {
            interactionCount++;

            // Check if the interaction count has exceeded the maximum
            if (interactionCount > maxInteractions)
            {
                // Trigger the limit exceeded event
                interactEvent2.Invoke();
                return; // Exit the Update method early
            }

            // Invoke the interact event for normal interactions
            interactEvent.Invoke();

            // Invoke the unity event on key press
            unityEvent.Invoke();
        }

        if (Input.GetKeyUp(interactKeyCode))
        {
            // Invoke the unity event on key release
            unityEvent2.Invoke();
        }
    }
}