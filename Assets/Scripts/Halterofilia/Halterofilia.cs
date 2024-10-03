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

        animator.SetInteger("Counter", (int)remainingTime);
        //isReversed = animator.GetBool("Reverse");
        

        // Verificar si se presiona la tecla Espacio
        if (Input.GetKeyDown(KeyCode.Space))
        {
            keyPressCount++;
            remainingTime++;
            Debug.Log("Pulsaciones: " + remainingTime);
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
