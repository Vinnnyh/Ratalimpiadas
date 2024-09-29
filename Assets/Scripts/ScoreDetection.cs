using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDetection : MonoBehaviour
{
    public ScoreManager scoreManager;  // Referencia al ScoreManager
    public Ball ball;  // Referencia a la pelota (para verificar los rebotes)
    private bool pointAwarded = false; // Bandera para evitar que se sumen puntos múltiples veces

    private void Start()
    {
        // Asignar automáticamente la pelota si no está asignada en el Inspector
        if (ball == null)
        {
            ball = FindObjectOfType<Ball>();

            if (ball == null)
            {
                Debug.LogError("¡No se encontró el objeto Ball!");
            }
        }
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
                    Debug.Log("Jugador anota! Rebote en la mesa del bot");
                    scoreManager.PlayerScores();  // El jugador anota si rebotó en la mesa del bot
                }
                else
                {
                    Debug.Log("Bot gana por falta de rebote en la mesa del bot");
                    scoreManager.BotScores();  // El bot gana si no rebotó en la mesa del bot
                }
            }
            // Si estamos en la zona de puntuación del bot (BotScorePlus)
            else if (gameObject.CompareTag("BotScorePlus"))
            {
                if (ball.playerBounces > 0)  // Si rebotó en la mesa del jugador
                {
                    Debug.Log("Bot anota! Rebote en la mesa del jugador");
                    scoreManager.BotScores();  // El bot anota si rebotó en la mesa del jugador
                }
                else
                {
                    Debug.Log("Jugador gana por falta de rebote en la mesa del jugador");
                    scoreManager.PlayerScores();  // El jugador gana si no rebotó en la mesa del jugador
                }
            }

            StartCoroutine(ResetPointAwarded());  // Resetear la bandera después de un tiempo
        }
    }

    private IEnumerator ResetPointAwarded()
    {
        yield return new WaitForSeconds(2);  // Espera 2 segundos
        pointAwarded = false;  // Permitir que se otorguen puntos nuevamente
    }
}

