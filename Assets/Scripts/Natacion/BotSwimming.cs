using UnityEngine;

public class BotSwimming : MonoBehaviour
{
    private float speed;
    public float minSpeed = 2f;
    public float maxSpeed = 3f;
    private float changeInterval = 1f;
    private float nextChangeTime = 0f;
    private Vector3 moveDirection = Vector3.forward;
    public RaceManager raceManager;

    void Start()
    {
        moveDirection = transform.forward;
        raceManager.RegisterRacer(transform, false); // Notifica al RaceManager que este es un bot
    }

    void Update()
    {
        if (Time.time >= nextChangeTime)
        {
            speed = Random.Range(minSpeed, maxSpeed);
            nextChangeTime = Time.time + changeInterval;
        }
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            raceManager.OnRacerReachEnd(transform);
            moveDirection = -moveDirection;
            transform.Rotate(0, 180, 0);
        }
        else if (other.CompareTag("Start"))
        {
            raceManager.OnRacerReachStart(transform);
            enabled = false;
        }
    }
}
