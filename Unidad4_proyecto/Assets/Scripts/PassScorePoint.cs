using UnityEngine;

public class PassScorePoint : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        GameManager.singleton.AddScore(1);
        BallController[] balls = FindObjectsByType<BallController>();
        balls[0].perfectPass++;
    }
}
