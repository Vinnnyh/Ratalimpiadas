using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Halterofilia : MonoBehaviour
{
    public Animator animator; // Referencia al componente Animator
    public Animator animatorTxtEstado; // Referencia al componente Animator de la animacion del mensaje
    private int keyPressCount = 0; // Contador de pulsaciones de tecla
    private int level = 1; //Nivel en el que esta
    private int difficulty = 1;

    public TextMeshProUGUI levelText;
    public TextMeshProUGUI txtEstado;
    private Double remainingTime = 0;
    private bool isPlaying = true;
    private float TimeLimit = 30f;
    private float tiempo = 3f;

    private int level1 = 1;
    private int level2 = 1;

    public Slider loadPulsaciones;
    public Slider loadBarraTiempo;

    void Update()
    {
        
        controlMensajesPulsaciones();
        controlNiveles();
        controlBar();
        controlBarraTiempo();
    }

    public void controlBar()
    {
        loadPulsaciones.value = (float)remainingTime;
    }

    public void controlBarraTiempo()
    {
        loadBarraTiempo.value = (float)TimeLimit;
    }
   
    void controlMensajesPulsaciones()
    {
        setTxtEstado(isPlaying);

        if (Mathf.Ceil((float)TimeLimit) > -1.0f)
        {
            //timeLimitText.text = "Tiempo: " + Mathf.Ceil((float)TimeLimit).ToString();
        }

        if (Mathf.Ceil((float)remainingTime) == 40)
        {
            if (level <= 3)
            {
                level++;
                level2 = level;
                remainingTime = 0;
                TimeLimit = 30;
                loadBarraTiempo.value = TimeLimit;
            }
        }
        else if (Mathf.Ceil((float)TimeLimit) == 0)
        {
            txtEstado.text = "Perdiste!";
            SceneManager.LoadScene("MapScene");
        }

        if (level1 != level2)
        {
            isPlaying = true;
        }
        else if (isPlaying)
        {
            tiempo -= Time.deltaTime;

            if (tiempo < 0)
            {
                tiempo = 3f;
                isPlaying = false;
            }
        }
        else
        {
            float TimeLimit2 = 60f;
            float Porciento1 = (TimeLimit2 * 60) / 100;
            float Porciento2 = (TimeLimit2 * 30) / 100;
            
            if (TimeLimit < Porciento1 && TimeLimit > Porciento2)
            {
                TimeLimit -= Time.deltaTime * (3/2);
            }
            else if (TimeLimit < Porciento2)
            {
                TimeLimit -= Time.deltaTime * (float)(0.5);
            }
            else
            {
                TimeLimit -= Time.deltaTime;
            }

            // Verificar si se presiona la tecla Espacio
            if (Input.GetButtonDown("AHalterofilia"))
            {
                keyPressCount++;
                remainingTime++;
            }
        }

        level1 = level;
    }

    void controlNiveles()
    {
        switch (level)
        {
            case 1:
                difficulty = 4;
                levelText.text = "Nivel: " + level;
                txtEstado.text = "Round " + level;
                break;
            case 2:
                difficulty = 5;
                levelText.text = "Nivel: " + level;
                txtEstado.text = "Round " + level;
                break;
            case 3:
                difficulty = 6;
                levelText.text = "Nivel: " + level;
                txtEstado.text = "Round " + level;
                break;
            case 4:
                levelText.text = "Ganaste!";
                txtEstado.text = "GANASTE!";
                SceneManager.LoadScene("MapScene");
                break;
        }

        pulsaciones(difficulty);

        animator.SetInteger("Counter", (int)remainingTime);
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
    }

    void setTxtEstado(bool isPlay)
    {
        if (isPlay)
        {
            animatorTxtEstado.SetBool("isPlaying", true);
        }
        else
        {
            animatorTxtEstado.SetBool("isPlaying", false);
        }
    }
}
