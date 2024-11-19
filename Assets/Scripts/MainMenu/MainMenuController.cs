using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Redirigir directamente a Ping-Pong (reemplazar "PingPongScene" con "MapScene" cuando esté listo)
        SceneManager.LoadScene("MapScene");
    }

    public void QuitGame()
    {
        // Cerrar el juego
        Application.Quit();
    }
}
