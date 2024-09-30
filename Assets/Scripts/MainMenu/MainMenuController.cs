using System.Collections;  // Necesario para IEnumerator
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // Para TextMeshProUGUI
using UnityEngine.UI;  // Para usar botones y texto de UI

public class MainMenuController : MonoBehaviour
{
    public TextMeshProUGUI countdownText;  // Referencia al texto del contador
    public GameObject playButton;  // Referencia al botón de jugar
    public GameObject quitButton;  // Referencia al botón de salir
    
    private void Start()
    {
        // Al iniciar la escena, ocultar el contador
        countdownText.gameObject.SetActive(false);
    }

    // Método para iniciar el juego
    public void PlayGame()
    {
        // Ocultar los botones cuando se presiona "Jugar"
        playButton.SetActive(false);
        quitButton.SetActive(false);

        // Mostrar el contador
        countdownText.gameObject.SetActive(true);

        // Iniciar la corutina del contador
        StartCoroutine(CountdownToStart());
    }

    // Corutina que muestra el texto "Listos", luego el contador regresivo y cambia de escena
    IEnumerator CountdownToStart()
    {
        // Mostrar "Listos"
        countdownText.text = "Listos";
        yield return new WaitForSeconds(1f);  // Esperar 1 segundo

        // Contador regresivo de 3 a 1
        int countdown = 3;
        while (countdown > 0)
        {
            // Mostrar el valor del contador en pantalla
            countdownText.text = countdown.ToString();
            
            // Esperar 1 segundo
            yield return new WaitForSeconds(1f);

            // Disminuir el contador
            countdown--;
        }

        // Cuando el contador llegue a 0, cambiar la escena
        SceneManager.LoadScene("PingPongScene");
    }

    // Método para salir del juego
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();  // Cierra la aplicación
    }
}
