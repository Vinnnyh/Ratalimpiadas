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
    public Animator animator;
    public Animator animatorTxtEstado;
    public Animator buttonAnimator; // Nuevo: animator para los botones
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI txtEstado;
    public Slider loadPulsaciones;
    public Slider loadBarraTiempo;

    private int keyPressCount = 0;
    private int level = 1;
    private int level1 = 1;
    private int level2 = 1;
    private int difficulty = 1;
    private double remainingTime = 0;
    private bool isPlaying = true;
    private float TimeLimit = 30f;
    private float tiempo = 3f;

    // Modificado: Ahora usamos integers para los botones para coincidir con los parámetros del animator
    private int[] buttonValues = { 1, 2, 3, 4 }; // 1=A, 2=B, 3=Y, 4=X
    private string[] buttonNames = { "A-A", "B-S", "Y-D", "X-W" };
    private string currentButton;
    private int currentButtonValue;
    private float buttonChangeTimer;
    private float nextButtonChangeTime;

    void Start()
    {
        SetRandomButton();
        SetNextButtonChangeTime();
    }

    void Update()
    {
        int pausaMenu = PlayerPrefs.GetInt("MenuPausa", 0);

        if (pausaMenu == 0)
        {
            if (!isPlaying)
            {
                buttonChangeTimer += Time.deltaTime;
                if (buttonChangeTimer >= nextButtonChangeTime)
                {
                    SetRandomButton();
                    SetNextButtonChangeTime();
                    buttonChangeTimer = 0;
                }
            }

            controlMensajesPulsaciones();
            controlNiveles();
            controlBar();
            controlBarraTiempo();
        }
    }

    private void SetRandomButton()
    {
        string previousButton = currentButton;
        int previousValue = currentButtonValue;
        int randomIndex;

        do
        {
            randomIndex = UnityEngine.Random.Range(0, buttonNames.Length);
            currentButton = buttonNames[randomIndex];
            currentButtonValue = buttonValues[randomIndex];
        } while (currentButton == previousButton);

        // Activar la animación correspondiente
        buttonAnimator.SetInteger("ButtonState", currentButtonValue);
        Debug.Log("Nuevo botón: " + currentButton);
    }

    private void SetNextButtonChangeTime()
    {
        nextButtonChangeTime = UnityEngine.Random.Range(8f, 11f);
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
            SceneManager.LoadScene("MedallasScene");
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
                TimeLimit -= Time.deltaTime * (3 / 2);
            }
            else if (TimeLimit < Porciento2)
            {
                TimeLimit -= Time.deltaTime * (float)(0.5);
            }
            else
            {
                TimeLimit -= Time.deltaTime;
            }

            if (Input.GetButtonDown(currentButton))
            {
                keyPressCount++;
                remainingTime++;
                // Opcional: Puedes agregar una animación de "presionado correcto"
                //buttonAnimator.SetTrigger("Pressed");
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
                guardarMedalla(0);
                break;
            case 2:
                difficulty = 5;
                levelText.text = "Nivel: " + level;
                txtEstado.text = "Round " + level;
                guardarMedalla(1);
                break;
            case 3:
                difficulty = 6;
                levelText.text = "Nivel: " + level;
                txtEstado.text = "Round " + level;
                guardarMedalla(2);
                break;
            case 4:
                levelText.text = "Ganaste!";
                txtEstado.text = "GANASTE!";
                guardarMedalla(3);
                SceneManager.LoadScene("MedallasScene");
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

    void guardarMedalla(int numMedalla)
    {
        int playerScore = PlayerPrefs.GetInt("MedallaHalterofilia", 4);

        PlayerPrefs.SetInt("MedallaTemporal", numMedalla);
        if (playerScore < level)
        {
            PlayerPrefs.SetInt("MedallaHalterofilia", numMedalla);
        }
        PlayerPrefs.Save();
    }
}
