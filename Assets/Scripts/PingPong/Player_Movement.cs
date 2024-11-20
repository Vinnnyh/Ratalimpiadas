using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Transform ball;
    public GameObject ballPredictionMarker;
    public GameObject botTable;
    public float speed = 3f;
    public float markerSpeed = 5f;
    public float hitRadius = 1.5f;
    public float maxChargeTime = 3f;
    public float baseHitForce = 5f;
    public float strongHitMultiplier = 1.2f;
    public float hitForceMultiplier = 10f;

    private Animator animator;
    bool hitting;
    float hitChargeTime = 0f;
    Vector3 predictedBallPosition;

    private Vector3 botTableMinBounds;
    private Vector3 botTableMaxBounds;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();

        if (ballPredictionMarker != null)
        {
            ballPredictionMarker.SetActive(true);
        }

        // Obtener los límites de BotTable en el espacio del mundo
        BoxCollider botTableCollider = botTable.GetComponent<BoxCollider>();
        botTableMinBounds = botTableCollider.bounds.min;
        botTableMaxBounds = botTableCollider.bounds.max;

        // Depuración para verificar los límites
        Debug.Log("BotTable Min Bounds: " + botTableMinBounds);
        Debug.Log("BotTable Max Bounds: " + botTableMaxBounds);

        // Centramos el marcador dentro de los límites del BotTable
        ballPredictionMarker.transform.position = new Vector3(
            (botTableMinBounds.x + botTableMaxBounds.x) / 2,
            ballPredictionMarker.transform.position.y,
            (botTableMinBounds.z + botTableMaxBounds.z) / 2
        );
    }

    void Update()
    {
        // Movimiento del jugador con joystick izquierdo o teclas A y D
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * speed * Time.deltaTime);

        // Control del BallPredictionMarker con joystick derecho o flechas
        MovePredictionMarker();

        // Control de golpe con Space o A en el mando
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Jump"))
        {
            hitting = true;
            hitChargeTime = 0f;
            animator.SetTrigger("Hit");
        }

        if (hitting)
        {
            hitChargeTime += Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Jump"))
        {
            hitting = false;
            PerformShot(false);
        }
    }

    private void MovePredictionMarker()
    {
        // Mover el BallPredictionMarker con joystick derecho o flechas
        float markerHorizontal = Input.GetAxisRaw("MarkerHorizontal") + (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) - (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0);
        float markerVertical = Input.GetAxisRaw("MarkerVertical") + (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) - (Input.GetKey(KeyCode.DownArrow) ? 1 : 0);

        Vector3 markerMovement = new Vector3(markerHorizontal, 0, markerVertical) * markerSpeed * Time.deltaTime;
        ballPredictionMarker.transform.Translate(markerMovement);

        // Limitar el marcador dentro de los límites de BotTable
        Vector3 clampedPosition = ballPredictionMarker.transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, botTableMinBounds.x, botTableMaxBounds.x);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, botTableMinBounds.z, botTableMaxBounds.z);

        ballPredictionMarker.transform.position = clampedPosition;
    }

    private void PerformShot(bool isStrongHit)
    {
        float chargePercent = Mathf.Clamp01(hitChargeTime / maxChargeTime);

        float finalForce = baseHitForce + (chargePercent * hitForceMultiplier);

        if (isStrongHit)
        {
            finalForce *= strongHitMultiplier;
        }

        if (Vector3.Distance(transform.position, ball.position) <= hitRadius)
        {
            Vector3 hitDirection = (ballPredictionMarker.transform.position - transform.position).normalized;

            // Ajustar la fuerza de disparo con altura ajustable
            float shotHeight = 4.0f;
            ball.GetComponent<Rigidbody>().velocity = hitDirection * finalForce + new Vector3(0, shotHeight, 0);
        }
    }
}
