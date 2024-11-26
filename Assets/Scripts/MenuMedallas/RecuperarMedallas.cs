using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecuperarMedallas : MonoBehaviour
{
    private int valueDefault = 4;
    private int playerScoreHalterofilia;
    private int playerScorePingPong;
    private int playerScoreAtletismo;
    private int playerScoreNatacion;

    // Start is called before the first frame update
    void Start()
    {
        playerScoreHalterofilia = PlayerPrefs.GetInt("MedallaHalterofilia", valueDefault);
        playerScorePingPong = PlayerPrefs.GetInt("MedallaPingPong", valueDefault);
        playerScoreAtletismo = PlayerPrefs.GetInt("MedallaAtletismo", valueDefault);
        playerScoreNatacion = PlayerPrefs.GetInt("MedallaNatacion", valueDefault);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerScore);
    }
}
