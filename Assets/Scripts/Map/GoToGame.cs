using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGame : MonoBehaviour
{

    public TextMeshProUGUI message;
    public string text;
    public string scene;

    private void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            message.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            message.text = "Pulsa E para ingresar a " + text + ".";
            message.gameObject.SetActive(true);
            if (Input.GetButtonDown("Interact"))
            {
                SceneManager.LoadScene(scene);
            }
        }
    }
}
