using UnityEngine;

public class AtletismoCameraFollow : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado para el seguimiento

    private Vector3 fixedCameraHeight; // Guarda la posición Y y Z fijas

    void Start()
    {
        // Fijar la altura y profundidad inicial de la cámara
        fixedCameraHeight = new Vector3(0, transform.position.y, transform.position.z);
    }

    void LateUpdate()
    {
        // Seguir solo en el eje X, mantener Y y Z fijas
        Vector3 targetPosition = new Vector3(player.position.x, fixedCameraHeight.y, fixedCameraHeight.z);

        // Interpolar suavemente hacia la nueva posición
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
    }
}
