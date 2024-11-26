using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleMedallas : MonoBehaviour
{
    public GameObject PJ;  // El GameObject del jugador (PJ)
    public Animator animator;  // Referencia al Animator

    private int valueDefault = 4;
    private int playerScore;
    private Vector3 posicionOro = new Vector3(0.39f, 0.15f, 2.9f);
    private Vector3 posicionPlata = new Vector3(-5.05f, -0.88f, 2.9f);
    private Vector3 posicionBronce = new Vector3(5.87f, -2.09f, 2.9f);
    private Vector3 posicionNoGana = new Vector3(9.4f, -3.78f, 2.9f);

    // Puedes agregar un valor para las transiciones de medallas
    private int CounterMedals;

    // Start is called before the first frame update
    void Start()
    {
        // Cargar el valor del puntaje de medalla desde PlayerPrefs
        playerScore = PlayerPrefs.GetInt("MedallaTemporal", valueDefault);

        // Inicializar el contador de medallas
        CounterMedals = playerScore;

        // Colocar el jugador en la posición correspondiente
        SetPlayerPosition();
        UpdateAnimatorTransition();
    }

    // Update is called once per frame
    void Update()
    {
        // Comprobar si el puntaje del jugador ha cambiado (si el contador de medallas cambia)
        if (playerScore != CounterMedals)
        {
            CounterMedals = playerScore;  // Actualizar el contador
            SetPlayerPosition();  // Cambiar la posición
            UpdateAnimatorTransition();  // Cambiar la transición del animator
        }
    }

    // Método para actualizar la posición del jugador según el puntaje
    void SetPlayerPosition()
    {
        switch (playerScore)
        {
            case 1:
                PJ.transform.position = posicionBronce;
                break;
            case 2:
                PJ.transform.position = posicionPlata;
                break;
            case 3:
                PJ.transform.position = posicionOro;
                break;
            default:
                // Si no hay un puntaje válido, colocar en una posición por defecto
                PJ.transform.position = posicionNoGana; // Ejemplo de posición por defecto
                break;
        }
    }

    // Método para actualizar la transición del Animator según el puntaje de medalla
    void UpdateAnimatorTransition()
    {
        // Aquí asumo que tienes un parámetro en tu Animator llamado "Medal"
        animator.SetInteger("Medal", playerScore);

        // Puedes agregar más lógica si necesitas transiciones más complejas.
    }
}
