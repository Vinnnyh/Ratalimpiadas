using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Halterofilia : MonoBehaviour
{
    public Animator animator; // Referencia al componente Animator
    private int keyPressCount = 0; // Contador de pulsaciones de tecla
    private int state = 0; // Estado de animación
    private int level = 1; //Nivel en el que esta
    private int difficulty = 1;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelText;
    private Double remainingTime = 0;

    void Update()
    {   

        if (Mathf.Ceil((float)remainingTime) == 40)
        {
            if (level < 4)
            {
                level++;
                remainingTime = 0;
            } 
            else
            {
                levelText.text = "Ganaste!";
            }
        }

        switch (level)
        {
            case 1:
                difficulty = 3;
                levelText.text = "Nivel: " + level;
                break;
            case 2:
                difficulty = 5;
                levelText.text = "Nivel: " + level;
                break;
            case 3:
                difficulty = 6;
                levelText.text = "Nivel: " + level;
                break;
        }

        pulsaciones(difficulty);

        // Verificar si se presiona la tecla Espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            keyPressCount++;
            remainingTime++;
            Debug.Log("Pulsaciones: " + remainingTime);

            // Cambiar estado basado en el número de pulsaciones
            if (remainingTime >= 30) //Estado de pesa levantada totalmente
            {
                state = 3;
            }
            else if (remainingTime < 30 || remainingTime > 20) //Estado de pesa en el pecho
            {
                state = 2;
            }
            else if (remainingTime >= 20) //Estado de pesa en el pecho
            {
                state = 2;
            }
            else if (remainingTime >= 10) //Estado de pesa recien levantada
            {
                state = 1;
            }

            CambiarAnimacion();
        }
    }

    // Método para cambiar la animación basada en el estado
    void CambiarAnimacion()
    {
        switch (state)
        {
            case 1:
                animator.Play("Pose 1");
                break;
            case 2:
                animator.Play("Pose 2");
                break;
            case 3:
                animator.Play("Pose 3");
                break;
            case 4:
                animator.speed = -1f;
                animator.Play("Pose 1");
                animator.speed = 1f;
                break;
            case 5:
                animator.speed = -1f;
                animator.Play("Pose 2");
                animator.speed = 1f;
                break;
            case 6:
                animator.speed = -1f;
                animator.Play("Pose 3");
                animator.speed = 1f;
                break;
        }
    }

    void pulsaciones(int difficulty)
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime * difficulty;
        }
        else
        {
            remainingTime = 0;
        }
        timerText.text = Mathf.Ceil((float)remainingTime).ToString();
    }
}
