using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0;  // Puntos del jugador, inicializados en 0
    public int botScore = 0;     // Puntos del bot, inicializados en 0
    public int winningScore = 3; // Puntaje necesario para ganar

    public TextMeshProUGUI scoreText;   // Referencia al marcador de puntos en la UI

    private bool gameEnded = false;     // Verificar si el juego ha terminado

    void Start()
    {
        // Asegúrate de que los puntajes comiencen en 0
        playerScore = 0;
        botScore = 0;
        UpdateScoreUI();  // Actualizar el texto del marcador al inicio del juego
    }

    // Método para que el jugador anote un punto
    public void PlayerScores()
    {
        if (!gameEnded)
        {
            playerScore++;
            CheckForWinner();
            UpdateScoreUI();
        }
    }

    // Método para que el bot anote un punto
    public void BotScores()
    {
        if (!gameEnded)
        {
            botScore++;
            CheckForWinner();
            UpdateScoreUI();
        }
    }

    // Método para verificar si alguien ha ganado
    void CheckForWinner()
    {
        if (playerScore >= winningScore || botScore >= winningScore)
        {
            gameEnded = true;
            // Aquí podrías cambiar de escena o detener el juego
            Debug.Log("¡Juego terminado!");
        }
    }

    // Actualiza el marcador en la UI
    void UpdateScoreUI()
    {
        scoreText.text = playerScore.ToString() + " / " + botScore.ToString();
    }

    // Método para reiniciar el juego
    public void ResetGame()
    {
        playerScore = 0;
        botScore = 0;
        gameEnded = false;
        UpdateScoreUI();
    }
}
