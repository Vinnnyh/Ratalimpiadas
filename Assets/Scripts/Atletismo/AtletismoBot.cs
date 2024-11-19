using UnityEngine;

public class AtletismoBot : MonoBehaviour
{
    private float speed; // Velocidad actual del bot
    private float minSpeed = 0.1f; // Velocidad mínima
    private float maxSpeed = 0.7f; // Velocidad máxima
    private float changeInterval = 1f; // Intervalo de tiempo para cambiar la velocidad
    private float nextChangeTime = 0f; // Tiempo para el próximo cambio de velocidad
    public AtletismoRaceManager atletismoRaceManager;

    void Start()
    {
        atletismoRaceManager.RegisterAthleticsRacer(transform); // Registrar bot en el AtletismoRaceManager
    }

    void Update()
    {
        // Cambiar la velocidad del bot cada cierto intervalo
        if (Time.time >= nextChangeTime)
        {
            speed = Random.Range(minSpeed, maxSpeed);
            nextChangeTime = Time.time + changeInterval;
        }

        // Mover el bot hacia adelante usando la velocidad
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            // Notificar al AtletismoRaceManager que el bot llegó al final
            atletismoRaceManager.OnAthleticsRacerReachEnd(transform);

            // Detener el bot al llegar al final
            speed = 0f;
            Debug.Log($"{name} ha llegado al final y ha dejado de correr.");
        }
    }
}
