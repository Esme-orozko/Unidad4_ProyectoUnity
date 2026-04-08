using UnityEngine;
using UnityEngine.InputSystem;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPosition;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.localEulerAngles;
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 currentTapPosition = Mouse.current.position.ReadValue();

            if (lastTapPosition == Vector2.zero)
            {
                lastTapPosition = currentTapPosition;
            }

            float distance = lastTapPosition.x - currentTapPosition.x;
            lastTapPosition = currentTapPosition;

            transform.Rotate(Vector3.up * distance);
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            lastTapPosition = Vector2.zero;
        }
    }
}