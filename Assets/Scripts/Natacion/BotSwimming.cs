using UnityEngine;

public class BotSwimming : MonoBehaviour
{
    private float speed; // Velocidad actual del bot
    public float minSpeed = 2f; // Velocidad mínima
    public float maxSpeed = 3f; // Velocidad máxima
    private float changeInterval = 1f; // Intervalo de tiempo en segundos para cambiar la velocidad
    private float nextChangeTime = 0f; // Tiempo para el próximo cambio de velocidad
    private Vector3 moveDirection = Vector3.forward; // Dirección de movimiento inicial
    public RaceManager raceManager;

    void Start()
    {
        // Inicializamos la dirección para que siempre se mueva hacia adelante al comenzar
        moveDirection = transform.forward;
        raceManager.RegisterRacer(transform);
    }

    void Update()
    {
        // Cambiar la velocidad del bot cada cierto intervalo
        if (Time.time >= nextChangeTime)
        {
            speed = Random.Range(minSpeed, maxSpeed);
            nextChangeTime = Time.time + changeInterval;
        }

        // Mover el bot en la dirección actual usando la velocidad
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            // Notificar al RaceManager que el bot llegó a End
            raceManager.OnRacerReachEnd(transform);

            // Cambiar dirección de movimiento
            moveDirection = -moveDirection;
            transform.Rotate(0, 180, 0);
        }
        else if (other.CompareTag("Start"))
        {
            raceManager.OnRacerReachStart(transform);

            // Detener al bot al llegar a Start
            Debug.Log($"Bot {name} alcanzó Start. Finalizando.");
            enabled = false; // Detener actualizaciones del bot
        }
    }
}
