using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    public float speed = 8.8f; // Velocidad del bot
    public float force = 6f; // Fuerza del golpe
    public float hitHeight = 2f; // Altura del golpe
    public float airSpeedReduction = 0.8f; // Velocidad mínima en el aire
    public float slowdownDuration = 1f; // Duración de la ralentización

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
            Rigidbody ballRb = other.GetComponent<Rigidbody>();

            if (ballRb != null)
            {
                ballRb.velocity = dir.normalized * force + new Vector3(0, hitHeight, 0); // Usar hitHeight para controlar la altura

                // Reducir la velocidad de la pelota en el aire manteniendo la fuerza
                StartCoroutine(SlowBallWhileMaintainingTrajectory(ballRb, dir));
            }
        }
    }

    private IEnumerator SlowBallWhileMaintainingTrajectory(Rigidbody ballRb, Vector3 targetDirection)
    {
        float timeElapsed = 0f;
        Vector3 initialVelocity = ballRb.velocity;

        while (timeElapsed < slowdownDuration)
        {
            timeElapsed += Time.deltaTime;

            // Mantener la dirección pero reducir gradualmente la velocidad
            float lerpFactor = timeElapsed / slowdownDuration;
            ballRb.velocity = Vector3.Lerp(initialVelocity, targetDirection.normalized * airSpeedReduction, lerpFactor);

            yield return null;
        }

        // Asegurar que la pelota mantenga la velocidad mínima después de ralentizarse
        ballRb.velocity = targetDirection.normalized * airSpeedReduction;
    }
}
