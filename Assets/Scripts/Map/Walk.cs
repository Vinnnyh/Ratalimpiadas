using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkScript : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento del personaje
    public Animator animator;
    public Vector3 movimientoDelante = new Vector3(0f, 0f, -1f);
    public Vector3 movimientoAtras = new Vector3(0f, 0f, 1f);
    public Vector3 movimientoDer = new Vector3(1f, 0f, 0f);
    public Vector3 movimientoIzq = new Vector3(-1f, 0f, 0f);

    private string currentState; // Estado actual del Animator

    const string STATE_FRONT = "State Front";
    const string STATE_BACK = "State Back";
    const string STATE_LEFT = "State Left";
    const string STATE_RIGHT = "State Right";
    const string WALK_FRONT = "Walk Front";
    const string WALK_BACK = "Walk Back";
    const string WALK_LEFT = "Walk Left";
    const string WALK_RIGHT = "Walk Right";

    void Update()
    {
        moverse();
    }

    void moverse()
    {
        if (Input.GetKey(KeyCode.S))
        {
            cambiarAnimacion(STATE_FRONT, WALK_FRONT); // Cambiar animación a caminar hacia atrás
            transform.Translate(movimientoAtras * velocidad * Time.deltaTime);
        }
        // Mover hacia adelante
        else if (Input.GetKey(KeyCode.W))
        {
            cambiarAnimacion(STATE_BACK, WALK_BACK); // Cambiar animación a caminar hacia adelante
            transform.Translate(movimientoDelante * velocidad * Time.deltaTime);
        }
        // Mover hacia la derecha
        else if (Input.GetKey(KeyCode.D))
        {
            cambiarAnimacion(STATE_RIGHT, WALK_RIGHT); // Cambiar animación a caminar hacia la derecha
            transform.Translate(movimientoDer * velocidad * Time.deltaTime);
        }
        // Mover hacia la izquierda
        else if (Input.GetKey(KeyCode.A))
        {
            cambiarAnimacion(STATE_LEFT, WALK_LEFT); // Cambiar animación a caminar hacia la izquierda
            transform.Translate(movimientoIzq * velocidad * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Walk", false); // Detener la animación de caminar cuando no se presionan teclas
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
}
