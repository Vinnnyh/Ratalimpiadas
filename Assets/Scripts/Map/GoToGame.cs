using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToGame : MonoBehaviour
{

    public TextMeshProUGUI message;
    public string text;

    private void Update()
    {
        message.text = "Pulsa E para ingresar a " + text + ".";
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
            message.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene("HalterofiliaScene");
            }
        }
    }
}
