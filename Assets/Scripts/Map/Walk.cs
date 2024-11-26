using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento del personaje
    public Animator animator;
    public Vector3 movimientoDelante = new Vector3(0f, 0f, -1f);
    public Vector3 movimientoAtras = new Vector3(0f, 0f, 1f);
    public Vector3 movimientoDer = new Vector3(1f, 0f, 0f);
    public Vector3 movimientoIzq = new Vector3(-1f, 0f, 0f);
    public Vector3 posicionInicial = new Vector3(0f, 0.7f, 0f);

    private string currentState; // Estado actual del Animator

    public GameObject PJ;

    const string STATE_FRONT = "State Front";
    const string STATE_BACK = "State Back";
    const string STATE_LEFT = "State Left";
    const string STATE_RIGHT = "State Right";
    const string WALK_FRONT = "Walk Front";
    const string WALK_BACK = "Walk Back";
    const string WALK_LEFT = "Walk Left";
    const string WALK_RIGHT = "Walk Right";

    float movimientoHorizontal;
    float movimientoVertical;

    void Start()
    {
        CargarPosicionObjeto();
    }

    void Update()
    {
        // Capturar los ejes de movimiento (joystick o teclas) en cada frame
        movimientoHorizontal = Input.GetAxis("HorizontalMap");
        movimientoVertical = Input.GetAxis("VerticalMap");

        // Capturar input del teclado
        if (Input.GetKey(KeyCode.A)) movimientoHorizontal = -1;
        if (Input.GetKey(KeyCode.D)) movimientoHorizontal = 1;
        if (Input.GetKey(KeyCode.W)) movimientoVertical = -1;
        if (Input.GetKey(KeyCode.S)) movimientoVertical = 1;

        moverse();
    }

    void moverse()
    {
        Vector3 movimiento = Vector3.zero;
        bool isMoving = false;

        // Priorizar movimiento horizontal
        if (Mathf.Abs(movimientoHorizontal) >= 0.2f)
        {
            if (movimientoHorizontal > 0)
            {
                movimiento = movimientoDer;
                cambiarAnimacion(STATE_RIGHT, WALK_RIGHT);
            }
            else
            {
                movimiento = movimientoIzq;
                cambiarAnimacion(STATE_LEFT, WALK_LEFT);
            }
            isMoving = true;
        }
        // Si no hay movimiento horizontal, considerar el vertical
        else if (Mathf.Abs(movimientoVertical) >= 0.2f)
        {
            if (movimientoVertical < 0)
            {
                movimiento = movimientoAtras;
                cambiarAnimacion(STATE_BACK, WALK_BACK);
            }
            else
            {
                movimiento = movimientoDelante;
                cambiarAnimacion(STATE_FRONT, WALK_FRONT);
            }
            isMoving = true;
        }

        // Aplicar el movimiento
        if (isMoving)
        {
            transform.Translate(movimiento * velocidad * Time.deltaTime);
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }


    void cambiarAnimacion(string nuevoEstado, string estadoCaminando)
    {
        // Si el estado actual es distinto del nuevo, cambiar el estado
        if (currentState != nuevoEstado)
        {
            animator.Play(nuevoEstado); // Cambia directamente al estado nuevo
            animator.SetBool("Walk", true); // Activar el movimiento

            // Actualizar el estado actual
            currentState = nuevoEstado;
        }
    }

    // Método para cargar la posición guardada
    void CargarPosicionObjeto()
    {
        // Verificar si las posiciones existen en PlayerPrefs
        if (PlayerPrefs.HasKey("PosicionX") && PlayerPrefs.HasKey("PosicionY") && PlayerPrefs.HasKey("PosicionZ"))
        {
            // Recuperar cada componente de la posición
            float x = PlayerPrefs.GetFloat("PosicionX");
            float y = PlayerPrefs.GetFloat("PosicionY");
            float z = PlayerPrefs.GetFloat("PosicionZ");

            // Crear el nuevo Vector3 con las coordenadas recuperadas
            Vector3 posicion = new Vector3(x, y, z);

            // Asignar la posición al objeto
            PJ.transform.position = posicion;

            // Confirmar que se cargó correctamente
            Debug.Log("Posición cargada: " + posicion);
        }
        else
        {
            // Si no existe la posición guardada, asignar una posición por defecto
            Debug.Log("No se encontró una posición guardada. Asignando posición por defecto.");
            PJ.transform.position = posicionInicial;
        }
    }
}
