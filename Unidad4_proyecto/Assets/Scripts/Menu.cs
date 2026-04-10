using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void EmpezarJuego ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SalirJuego ()
    {
        Application.Quit();
        Debug.Log("Salir del Juego");
    }
}
