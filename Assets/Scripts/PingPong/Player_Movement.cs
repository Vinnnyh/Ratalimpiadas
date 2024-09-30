using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Transform ball;  
    public GameObject ballPredictionMarker; 
    public float speed = 3f;
    public float hitRadius = 1.5f; 
    public float maxChargeTime = 3f; 
    public float baseHitForce = 5f;  
    public float strongHitMultiplier = 1.2f; 
    public float hitForceMultiplier = 10f; 

    private BoxCollider racketCollider;  // Collider para el golpe de la raqueta
    bool hitting;
    float hitChargeTime = 0f;

    Vector3 predictedBallPosition;

    private void Start()
    {
        ballPredictionMarker.SetActive(false); 

        // Buscar el BoxCollider de la raqueta en el objeto hijo "RacketCollider"
        racketCollider = GameObject.Find("RacketCollider").GetComponent<BoxCollider>();  
    }

    void Update()
    {
        // Movimiento del jugador
        float h = Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * h * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            hitting = true;
            hitChargeTime = 0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            hitting = true;
            hitChargeTime = 0f;  
        }

        if (hitting)
        {
            hitChargeTime += Time.deltaTime;  
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            hitting = false;
            PerformShot(false); 
        }

        if (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyUp(KeyCode.RightControl))
        {
            hitting = false;
            PerformShot(true); 
        }

        PredictBotShot();  
    }

    private void PerformShot(bool isStrongHit)
    {
        float chargePercent = Mathf.Clamp01(hitChargeTime / maxChargeTime);

        float finalForce = baseHitForce + (chargePercent * hitForceMultiplier);

        if (isStrongHit)
        {
            finalForce *= strongHitMultiplier; 
        }

        // Solo golpea si la pelota está dentro del radio de golpeo
        if (Vector3.Distance(transform.position, ball.position) <= hitRadius)
        {
            Vector3 hitDirection = Vector3.forward;

            if (Input.GetKey(KeyCode.A))
            {
                hitDirection = new Vector3(-0.5f, 0, 1).normalized;  
            }
            else if (Input.GetKey(KeyCode.D))
            {
                hitDirection = new Vector3(0.5f, 0, 1).normalized; 
            }

            ball.GetComponent<Rigidbody>().velocity = hitDirection * finalForce + new Vector3(0, 3, 0); 
        }
    }

    // Detectar colisiones para asegurarse de que solo el RacketCollider golpea la pelota
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // Solo permite golpear si el collider tiene el tag "Racket"
            if (other.transform.CompareTag("Racket"))
            {
                Debug.Log("La pelota fue golpeada por la raqueta.");
                PerformShot(false);  // Realiza el golpe aquí
            }
        }
    }

    private void PredictBotShot()
    {
        if (ball.GetComponent<Rigidbody>().velocity.z < 0) 
        {
            RaycastHit hit;
            if (Physics.Raycast(ball.position, ball.GetComponent<Rigidbody>().velocity, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Table"))  
                {
                    predictedBallPosition = hit.point;  

                    ballPredictionMarker.SetActive(true);
                    ballPredictionMarker.transform.position = new Vector3(predictedBallPosition.x, ballPredictionMarker.transform.position.y, predictedBallPosition.z);
                }
            }
        }
        else
        {
            ballPredictionMarker.SetActive(false); 
        }
    }
}
