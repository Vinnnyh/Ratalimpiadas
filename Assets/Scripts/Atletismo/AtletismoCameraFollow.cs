using UnityEngine;

public class AtletismoCameraFollow : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public Transform focusPoint; // Objeto vacío para el acercamiento inicial
    public float smoothSpeed = 0.125f; // Velocidad de suavizado para el seguimiento
    public float focusSmoothSpeed = 0.05f; // Velocidad de acercamiento al punto inicial
    public float focusThreshold = 0.01f; // Distancia mínima para considerar que se alcanzó el punto de enfoque

    private bool isFocusing = true; // Controla si la cámara está en el proceso de enfoque
    private Vector3 fixedCameraHeight; // Guarda la posición Y y Z después del enfoque

    void Start()
    {
        // Verificar si el `focusPoint` está asignado
        if (focusPoint == null)
        {
            Debug.LogError("No se ha asignado un `focusPoint` en el inspector.");
            isFocusing = false; // Evita problemas si falta el punto de enfoque
        }
    }

    void LateUpdate()
    {
        if (isFocusing && focusPoint != null)
        {
            // Acercamiento inicial al objeto focusPoint
            Vector3 focusPosition = new Vector3(focusPoint.position.x, focusPoint.position.y, focusPoint.position.z);
            transform.position = Vector3.Lerp(transform.position, focusPosition, focusSmoothSpeed);

            // Revisar si la cámara ha alcanzado suficientemente cerca del focusPoint
            if (Vector3.Distance(transform.position, focusPosition) <= focusThreshold)
            {
                transform.position = focusPosition; // Fija la posición exactamente en el `focusPoint`
                fixedCameraHeight = new Vector3(0, focusPosition.y, focusPosition.z); // Fija la altura y profundidad en el punto alcanzado
                isFocusing = false; // Termina el enfoque inicial
                Debug.Log("Cámara detenida en el punto de enfoque.");
            }
        }
        else
        {
            // Seguir solo en el eje X, mantener Y y Z fijas
            Vector3 targetPosition = new Vector3(player.position.x, fixedCameraHeight.y, fixedCameraHeight.z);

            // Interpolar suavemente hacia la nueva posición
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        }
    }
}
