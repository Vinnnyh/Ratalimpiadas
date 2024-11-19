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

        // Get the bounds of BotTable in world space
        BoxCollider botTableCollider = botTable.GetComponent<BoxCollider>();
        botTableMinBounds = botTableCollider.bounds.min;
        botTableMaxBounds = botTableCollider.bounds.max;
    }

    void Update()
    {
        // Player movement with left joystick or A and D keys
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * speed * Time.deltaTime);

        // Control BallPredictionMarker with right joystick or arrow keys
        MovePredictionMarker();

        // Hit control with Space key or A button on the controller
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
        // Move BallPredictionMarker with right joystick or arrow keys
        float markerHorizontal = Input.GetAxisRaw("MarkerHorizontal") + (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) - (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0);
        float markerVertical = Input.GetAxisRaw("MarkerVertical") + (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) - (Input.GetKey(KeyCode.DownArrow) ? 1 : 0);

        Vector3 markerMovement = new Vector3(markerHorizontal, 0, markerVertical) * markerSpeed * Time.deltaTime;
        ballPredictionMarker.transform.Translate(markerMovement);

        // Clamp the marker within BotTable boundaries
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

            ball.GetComponent<Rigidbody>().velocity = hitDirection * finalForce + new Vector3(0, 3, 0);
        }
    }
}
