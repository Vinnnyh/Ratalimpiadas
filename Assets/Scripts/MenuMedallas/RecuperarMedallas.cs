using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecuperarMedallas : MonoBehaviour
{
    private int valueDefault = 4;
    private int playerScore;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = PlayerPrefs.GetInt("MedallaHalterofilia", valueDefault);
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerScore);
    }
}
