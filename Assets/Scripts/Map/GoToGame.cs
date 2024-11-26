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
    public AudioSource audioSource;
    public GameObject PJ;

    private bool playerInTrigger = false;  // Para saber si el jugador está dentro del trigger

    private void Update()
    {
        // Verifica si el jugador está dentro del trigger y presiona el botón de interacción
        if (playerInTrigger && Input.GetButtonDown("InteractMap"))
        {
            GuardarPosicionObjeto();
            SceneManager.LoadScene(scene);
        }

        // Guardar el tiempo actual del video en PlayerPrefs
        PlayerPrefs.SetFloat("VideoTime", (float)audioSource.time);
        PlayerPrefs.Save();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            message.text = "Pulsa Y para ingresar a " + text + ".";
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

    // Método para guardar la posición del objeto
    void GuardarPosicionObjeto()
    {
        Vector3 posicion = PJ.transform.position;

        // Guardar cada componente de la posición por separado
        PlayerPrefs.SetFloat("PosicionX", posicion.x);
        PlayerPrefs.SetFloat("PosicionY", posicion.y);
        PlayerPrefs.SetFloat("PosicionZ", posicion.z);

        // Confirmar que se guardó correctamente
        PlayerPrefs.Save();
        Debug.Log("Posición guardada: " + posicion);
    }
}
