using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NextGameController : MonoBehaviour
{
    public TextMeshProUGUI countdownText;  // Texto del contador en la UI
    public float countdownTime = 4f;       // Tiempo del contador (4 segundos para "Listos, 3, 2, 1")

    public GameObject[] uiElementsToHide;  // Lista de los elementos del Canvas a ocultar

    private string nextGameSceneName;

    void Start()
    {
        // Obtener el siguiente juego dinámicamente de PlayerPrefs
        nextGameSceneName = PlayerPrefs.GetString("NextGameScene", "MainMenuScene");  // Default: redirige al menú si no hay un siguiente juego
    }

    // Método para iniciar la cuenta regresiva y cargar el siguiente juego
    public void StartNextGameWithCountdown()
    {
        // Ocultar los elementos de la UI antes de empezar el contador
        HideUIElements();

        // Iniciar la cuenta regresiva
        StartCoroutine(CountdownRoutine());
    }

    // Corrutina para manejar el contador y redirigir al siguiente juego
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

        // Cargar la escena del siguiente juego o el menú si no está definido
        SceneManager.LoadScene(nextGameSceneName);
    }

    // Método para ocultar los elementos de la UI
    private void HideUIElements()
    {
        if (uiElementsToHide != null)  // Verificar que el arreglo no sea nulo
        {
            foreach (GameObject element in uiElementsToHide)
            {
                if (element != null)  // Verificar que cada elemento esté asignado
                {
                    element.SetActive(false);  // Ocultar cada elemento
                }
            }
        }
        else
        {
            Debug.LogWarning("uiElementsToHide no tiene elementos asignados.");
        }
    }
}
