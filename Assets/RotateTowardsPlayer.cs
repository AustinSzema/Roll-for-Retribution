using UnityEngine;

public class RotateTowardsPlayer : MonoBehaviour
{
    void Update()
    {
        Vector3 targetPosition = GameManager.Instance.playerPosition;

        // Flatten both vectors to only care about X and Z (ignore height difference)
        Vector3 direction = targetPosition - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.001f)
            return; // Don't rotate if direction is too small

        // Rotate only on Y axis
        float singleStep = 100f * Time.deltaTime;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, singleStep, 0.0f);

        // Flatten the new direction again to ensure it stays horizontal
        newDirection.y = 0f;

        // Apply the rotation
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}