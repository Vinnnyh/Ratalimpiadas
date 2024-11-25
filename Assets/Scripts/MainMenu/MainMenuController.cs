using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public void PlayGame()
    {
        // Redirigir directamente a Ping-Pong (reemplazar "PingPongScene" con "MapScene" cuando esté listo)
        SceneManager.LoadScene("MapScene");
        GuardarPosicionObjeto();
    }

    public void QuitGame()
    {
        // Cerrar el juego
        Application.Quit();
    }

    // Método para guardar la posición del objeto
    void GuardarPosicionObjeto()
    {
        // Guardar cada componente de la posición por separado
        PlayerPrefs.SetFloat("PosicionX", 0f);
        PlayerPrefs.SetFloat("PosicionY", 0.7f);
        PlayerPrefs.SetFloat("PosicionZ", 0f);

        // Confirmar que se guardó correctamente
        PlayerPrefs.Save();
    }
}
