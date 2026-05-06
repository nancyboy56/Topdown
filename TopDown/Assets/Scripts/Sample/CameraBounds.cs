using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;

    [Header("Bounds")]
    // The minimum X and Y position the camera is allowed to reach
    [SerializeField] Vector2 minBounds;
    // The maximum X and Y position the camera is allowed to reach
    [SerializeField] Vector2 maxBounds;

    void LateUpdate()
    {
        // Calculate where we WANT the camera to be (player's position + offset)
        Vector3 targetPosition = player.position + offset;

        // Clamp stops the value from going below min or above max
        // e.g. Mathf.Clamp(15, 0, 10) would return 10
        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

        // Apply the clamped position to the camera
        // We use LateUpdate (instead of Update) so this runs AFTER the player has moved
        transform.position = targetPosition;
    }
}
