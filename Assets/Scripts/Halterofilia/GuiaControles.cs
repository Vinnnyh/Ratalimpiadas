using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuiaControles : MonoBehaviour
{
    public string buttonName = "B-S";
    public GameObject GuiaControl;
    int guiaControl;

    private bool isPaused = true; // Variable para controlar el estado de pausa

    // Nuevas variables
    public List<GameObject> additionalCanvasObjects; // Lista de Canvas adicionales

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("GuiaControl", 1);
        int YaEjecuto = PlayerPrefs.GetInt("YaEjecuto", 1);
        PlayerPrefs.Save();

        GuiaControl.SetActive(true);
        foreach (var canvas in additionalCanvasObjects)
        {
            canvas.SetActive(false);
        }
        Pausar();
    }

    // Update is called once per frame
    void Update()
    {
        guiaControl = PlayerPrefs.GetInt("GuiaControl", 1);
        if (guiaControl == 1)
        {
            // Si se presiona el botón, desactiva el canvas principal y los adicionales
            if (Input.GetButtonDown(buttonName) || guiaControl == 0)
            {
                PlayerPrefs.SetInt("GuiaControl", 0);
                PlayerPrefs.SetInt("YaEjecuto", 1);
                PlayerPrefs.Save();
                GuiaControl.SetActive(false);
                foreach (var canvas in additionalCanvasObjects)
                {
                    canvas.SetActive(true);
                }
                Reanudar();
            }
        }
    }

    public void Reanudar()
    {
        PlayerPrefs.SetInt("MenuPausa", 0);
        PlayerPrefs.Save();

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false; // Actualizar el estado a no pausado
    }

    void Pausar()
    {
        PlayerPrefs.SetInt("MenuPausa", 1);
        PlayerPrefs.Save();

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true; // Actualizar el estado a pausado
    }
}
