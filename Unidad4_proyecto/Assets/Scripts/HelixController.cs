using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HelixController : MonoBehaviour
{
    private Vector2 lastTapPosition;
    private Vector3 startRotation;

    public Transform topTransform;
    public Transform goalTransform;
    public GameObject helicLevelPrefab;
    public List<Stage> allStages = new List<Stage>();
    public float helixDistance;
    private List<GameObject> spawnedLevels = new List<GameObject>();

    private void Awake()
    {
        startRotation = transform.localEulerAngles;
        helixDistance = topTransform.localPosition.y - (goalTransform.localPosition.y + .1f);
        loadStage(0);
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
        Stage stage = allStages[Mathf.Clamp(stageNumber, 0, allStages.Count-1)];
        if (stage == null)
        {
            Debug.Log("No stages");
            return;
        }

        Camera.main.backgroundColor = allStages[stageNumber].stageBackgroundColor;
        FindAnyObjectByType<BallController>().GetComponent<Renderer>().material.color = allStages[stageNumber].stageBallColor;
        transform.localEulerAngles = startRotation;

        foreach (GameObject go in spawnedLevels)
        {
            Destroy(go);
        }

        float levelDistance = helixDistance/stage.levels.Count;
        float spawnPosY = topTransform.localPosition.y;

        for (int i = 0; i < stage.levels.Count; i++)
        {
            spawnPosY-=levelDistance;
            GameObject level = Instantiate(helicLevelPrefab, transform);

            level.transform.localPosition = new Vector3(0, spawnPosY, 0);

            spawnedLevels.Add(level);

            int partsToDisable = 12 - stage.levels[i].partCount;
            List<GameObject> disableParts = new List<GameObject>();

            while (disableParts.Count < partsToDisable)
            {
                GameObject randomPart = level.transform.GetChild(Random.Range(0, level.transform.childCount)).gameObject;

                if (!disableParts.Contains(randomPart))
                {
                    randomPart.SetActive(false);
                    disableParts.Add(randomPart);
                }
            }

            List<GameObject> leftParts = new List<GameObject>();

            foreach (Transform t in level.transform)
            {
                t.GetComponent<Renderer>().material.color = allStages[stageNumber].stageLevelPartColor;

                if (t.gameObject.activeInHierarchy)
                {
                    leftParts.Add(t.gameObject);
                }
            }

            List<GameObject> deathParts = new List<GameObject>();

            while (deathParts.Count < stage.levels[i].deathPartCount)
            {
                GameObject randomPart = leftParts[Random.Range(0, leftParts.Count)];

                if (!deathParts.Contains(randomPart))
                {
                    randomPart.gameObject.AddComponent<DeathPart>();
                    deathParts.Add(randomPart);
                }
            }

        }

    }
}