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

    private float roundTime = 20f;  // Tiempo por ronda (en segundos)
    private float currentRoundTime;
    private int currentRound = 1;
    private int totalRounds = 3;  
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
            StartCoroutine(RoundTimer());
        }
    }

    private IEnumerator StartGameAfterIntro()
    {
        // Espera hasta que la animación de introducción termine
        yield return new WaitUntil(() => introAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hidden"));

        isPaused = false;
        messageText.gameObject.SetActive(false);
        StartCoroutine(RoundTimer());
    }

    private IEnumerator RoundTimer()
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

        EndRound();
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

    private void EndRound()
    {
        if (currentRound < totalRounds)
        {
            StartCoroutine(ShowRoundCompleteMessage());
        }
        else
        {
            EndGame();
        }
    }

    private IEnumerator ShowRoundCompleteMessage()
    {
        isPaused = true;
        FreezeGame(true);

        // Muestra el mensaje de fin de ronda
        messageText.text = "Ronda " + currentRound + " completada";
        messageText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);

        messageText.gameObject.SetActive(false);
        currentRound++;

        // Reiniciar solo el temporizador y las posiciones de los objetos
        currentRoundTime = roundTime;
        ResetRoundPositions();

        isPaused = false;
        FreezeGame(false);
        StartCoroutine(RoundTimer());
    }

    public void ResetRoundPositions()
    {
        ball.ResetBall();  // Resetear la posición de la pelota
        // Aquí puedes agregar código para resetear también la posición inicial del jugador y el bot.
    }

    private void EndGame()
    {
        messageText.text = "Juego Completado";
        SceneManager.LoadScene("MapScene");
        messageText.gameObject.SetActive(true);
        // Aquí puedes agregar la lógica final del juego.
    }
}
