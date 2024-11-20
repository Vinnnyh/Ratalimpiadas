using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGame : MonoBehaviour
{

    public TextMeshProUGUI message;
    public string text;
    public string scene;

    private bool playerInTrigger = false;  // Para saber si el jugador está dentro del trigger

    private void Update()
    {
        // Verifica si el jugador está dentro del trigger y presiona el botón de interacción
        if (playerInTrigger && Input.GetButtonDown("InteractMap"))
        {
            SceneManager.LoadScene(scene);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            message.text = "Pulsa E para ingresar a " + text + ".";
            message.gameObject.SetActive(true);
            playerInTrigger = true;  // Marca que el jugador está dentro del trigger
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            message.gameObject.SetActive(false);
            playerInTrigger = false;  // Marca que el jugador ha salido del trigger
        }
    }
}
