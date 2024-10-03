using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;  // Para manejar el cambio de escena

public class ScoreManager : MonoBehaviour
{
    public int playerScore = 0;  // Puntos del jugador
    public int botScore = 0;     // Puntos del bot
    public int winningScore = 3; // Puntaje necesario para ganar

    public TextMeshProUGUI scoreText;   // Referencia al marcador de puntos en la UI

    private bool gameEnded = false;     // Verificar si el juego ha terminado

    void Start()
    {
        PlayerPrefs.SetString("CurrentGameMode", "PingPongScene");
        PlayerPrefs.Save();  // Asegurar que el dato se guarde

        // Cargar el puntaje guardado desde PlayerPrefs (si existe)
        playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        botScore = PlayerPrefs.GetInt("BotScore", 0);

        UpdateScoreUI();  // Actualizar el texto del marcador al inicio del juego
    }

    // Método para que el jugador anote un punto
    public void PlayerScores()
    {
        if (!gameEnded)
        {
            playerScore++;
            SaveScore();  // Guardar el puntaje en PlayerPrefs
            UpdateScoreUI();

            ShowScoreScene();  // Cambiar a la escena de puntaje
            CheckForWinner();  // Verificar si el juego ha terminado
        }
    }

    // Método para que el bot anote un punto
    public void BotScores()
    {
        if (!gameEnded)
        {
            botScore++;
            SaveScore();  // Guardar el puntaje en PlayerPrefs
            UpdateScoreUI();

            ShowScoreScene();  // Cambiar a la escena de puntaje
            CheckForWinner();  // Verificar si el juego ha terminado
        }
    }

    // Método para verificar si alguien ha ganado
    void CheckForWinner()
    {
        if (playerScore >= winningScore || botScore >= winningScore)
        {
            gameEnded = true;
            DetermineMedal();  // Determinar la medalla para el jugador
        }
    }

    // Determina qué medalla obtiene el jugador y carga la escena correspondiente
    void DetermineMedal()
    {
        // Limpiar el puntaje al determinar el resultado
        ClearScore();

        // Guardar el siguiente juego en PlayerPrefs (WeightliftingScene)
        PlayerPrefs.SetString("NextGameScene", "halterofilia_escene");
        PlayerPrefs.Save();

        if (playerScore == 0)
        {
            // El jugador no marcó puntos, mostrar la escena de derrota
            SceneManager.LoadScene("PlayerDefeatScene");
        }
        else if (playerScore == 1)
        {
            // El jugador marcó 1 punto, gana bronce
            SceneManager.LoadScene("PlayerBronzeScene");
        }
        else if (playerScore == 2)
        {
            // El jugador marcó 2 puntos, gana plata
            SceneManager.LoadScene("PlayerSilverScene");
        }
        else if (playerScore == 3)
        {
            // El jugador gana con 3 puntos, medalla de oro
            SceneManager.LoadScene("PlayerGoldScene");
        }
    }

    // Método para mostrar la escena del puntaje después de que alguien anote
    private void ShowScoreScene()
    {
        SceneManager.LoadScene("ScoreDisplayScene");  // Cambiar a la escena de puntaje
    }

    // Actualiza el marcador en la UI
    void UpdateScoreUI()
    {
        scoreText.text = playerScore.ToString() + " / " + botScore.ToString();
    }

    // Método para guardar el puntaje actual en PlayerPrefs
    private void SaveScore()
    {
        PlayerPrefs.SetInt("PlayerScore", playerScore);
        PlayerPrefs.SetInt("BotScore", botScore);
        PlayerPrefs.Save();  // Asegurar que los datos se guarden
    }

    // Método para limpiar el puntaje almacenado en PlayerPrefs
    private void ClearScore()
    {
        PlayerPrefs.DeleteKey("PlayerScore");
        PlayerPrefs.DeleteKey("BotScore");
        PlayerPrefs.Save();
    }

    // Método para reiniciar el juego (si es necesario)
    public void ResetGame()
    {
        playerScore = 0;
        botScore = 0;
        gameEnded = false;
        ClearScore();  // Limpiar el puntaje guardado en PlayerPrefs
        UpdateScoreUI();
    }
}
