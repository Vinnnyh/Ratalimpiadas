using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtletismoRaceManager : MonoBehaviour
{
    private List<Transform> racersReachedEnd = new List<Transform>(); // Lista de todos los corredores que llegaron a "End"
    private Dictionary<Transform, bool> racerTypes = new Dictionary<Transform, bool>(); // Identificar si es jugador o bot

    public static string athleticsPlayerMedal = "No Medal";
    private int level;
    private bool playerFinished = false; // Verifica si el jugador ya terminó

    public void RegisterRacer(Transform racer, bool isPlayer)
    {
        racerTypes[racer] = isPlayer; // Registrar si es jugador o bot
        Debug.Log($"Racer {racer.name} registered. IsPlayer: {isPlayer}");
    }

    public void OnAthleticsRacerReachEnd(Transform racer)
    {
        // Verificar si el corredor ya está registrado y si ya llegó
        if (!racerTypes.ContainsKey(racer) || racersReachedEnd.Contains(racer))
        {
            Debug.LogWarning($"Racer {racer.name} is not registered or has already reached the End.");
            return;
        }

        // Agregar el corredor a la lista de finalización
        racersReachedEnd.Add(racer);
        int place = racersReachedEnd.Count - 1; // Posición basada en la lista
        Debug.Log($"{racer.name} reached the End. Position: {place}");

        // Si es el jugador, asignar medalla
        if (racerTypes[racer])
        {
            AssignMedal(place);
            Debug.Log(place);
        }
    }

    private void AssignMedal(int place)
    {
        if (place == 0)
        {
            athleticsPlayerMedal = "Gold";
            Debug.Log("You won a Gold Medal!");
            level = 3;
            guardarMedalla(3);
            SceneManager.LoadScene("MedallasScene");
        }
        else if (place == 1)
        {
            athleticsPlayerMedal = "Silver";
            Debug.Log("You won a Silver Medal!");
            level = 2;
            guardarMedalla(2);
            SceneManager.LoadScene("MedallasScene");
        }
        else if (place == 2)
        {
            athleticsPlayerMedal = "Bronze";
            Debug.Log("You won a Bronze Medal!");
            level = 1;
            guardarMedalla(1);
            SceneManager.LoadScene("MedallasScene");
        }
        else
        {
            athleticsPlayerMedal = "No Medal";
            Debug.Log("You did not win any medal.");
            level = 0;
            guardarMedalla(0);
            SceneManager.LoadScene("MedallasScene");
        }
    }

    void guardarMedalla(int numMedalla)
    {
        int previousMedal = PlayerPrefs.GetInt("MedallaAtletismo", 4);
        Debug.Log(previousMedal);
        PlayerPrefs.SetInt("MedallaTemporal", numMedalla);
        if (previousMedal > level)
        {   
            Debug.Log("Llegamos al if uwu");
            PlayerPrefs.SetInt("MedallaAtletismo", numMedalla);
        }
        PlayerPrefs.Save();
    }
}
