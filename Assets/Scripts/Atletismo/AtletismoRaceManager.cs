using System.Collections.Generic;
using UnityEngine;

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
                        break;
                    case 2:
                        athleticsPlayerMedal = "Silver";
                        Debug.Log("Player won the Silver Medal!");
                        break;
                    case 3:
                        athleticsPlayerMedal = "Bronze";
                        Debug.Log("Player won the Bronze Medal!");
                        break;
                    default:
                        athleticsPlayerMedal = "No Medal";
                        Debug.Log("Player did not win a medal.");
                        break;
                }
            }
        }
        else
        {
            Debug.Log($"Racer {racer.name} has already reached the End.");
        }
    }
}
