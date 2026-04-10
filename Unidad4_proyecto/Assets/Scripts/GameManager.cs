using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int bestScore;
    public int currentScore;
    public int currentLevel = 0;
    public static GameManager singleton;
    void Awake()
    {
        if ( singleton == null)
        {
            singleton = this;
        }
        else if ( singleton != this)
        {
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt("HighScore");
    }

public void NextLevel()
{
    currentLevel++;
    HelixController helix = FindObjectsByType<HelixController>()[0];

    // Verificar si ya no hay más niveles
    if (currentLevel >= helix.allStages.Count)
    {
        SceneManager.LoadScene("MainMenu"); 
        return;
    }

    // Continuar con el siguiente nivel
    FindObjectsByType<BallController>()[0].ResetBall();
    FindObjectsByType<HelixController>()[0].loadStage(currentLevel);
}
    public void RestartLevel()
    {
        singleton.currentScore = 0;
        FindObjectsByType<BallController>()[0].ResetBall();
        FindObjectsByType<HelixController>()[0].loadStage(currentLevel);
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;

        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            PlayerPrefs.SetInt("HighScore", currentScore);

        }
    }


}
