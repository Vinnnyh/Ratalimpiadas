using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreDisplayController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // Texto que mostrará el puntaje

    void Start()
    {
        // Obtener el puntaje actual desde PlayerPrefs
        int playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        int botScore = PlayerPrefs.GetInt("BotScore", 0);

        // Mostrar el puntaje actual en el formato "Jugador / Bot"
        scoreText.text = playerScore.ToString() + " / " + botScore.ToString();

        // Iniciar la corutina para volver a la escena del Ping-Pong después de 1 segundo
        StartCoroutine(ReturnToPingPong());
    }

    IEnumerator ReturnToPingPong()
    {
        yield return new WaitForSeconds(1f);  // Esperar 1 segundo
        SceneManager.LoadScene("PingPongScene");  // Volver a la escena del Ping-Pong
    }
}
