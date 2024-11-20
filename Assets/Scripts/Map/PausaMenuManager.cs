using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PausaMenuManager : MonoBehaviour
{
    public static PausaMenuManager instance; // Declaraci�n de la instancia
    public GameObject menuPausaUI; // El Canvas que contiene todo el men� de pausa
    public Button btn_salir; // El bot�n que deseas mostrar u ocultar
    GameObject personaje; // Referencia al objeto que contiene el script Walk
    public GameObject buttonToSelect; // El bot�n que deseas mantener seleccionado
    private Walk walk; // Referencia al script Walk
    private bool isPaused = false; // Variable para controlar el estado de pausa

    void Awake()
    {
        // Asegurarse de que solo haya un men� de pausa (Singleton)
        if (instance == null)
        {
            instance = this; // Asigna la instancia actual
            DontDestroyOnLoad(gameObject); // No destruir este objeto y sus hijos
        }
        else
        {
            Destroy(gameObject); // Destruir duplicados si ya hay un men�
        }
    }

    void Start()
    {
        // Aseg�rate de que el bot�n seleccionado est� en el inicio
        if (buttonToSelect != null)
        {
            EventSystem.current.SetSelectedGameObject(buttonToSelect);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Suscribirse al evento de carga de escena
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Cancelar la suscripci�n al evento
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verificar si estamos en "MapScene"
        if (scene.name == "MapScene")
        {
            btn_salir.gameObject.SetActive(false); // Desactivar el bot�n si estamos en "MapScene"
        }
        else
        {
            btn_salir.gameObject.SetActive(true); // Activar el bot�n si estamos en otra escena
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("PauseMap"))
        {
            if (isPaused)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }

        // Si el juego est� en pausa, evita el control del jugador
        if (isPaused)
        {
            return; // Salir del m�todo Update para prevenir m�s entradas
        }

        // Aseg�rate de que el bot�n seleccionado est� siempre seleccionado
        if (buttonToSelect != null && EventSystem.current.currentSelectedGameObject != buttonToSelect)
        {
            EventSystem.current.SetSelectedGameObject(buttonToSelect);
        }
    }

    public void Reanudar()
    {
        menuPausaUI.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false; // Actualizar el estado a no pausado

        personaje = GameObject.FindWithTag("Player");
        walk = personaje.GetComponent<Walk>();
        if (walk != null)
        {
            walk.enabled = true; // Activa el script Walk
        }
    }

    void Pausar()
    {
        menuPausaUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        isPaused = true; // Actualizar el estado a pausado

        personaje = GameObject.FindWithTag("Player");
        walk = personaje.GetComponent<Walk>();
        if (walk != null)
        {
            walk.enabled = false; // Desactiva el script Walk
        }
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void Salir()
    {
        SceneManager.LoadScene("MapScene");
        Reanudar();
    }
}