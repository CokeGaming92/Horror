using UnityEngine;

public class PlayerTranslateToPosition : MonoBehaviour
{
    public GameObject targetPosition, movable;
 

    

    public void MovePlayer()
    {
      

        // Move the player towards the target position
        movable.transform.position = targetPosition.transform.position;

      
    }
}