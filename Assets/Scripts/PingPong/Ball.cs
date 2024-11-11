using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 initialPosition;
    private Rigidbody rb;

    public int playerBounces = 0;  // Rebotes en la mesa del jugador
    public int botBounces = 0;     // Rebotes en la mesa del bot

    void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Método para resetear la posición de la pelota y los contadores de rebotes
    public void ResetBall()
    {
        rb.velocity = Vector3.zero;  // Detener la pelota
        transform.position = initialPosition;  // Reiniciar la posición de la pelota
        playerBounces = 0;
        botBounces = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detectar colisiones con la mesa del jugador
        if (collision.transform.CompareTag("PlayerTable"))
        {
            playerBounces++;
        }

        // Detectar colisiones con la mesa del bot
        if (collision.transform.CompareTag("BotTable"))
        {
            botBounces++;
        }

        // Si la pelota toca un "Wall", reinicia la pelota sin otorgar el punto
        if (collision.transform.CompareTag("Wall"))
        {
            ResetBall();
        }
    }

    // Método para verificar si la pelota ha rebotado al menos una vez en cualquier mesa
    public bool HasBouncedAtLeastOnce()
    {
        return (playerBounces > 0 || botBounces > 0);  // Verifica que haya rebotado en cualquier mesa
    }
}
