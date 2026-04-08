using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPosition;
    private Vector3 startPosition;

    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helicLevelPrefab;
    public List<Stage> allStage = new List<Stage>();
    public float helixDistance;
    private List<GameObject> spawnedLevels = new List<GameObject>();

    private void Awake()
    {
        startPosition = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + .1f);
        //loadStage(0);
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

    public void loadStage (int stageNumber)
    {
        
    }
}