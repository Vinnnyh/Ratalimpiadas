using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingController : MonoBehaviour
{
    public float swimSpeed = 10f;

    private bool canPressW = true;
    private bool canPressS = false;
    private Vector3 moveDirection = Vector3.forward; // Dirección de movimiento inicial
    public RaceManager raceManager;

    void Start()
    {
        // Inicializamos la dirección para que siempre se mueva hacia adelante al comenzar
        moveDirection = transform.forward;
        raceManager.RegisterRacer(transform);
    }

    void Update()
    {
        // Alternativa para teclado con W y S
        if (Input.GetKeyDown(KeyCode.W) && canPressW)
        {
            SwimForward();
            canPressW = false;
            canPressS = true; // Ahora solo S puede avanzar
        }
        else if (Input.GetKeyDown(KeyCode.S) && canPressS)
        {
            SwimForward();
            canPressS = false;
            canPressW = true; // Ahora solo W puede avanzar
        }

        // Alternativa para los joysticks
        if ((Input.GetAxis("AtletismTriggerLeft") < 0f && canPressW) && (Input.GetAxis("AtletismTriggerRight") < 0f && canPressS)) {

        } else {
            if (Input.GetAxis("AtletismTriggerLeft") < 0f && canPressW)
            {
                SwimForward();
                canPressW = false;
                canPressS = true; // Ahora solo el joystick derecho puede avanzar
            }
            else if (Input.GetAxis("AtletismTriggerRight") < 0f && canPressS)
            {
                SwimForward();
                canPressS = false;
                canPressW = true; // Ahora solo el joystick izquierdo puede avanzar
            }
        }
        
    }

    private void SwimForward()
    {
        // Avanzar en la dirección de movimiento
        transform.Translate(moveDirection * swimSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            // Notificar al RaceManager que el jugador/bot llegó a End
            raceManager.OnRacerReachEnd(transform);

            // Cambiar dirección de movimiento
            moveDirection = -moveDirection;
            transform.Rotate(0, 180, 0);
        }
        else if (other.CompareTag("Start"))
        {
            // Notificar al RaceManager que el jugador/bot volvió a Start
            raceManager.OnRacerReachStart(transform);

            // Cambiar dirección de movimiento
            moveDirection = -moveDirection;
            transform.Rotate(0, 180, 0);
        }
    }
}
