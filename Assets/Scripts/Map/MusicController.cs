using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource; // Asigna el AudioSource desde el Inspector

    void Start()
    {
        // Recuperar el tiempo guardado en PlayerPrefs
        float savedTime = PlayerPrefs.GetFloat("VideoTime", 0f);

        // Si hay un tiempo guardado, configurar el audio para empezar desde ahí
        if (savedTime > 0f && savedTime < audioSource.clip.length)
        {
            audioSource.time = savedTime;
        }

        // Iniciar la reproducción
        audioSource.Play();
    }
}
