using System.Collections;
using UnityEngine;

public class CameraZoomEffect : MonoBehaviour
{
    public Transform targetPosition; // La posición hacia la que la cámara se moverá
    public float moveDuration = 2f; // Duración del movimiento de la cámara
    public float pauseBeforeStart = 0f; // Tiempo antes de iniciar el acercamiento

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main; // Obtener la cámara principal
        if (targetPosition != null)
        {
            StartCoroutine(ZoomEffect());
        }
        else
        {
            Debug.LogError("Debes asignar una posición de destino en el inspector.");
        }
    }

    private IEnumerator ZoomEffect()
    {
        // Esperar antes de comenzar el acercamiento
        yield return new WaitForSeconds(pauseBeforeStart);

        // Mover la cámara hacia la posición objetivo
        float elapsedTime = 0f;
        Vector3 startPos = mainCamera.transform.position;
        Quaternion startRot = mainCamera.transform.rotation;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;

            // Interpolar posición y rotación
            mainCamera.transform.position = Vector3.Lerp(startPos, targetPosition.position, t);
            mainCamera.transform.rotation = Quaternion.Lerp(startRot, targetPosition.rotation, t);

            yield return null;
        }

        // Asegurarse de que la cámara llegue exactamente al destino
        mainCamera.transform.position = targetPosition.position;
        mainCamera.transform.rotation = targetPosition.rotation;

        Debug.Log("Acercamiento completado.");
    }
}
