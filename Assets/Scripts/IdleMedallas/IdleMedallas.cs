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

        // Colocar el jugador en la posici�n correspondiente
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
            SetPlayerPosition();  // Cambiar la posici�n
            UpdateAnimatorTransition();  // Cambiar la transici�n del animator
        }
    }

    // M�todo para actualizar la posici�n del jugador seg�n el puntaje
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
                // Si no hay un puntaje v�lido, colocar en una posici�n por defecto
                PJ.transform.position = posicionNoGana; // Ejemplo de posici�n por defecto
                break;
        }
    }

    // M�todo para actualizar la transici�n del Animator seg�n el puntaje de medalla
    void UpdateAnimatorTransition()
    {
        // Aqu� asumo que tienes un par�metro en tu Animator llamado "Medal"
        animator.SetInteger("Medal", playerScore);

        // Puedes agregar m�s l�gica si necesitas transiciones m�s complejas.
    }
}
