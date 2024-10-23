using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Halterofilia : MonoBehaviour
{
    public Animator animator; // Referencia al componente Animator
    private int keyPressCount = 0; // Contador de pulsaciones de tecla
    private int level = 1; //Nivel en el que esta
    private int difficulty = 1;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI timeLimitText;
    private Double remainingTime = 0;
    private float TimeLimit = 60f;

    void Update()
    {
        TimeLimit -= Time.deltaTime;
        timeLimitText.text = "Tiempo: " + Mathf.Ceil((float)TimeLimit).ToString();

        if (Mathf.Ceil((float)remainingTime) == 40)
        {
            if (level <= 3)
            {
                level++;
                remainingTime = 0;
                TimeLimit = 60;
            }
        } 
        else if(Mathf.Ceil((float)TimeLimit) == 0)
        {
            timeLimitText.text = "Perdiste!";
            Debug.Log("Perdiste!");
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
            case 4:
                levelText.text = "Ganaste!";
                break;
        }

        pulsaciones(difficulty);

        animator.SetInteger("Counter", (int)remainingTime);

        // Verificar si se presiona la tecla Espacio
        if (Input.GetButtonDown("Jump"))
        {
            keyPressCount++;
            remainingTime++;
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
