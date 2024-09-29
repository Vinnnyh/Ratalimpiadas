using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector3 Initial_position;
    Rigidbody rb;

    public int playerBounces = 0;  // Rebotes en la mesa del jugador
    public int botBounces = 0;     // Rebotes en la mesa del bot

    void Start()
    {
        Initial_position = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    public void ResetBall()
    {
        rb.velocity = Vector3.zero;
        transform.position = Initial_position;
        playerBounces = 0;
        botBounces = 0;
        Debug.Log("Ball reset. Bounces reset.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Detectar colisiones con la mesa del jugador
        if (collision.transform.CompareTag("PlayerTable"))
        {
            playerBounces++;
            Debug.Log("Rebote en la mesa del jugador: " + playerBounces + " rebote(s)");
        }

        // Detectar colisiones con la mesa del bot
        if (collision.transform.CompareTag("BotTable"))
        {
            botBounces++;
            Debug.Log("Rebote en la mesa del bot: " + botBounces + " rebote(s)");
        }

        // Si la pelota toca un "Wall", reinicia la pelota sin otorgar el punto
        if (collision.transform.CompareTag("Wall"))
        {
            ResetBall();
        }
    }

    public bool HasBouncedAtLeastOnce()
    {
        return (playerBounces > 0 || botBounces > 0);  // Verifica que haya rebotado en cualquier mesa
    }
}
