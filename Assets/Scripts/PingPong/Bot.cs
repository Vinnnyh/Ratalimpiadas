using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public float speed = 10f;
    public float force = 6f;
    public float hitHeight = 4f; 
    public Transform ball;
    public Transform aimTarget;
    public Transform[] targets;

    private Animator animator;
    Vector3 targetPosition;
    float reactionTime = 0.018f;
    float lastMoveTime = 0f;

    void Start()
    {
        targetPosition = transform.position;

        // Obtener el Animator del objeto o de un objeto hijo
        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("No se encontró el Animator en el objeto o en sus hijos.");
        }
    }

    void Update()
    {
        // Control del movimiento con el tiempo de reacción
        if (Time.time > lastMoveTime + reactionTime)
        {
            Move();
            lastMoveTime = Time.time;
        }
    }

    void Move()
    {
        // Seguir la pelota en el eje X
        targetPosition.x = ball.position.x;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    Vector3 PickTarget()
    {
        int randomValue = Random.Range(0, targets.Length);
        return targets[randomValue].position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // Activar la animación de golpe
            if (animator != null)
            {
                animator.ResetTrigger("Hit");  // Reiniciar el trigger en caso de que ya esté activado
                animator.SetTrigger("Hit");    // Activar el trigger para la animación de golpe
            }

            // Calcular la dirección de golpe y aplicar fuerza
            Vector3 dir = PickTarget() - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, hitHeight, 0); // Usar hitHeight para controlar la altura
        }
    }
}
