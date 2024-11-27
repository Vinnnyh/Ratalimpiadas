using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PingPongGameController : MonoBehaviour
{   
    public int playerScore = 0; 
    public int botScore = 0;     
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI timerText;   
    public TextMeshProUGUI messageText;
    public Ball ball;  
    public Player_Movement player;
    public Bot bot; 

    private int level;

    private float roundTime = 30f;  // Tiempo total del juego (30 segundos)
    private float currentRoundTime;
    private bool isPaused = true;
    public Animator introAnimator;  

    private void Start()
    {
        currentRoundTime = roundTime;
        UpdateScoreUI();
        UpdateTimerUI();
        messageText.gameObject.SetActive(false);
        
        if (introAnimator != null)
        {
            // Reproduce la animación de introducción y espera su final
            introAnimator.Play("IntroAnimation");
            StartCoroutine(StartGameAfterIntro());
        }
        else
        {
            isPaused = false;
            StartCoroutine(GameTimer());
        }
    }

    private IEnumerator StartGameAfterIntro()
    {
        // Espera hasta que la animación de introducción termine
        yield return new WaitUntil(() => introAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hidden"));

        isPaused = false;
        messageText.gameObject.SetActive(false);
        StartCoroutine(GameTimer());
    }

    private IEnumerator GameTimer()
    {
        while (currentRoundTime > 0)
        {
            if (!isPaused)
            {
                currentRoundTime -= Time.deltaTime;
                UpdateTimerUI();
            }
            yield return null;
        }

        // Cuando se acaben los 30 segundos, termina el juego
        EndGame();
    }

    public void PlayerScores()
    {
        playerScore++;
        UpdateScoreUI();
        StartCoroutine(ShowMessage("GREAT", 2));
    }

    public void BotScores()
    {
        botScore++;
        UpdateScoreUI();
        StartCoroutine(ShowMessage("OH NO", 2));
    }

    private void UpdateScoreUI()
    {
        scoreText.text = playerScore.ToString() + " - " + botScore.ToString();
    }

    private void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(currentRoundTime / 60f);
        int seconds = Mathf.FloorToInt(currentRoundTime % 60f);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private IEnumerator ShowMessage(string message, int countdown)
    {
        isPaused = true;
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        FreezeGame(true);  // Congelar el juego

        yield return new WaitForSeconds(1);

        // Contador regresivo 3, 2, 1
        while (countdown > 0)
        {
            messageText.text = countdown.ToString();
            yield return new WaitForSeconds(1);
            countdown--;
        }

        messageText.gameObject.SetActive(false);
        isPaused = false;

        FreezeGame(false);  // Descongelar el juego

        ResetRoundPositions(); // Reiniciar solo las posiciones de los objetos sin alterar puntaje o tiempo
    }

    private void FreezeGame(bool freeze)
    {
        if (ball != null && ball.GetComponent<Rigidbody>() != null)
        {
            ball.GetComponent<Rigidbody>().isKinematic = freeze;
        }

        if (player != null)
        {
            player.enabled = !freeze;
        }

        if (bot != null)
        {
            bot.enabled = !freeze;
        }
    }

    public void ResetRoundPositions()
    {
        ball.ResetBall();  // Resetear la posición de la pelota
        // Aquí puedes agregar código para resetear también la posición inicial del jugador y el bot.
    }

    private void EndGame()
    {
        // Evaluar el resultado al final del juego
        if (currentRoundTime > 0)
        {
            Debug.Log("El tiempo no ha terminado, no se evaluará la medalla aún.");
            return;
        }

        if (playerScore > botScore && playerScore > 4)
        {
            // El jugador gana oro
            PlayerPrefs.SetString("PingPongPlayerMedal", "Gold");
            Debug.Log("You won a Gold Medal!");
            level = 3;
            guardarMedalla(3);
            SceneManager.LoadScene("MedallasScene");
        }
        else if (botScore > playerScore && (botScore - playerScore) < 2)
        {
            // El jugador pierde, pero la diferencia de puntaje es menor que 2 - Plata
            PlayerPrefs.SetString("PingPongPlayerMedal", "Silver");
            Debug.Log("You won a Silver Medal!");
            level = 2;
            guardarMedalla(2);
            SceneManager.LoadScene("MedallasScene");
        }
        else if (playerScore >= 2)
        {
            // El jugador anotó al menos 2 puntos - Bronce
            PlayerPrefs.SetString("PingPongPlayerMedal", "Bronze");
            Debug.Log("You won a Bronze Medal!");
            level = 1;
            guardarMedalla(1);
            SceneManager.LoadScene("MedallasScene");
        }
        else
        {
            // El jugador no anotó los puntos mínimos - Sin medalla
            PlayerPrefs.SetString("PingPongPlayerMedal", "No Medal");
            Debug.Log("You did not win any medal.");
            level = 0;
            guardarMedalla(0);
            SceneManager.LoadScene("MedallasScene");
        }
    }

    void guardarMedalla(int numMedalla)
    {
        int playerScore = PlayerPrefs.GetInt("MedallaPingPong", 4);

        Debug.Log(playerScore);

        PlayerPrefs.SetInt("MedallaTemporal", numMedalla);
        if (playerScore < level)
        {
            PlayerPrefs.SetInt("MedallaPingPong", numMedalla);
        }
        PlayerPrefs.Save();
    }
    
}
