using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtletismoRaceManager : MonoBehaviour
{
    private List<Transform> racersReachedEnd = new List<Transform>();
    public static string athleticsPlayerMedal = "No Medal";

    public void RegisterAthleticsRacer(Transform racer)
    {
        Debug.Log($"Racer {racer.name} registered for athletics.");
    }

    public void OnAthleticsRacerReachEnd(Transform racer)
    {
        if (!racersReachedEnd.Contains(racer))
        {
            racersReachedEnd.Add(racer);
            Debug.Log($"Racer {racer.name} reached the End. Position in list: {racersReachedEnd.Count}");

            // Asignar medallas
            int position = racersReachedEnd.Count;
            if (racer.CompareTag("Player"))
            {
                switch (position)
                {
                    case 1:
                        athleticsPlayerMedal = "Gold";
                        Debug.Log("Player won the Gold Medal!");
                        SceneManager.LoadScene("MapScene");
                        guardarMedalla(3);
                        break;
                    case 2:
                        athleticsPlayerMedal = "Silver";
                        Debug.Log("Player won the Silver Medal!");
                        SceneManager.LoadScene("MapScene");
                        guardarMedalla(2);
                        break;
                    case 3:
                        athleticsPlayerMedal = "Bronze";
                        Debug.Log("Player won the Bronze Medal!");
                        SceneManager.LoadScene("MapScene");
                        guardarMedalla(1);
                        break;
                    default:
                        athleticsPlayerMedal = "No Medal";
                        Debug.Log("Player did not win a medal.");
                        SceneManager.LoadScene("MapScene");
                        guardarMedalla(0);
                        break;
                }
            }
        }
        else
        {
            Debug.Log($"Racer {racer.name} has already reached the End.");
        }
    }
    void guardarMedalla(int numMedalla)
    {
        PlayerPrefs.SetInt("MedallaAtletismo", numMedalla);
        PlayerPrefs.Save();
    }
}
