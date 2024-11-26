using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public string buttonName = "B-S";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Si se presiona el botón, inicia el fade-out del canvas y muestra el primer video
        if (Input.GetButtonDown(buttonName))
        {
            SceneManager.LoadScene("MapScene");
        }
    }
}
