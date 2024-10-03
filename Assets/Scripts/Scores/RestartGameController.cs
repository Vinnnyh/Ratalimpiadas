using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RestartGameController : MonoBehaviour
{
    public TextMeshProUGUI countdownText;  // Texto del contador en la UI
    public float countdownTime = 4f;       // Tiempo del contador (4 segundos para "Listos, 3, 2, 1")

    public GameObject[] uiElementsToHide;  // Lista de los elementos del Canvas a ocultar

    private string gameSceneName;

    // Método para iniciar el reinicio con cuenta regresiva
    public void StartGameWithCountdown()
    {
        // Recuperar el nombre del modo de juego actual desde PlayerPrefs
        gameSceneName = PlayerPrefs.GetString("CurrentGameMode", "DefaultGameScene");

        // Si se encuentra el modo de juego, iniciar la cuenta regresiva
        if (!string.IsNullOrEmpty(gameSceneName) && gameSceneName != "DefaultGameScene")
        {
            // Ocultar los elementos de la UI antes de empezar el contador
            HideUIElements();

            // Iniciar la cuenta regresiva
            StartCoroutine(CountdownRoutine());
        }
        else
        {
            Debug.LogError("No se ha encontrado el modo de juego actual en PlayerPrefs.");
        }
    }

    // Corrutina para manejar el contador y redirigir al juego
    IEnumerator CountdownRoutine()
    {
        countdownText.gameObject.SetActive(true);

        // Mostrar "Listos" al inicio
        countdownText.text = "Listos";
        yield return new WaitForSeconds(1f);

        // Contar "3, 2, 1"
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "";  // Limpiar el texto al final
        countdownText.gameObject.SetActive(false);

        // Cargar la escena del modo de juego anterior
        SceneManager.LoadScene(gameSceneName);
    }

    // Método para ocultar los elementos de la UI
    private void HideUIElements()
    {
        foreach (GameObject element in uiElementsToHide)
        {
            element.SetActive(false);  // Ocultar cada elemento
        }
    }
}
