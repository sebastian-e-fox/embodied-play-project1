using UnityEngine;

public class ExclamationFollow : MonoBehaviour
{
    public Transform playerCamera; // Assign the player's camera in the Inspector

    void Update()
    {
        // Make the exclamation always face the player
        transform.LookAt(playerCamera);
        transform.Rotate(0, 180, 0); // Fixes it from facing backward
    }
}