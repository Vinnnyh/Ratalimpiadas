using UnityEngine;
using UnityEngine.SceneManagement;

public class ChasingWall : MonoBehaviour
{
    public float wallSpeed = 3f; // Velocidad de la pared
    public Transform player; // Referencia al jugador
    public float resetDelay = 2f; // Tiempo de espera antes de reiniciar el juego

    public float startDelay = 3f; // Tiempo de retraso antes de que la pared comience a moverse
    private bool shouldMove = false; // Controla si la pared debe moverse

    void Start()
    {
        // Iniciar la corutina para el retraso
        StartCoroutine(StartWallMovementAfterDelay());
    }

    void Update()
    {
        // Solo mover la pared si está habilitada para moverse
        if (shouldMove)
        {
            transform.Translate(Vector3.up * wallSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si la pared colisiona con el jugador, reinicia la escena
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡La pared atrapó al jugador! Reiniciando el juego...");
            Invoke("ResetGame", resetDelay);
        }
    }

    private System.Collections.IEnumerator StartWallMovementAfterDelay()
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(startDelay);

        // Activar el movimiento de la pared
        shouldMove = true;
        Debug.Log("La pared ha comenzado a moverse.");
    }

    private void ResetGame()
    {
        // Reiniciar la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
