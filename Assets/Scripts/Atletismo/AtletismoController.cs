using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtletismoController : MonoBehaviour
{
    public float runSpeed = 10f; // Velocidad al correr
    private bool canPressLeft = true; // Controla si se puede usar el gatillo izquierdo o la tecla A
    private bool canPressRight = false; // Controla si se puede usar el gatillo derecho o la tecla D
    public AtletismoRaceManager raceManager;

    void Start()
    {
        raceManager.RegisterAthleticsRacer(transform);
    }

    void Update()
    {
        // Movimiento con teclado
        if (Input.GetKeyDown(KeyCode.A) && canPressLeft) // Detectar la tecla A
        {
            MoveForward();
            canPressLeft = false;
            canPressRight = true; // Ahora solo D o RT puede avanzar
        }
        else if (Input.GetKeyDown(KeyCode.D) && canPressRight) // Detectar la tecla D
        {
            MoveForward();
            canPressRight = false;
            canPressLeft = true; // Ahora solo A o LT puede avanzar
        }

        // Movimiento con gatillos
        if (Input.GetAxis("AtletismTriggerLeft") > 0.5f && canPressLeft) // Detectar gatillo izquierdo
        {
            MoveForward();
            canPressLeft = false;
            canPressRight = true; // Ahora solo RT o D puede avanzar
        }
        else if (Input.GetAxis("AtletismTriggerRight") > 0.5f && canPressRight) // Detectar gatillo derecho
        {
            MoveForward();
            canPressRight = false;
            canPressLeft = true; // Ahora solo LT o A puede avanzar
        }
    }

    private void MoveForward()
    {
        // Mueve al jugador hacia adelante en el eje X
        transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
        Debug.Log("Corriendo. canPressLeft = " + canPressLeft + ", canPressRight = " + canPressRight);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            Debug.Log("Has llegado al final. Carrera terminada.");
            raceManager.OnAthleticsRacerReachEnd(transform);
        }
    }
}
