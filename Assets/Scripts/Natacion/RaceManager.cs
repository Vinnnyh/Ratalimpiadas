using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceManager : MonoBehaviour
{
    private List<Transform> racersReachedEnd = new List<Transform>();
    public static string swimmingPlayerMedal = "No Medal";

    public void RegisterRacer(Transform racer)
    {
        Debug.Log($"Racer {racer.name} registered.");
    }

    public void OnRacerReachEnd(Transform racer)
    {
        if (!racersReachedEnd.Contains(racer))
        {
            racersReachedEnd.Add(racer);
            Debug.Log($"Racer {racer.name} reached the End. Position in list: {racersReachedEnd.IndexOf(racer)}");
        }
        else
        {
            Debug.Log($"Racer {racer.name} has already reached the End and is in the list.");
        }
    }

    public void OnRacerReachStart(Transform racer)
    {
        if (racersReachedEnd.Contains(racer))
        {
            int place = racersReachedEnd.IndexOf(racer);
            Debug.Log($"Racer {racer.name} reached the Start. Position in list: {place}");

            if (place == 0)
            {
                swimmingPlayerMedal = "Gold";
                Debug.Log("You won a Gold Medal!");
                SceneManager.LoadScene("MapScene");
                guardarMedalla(3);
            }
            else if (place == 1)
            {
                swimmingPlayerMedal = "Silver";
                Debug.Log("You won a Silver Medal!");
                SceneManager.LoadScene("MapScene");
                guardarMedalla(2);
            }
            else if (place == 2)
            {
                swimmingPlayerMedal = "Bronze";
                Debug.Log("You won a Bronze Medal!");
                SceneManager.LoadScene("MapScene");
                guardarMedalla(1);
            }
            else
            {
                swimmingPlayerMedal = "No Medal";
                Debug.Log("You did not win any medal.");
                SceneManager.LoadScene("MapScene");
                guardarMedalla(0);
            }

            // Remove the racer from the list after they reach Start
            racersReachedEnd.Remove(racer);
        }
        else
        {
            Debug.LogWarning($"Racer {racer.name} reached the Start without having reached the End first.");
        }
    }
    void guardarMedalla(int numMedalla)
    {
        PlayerPrefs.SetInt("MedallaNatacion", numMedalla);
        PlayerPrefs.Save();
    }
}
