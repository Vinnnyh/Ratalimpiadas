using UnityEngine;

public class CameraFollowSwimming : MonoBehaviour
{
    public Transform player; // Referencia al jugador
    public Vector3 offset; // Offset inicial para posicionar la cámara detrás del jugador
    public float smoothSpeed = 0.125f; // Velocidad de suavizado para el seguimiento

    private Vector3 initialOffset;

    void Start()
    {
        // Calcular y almacenar el offset inicial entre la cámara y el jugador
        initialOffset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Determinar la dirección del jugador
        Vector3 desiredPosition = player.position + player.forward * initialOffset.z + player.up * initialOffset.y;
        
        // Interpolar suavemente la posición de la cámara hacia la posición deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Ajustar la rotación de la cámara para que siempre mire hacia la parte posterior del jugador
        Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed);
    }
}
