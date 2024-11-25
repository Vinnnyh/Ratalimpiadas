using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Asigna el VideoPlayer desde el Inspector

    void Update()
    {
        if (videoPlayer.isPlaying)
        {
            // Guardar el tiempo actual del video en PlayerPrefs
            PlayerPrefs.SetFloat("VideoTime", (float)videoPlayer.time);
            PlayerPrefs.Save();
        }
    }

    void OnApplicationQuit()
    {
        // Asegurarte de guardar el tiempo si el juego se cierra
        PlayerPrefs.SetFloat("VideoTime", (float)videoPlayer.time);
        PlayerPrefs.Save();
    }
}
