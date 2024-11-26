using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medallas : MonoBehaviour
{
    // Referencias a los SpriteRenderers de las medallas
    public SpriteRenderer MedallaHalterofilia;
    public SpriteRenderer MedallaPingPong;
    public SpriteRenderer MedallaAtletismo;
    public SpriteRenderer MedallaNatacion;

    private int valueDefault = 4;
    private int playerScore;
    private int ScoreHalterofilia;
    private int ScorePingPong;
    private int ScoreAtletismo;
    private int ScoreNatacion;

    // Los sprites de medallas
    public Sprite bronce;
    public Sprite plata;
    public Sprite oro;

    void Start()
    {
        // Cargar y mostrar las medallas para cada mini-juego
        ScoreHalterofilia = PlayerPrefs.GetInt("MedallaHalterofilia", valueDefault);
        ScorePingPong = PlayerPrefs.GetInt("MedallaPingPong", valueDefault);
        ScoreAtletismo = PlayerPrefs.GetInt("MedallaAtletismo", valueDefault);
        ScoreNatacion = PlayerPrefs.GetInt("MedallaNatacion", valueDefault);
    }

    void Update()
    {
        MostrarMedallas();
    }

    void MostrarMedallas()
    {
        MostrarMedalla(MedallaHalterofilia, ScoreHalterofilia);

        MostrarMedalla(MedallaPingPong, ScorePingPong);

        MostrarMedalla(MedallaAtletismo, ScoreAtletismo);

        MostrarMedalla(MedallaNatacion, ScoreNatacion);
    }

    // Método para mostrar la medalla según el valor
    void MostrarMedalla(SpriteRenderer medallaRenderer, int medalla)
    {
        switch (medalla)
        {
            case 0: // Sin medalla
                medallaRenderer.sprite = null; // No mostramos ninguna medalla
                break;
            case 1: // Bronce
                medallaRenderer.sprite = bronce;
                break;
            case 2: // Plata
                medallaRenderer.sprite = plata;
                break;
            case 3: // Oro
                medallaRenderer.sprite = oro;
                break;
        }
    }
}
