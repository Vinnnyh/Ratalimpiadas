using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDetection : MonoBehaviour
{
    public PingPongGameController gameController;  // Referencia al controlador principal del juego
    public Ball ball;  // Referencia a la pelota
    public TextMeshProUGUI messageText;  // Texto para mostrar mensajes de "GREAT" o "OH NO"
    private bool pointAwarded = false; // Bandera para evitar que se sumen puntos múltiples veces

    private void Start()
    {
        // Asignar automáticamente la pelota y el controlador si no están asignados en el Inspector
        if (ball == null)
        {
            ball = FindObjectOfType<Ball>();
            if (ball == null)
            {
                Debug.LogError("¡No se encontró el objeto Ball!");
            }
        }
        if (gameController == null)
        {
            gameController = FindObjectOfType<PingPongGameController>();
        }

        // Inicializar el mensaje y ocultarlo al inicio
        messageText.text = "";
        messageText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en la zona es la pelota y no se ha otorgado ya un punto
        if (other.CompareTag("Ball") && !pointAwarded)
        {
            pointAwarded = true;  // Marcar que ya se otorgó el punto

            // Si estamos en la zona de puntuación del jugador (PlayerScorePlus)
            if (gameObject.CompareTag("PlayerScorePlus"))
            {
                if (ball.botBounces > 0)  // Si rebotó en la mesa del bot
                {
                    gameController.PlayerScores();  // El jugador anota si rebotó en la mesa del bot
                    ShowMessage("GREAT");  // Mostrar mensaje de éxito para el jugador
                }
                else
                {
                    gameController.BotScores();  // El bot gana si no rebotó en la mesa del bot
                    ShowMessage("OH NO");  // Mostrar mensaje de fallo para el jugador
                }
            }
            // Si estamos en la zona de puntuación del bot (BotScorePlus)
            else if (gameObject.CompareTag("BotScorePlus"))
            {
                if (ball.playerBounces > 0)  // Si rebotó en la mesa del jugador
                {
                    gameController.BotScores();  // El bot anota si rebotó en la mesa del jugador
                    ShowMessage("OH NO");  // Mostrar mensaje de fallo para el jugador
                }
                else
                {
                    gameController.PlayerScores();  // El jugador gana si no rebotó en la mesa del jugador
                    ShowMessage("GREAT");  // Mostrar mensaje de éxito para el jugador
                }
            }

            // Iniciar la cuenta regresiva para reiniciar la ronda después de mostrar el mensaje
            StartCoroutine(CountdownAndReset());
        }
    }

    private void ShowMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);
    }

    private IEnumerator CountdownAndReset()
    {
        yield return new WaitForSeconds(1);  // Mostrar "GREAT" o "OH NO" por un segundo

        // Iniciar cuenta regresiva: 3, 2, 1
        for (int i = 3; i > 0; i--)
        {
            messageText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        // Ocultar el mensaje de cuenta regresiva y reiniciar la ronda
        messageText.gameObject.SetActive(false);
        pointAwarded = false;  // Permitir que se otorguen puntos nuevamente
        gameController.ResetRoundPositions();  // Llamar a ResetRoundPositions en el controlador principal
    }
}
