using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    private List<Transform> racersReachedStart = new List<Transform>();
    private Dictionary<Transform, bool> racerTypes = new Dictionary<Transform, bool>();
    public static string pingPongPlayerMedal = "No Medal";
    private int level;

    public void RegisterRacer(Transform racer, bool isPlayer)
    {
        racerTypes[racer] = isPlayer; // Registrar si es jugador o bot
        Debug.Log($"Racer {racer.name} registered. IsPlayer: {isPlayer}");
    }

    public void OnRacerReachEnd(Transform racer)
    {
        Debug.Log($"{racer.name} reached the End.");
    }

    public void OnRacerReachStart(Transform racer)
    {
        if (!racerTypes.ContainsKey(racer)) return;

        // Si ya llegó a "Start", no volver a procesar
        if (racersReachedStart.Contains(racer))
        {
            Debug.LogWarning($"{racer.name} already reached the Start.");
            return;
        }

        racersReachedStart.Add(racer);
        int place = racersReachedStart.Count - 1;

        Debug.Log($"{racer.name} reached the Start. Position: {place}");

        if (racerTypes[racer]) // Si es el jugador
        {
            AssignMedalToPlayer(place);
        }
    }

    private void AssignMedalToPlayer(int place)
    {
        if (place == 0)
        {
            pingPongPlayerMedal = "Gold";
            Debug.Log("You won a Gold Medal!");
            level = 3;
            guardarMedalla(3);
        }
        else if (place == 1)
        {
            pingPongPlayerMedal = "Silver";
            Debug.Log("You won a Silver Medal!");
            level = 2;
            guardarMedalla(2);
        }
        else if (place == 2)
        {
            pingPongPlayerMedal = "Bronze";
            Debug.Log("You won a Bronze Medal!");
            level = 1;
            guardarMedalla(1);
        }
        else
        {
            pingPongPlayerMedal = "No Medal";
            Debug.Log("You did not win any medal.");
            level = 0;
            guardarMedalla(0);
        }

        SceneManager.LoadScene("MedallasScene");
    }

    void guardarMedalla(int numMedalla)
    {
        int previousMedal = PlayerPrefs.GetInt("MedallaNatacion", 4);

        PlayerPrefs.SetInt("MedallaTemporal", numMedalla);
        if (previousMedal < level)
        {
            PlayerPrefs.SetInt("MedallaNatacion", numMedalla);
        }
        PlayerPrefs.Save();
    }
}
