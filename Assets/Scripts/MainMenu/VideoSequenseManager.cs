using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class VideoSequenseManager : MonoBehaviour
{
    public GameObject firstVideoObject;  // Objeto del primer video
    public GameObject secondVideoObject; // Objeto del segundo video
    public Canvas initialCanvas;         // Canvas inicial con el mensaje
    public Canvas secondCanvas;          // Canvas que aparece después de 24 segundos
    public TextMeshProUGUI blinkText;    // Texto con efecto de parpadeo (TextMeshProUGUI)
    public string buttonName = "B-S";    // Nombre del botón en el Input Manager

    private VideoPlayer firstVideoPlayer;
    private VideoPlayer secondVideoPlayer;

    private bool isButtonPressed = false; // Controla si se ha presionado el botón
    private bool fadeOutStarted = false;  // Controla si el fade-out del canvas inicial ha comenzado
    private float timer = 0f;             // Temporizador para el fade-in del segundo canvas
    private bool fadeInStarted = false;  // Controla si el fade-in del segundo canvas ha comenzado

    public float fadeDuration = 2f;       // Duración del fade-out y fade-in
    public float blinkInterval = 0.5f;   // Intervalo del parpadeo del texto

    private void Start()
    {
        // Inicializa los componentes de VideoPlayer
        firstVideoPlayer = firstVideoObject.GetComponent<VideoPlayer>();
        secondVideoPlayer = secondVideoObject.GetComponent<VideoPlayer>();

        // Asegúrate de que los videos estén desactivados inicialmente
        firstVideoObject.SetActive(true);
        secondVideoObject.SetActive(false);
        firstVideoPlayer.Pause();

        // Inicialmente el segundo canvas debe estar invisible
        secondCanvas.GetComponent<CanvasGroup>().alpha = 0f;

        // Comienza el parpadeo del texto
        StartCoroutine(BlinkText());
    }

    private void Update()
    {
        // Si se presiona el botón, inicia el fade-out del canvas y muestra el primer video
        if (Input.GetButtonDown(buttonName) && !isButtonPressed)
        {
            isButtonPressed = true;
            StartCoroutine(FadeOutInitialCanvas());  // Fade-out del canvas inicial
            firstVideoObject.SetActive(true); // Muestra el primer video
            
            firstVideoPlayer.Play();                // Inicia el primer video
        }

        // Si el botón ya fue presionado y han pasado 24 segundos, comienza el fade-in del segundo canvas
        if (isButtonPressed && !fadeInStarted)
        {
            timer += Time.deltaTime;

            if (timer >= 25f)
            {
                fadeInStarted = true;
                StartCoroutine(FadeInSecondCanvas()); // Realiza el fade-in del segundo canvas
            }
        }
    }

    // Método para hacer fade-out del canvas inicial
    private IEnumerator FadeOutInitialCanvas()
    {
        float elapsedTime = 0f;
        CanvasGroup canvasGroup = initialCanvas.GetComponent<CanvasGroup>();

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = 1f - Mathf.Clamp01(elapsedTime / fadeDuration); // Fade-out
            yield return null;
        }

        // Asegura que el canvas inicial esté completamente invisible al final
        canvasGroup.alpha = 0f;
        initialCanvas.gameObject.SetActive(false); // Desactiva el canvas inicial
    }

    // Método para hacer fade-in del segundo canvas
    private IEnumerator FadeInSecondCanvas()
    {
        float elapsedTime = 0f;
        CanvasGroup canvasGroup = secondCanvas.GetComponent<CanvasGroup>();

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration); // Fade-in
            yield return null;
        }

        // Asegura que el canvas esté completamente visible al final
        canvasGroup.alpha = 1f;
        secondCanvas.gameObject.SetActive(true); // Activa el segundo canvas
    }

    // Efecto de parpadeo del texto
    private IEnumerator BlinkText()
    {
        while (true)
        {
            blinkText.enabled = !blinkText.enabled; // Alterna la visibilidad del texto
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
