using System.Collections;
using UnityEngine;

public class PingPongCameraController : MonoBehaviour
{
    public Transform focusPoint; // Objeto vacío para el acercamiento inicial
    public float moveDuration = 2f; // Duración del movimiento de la cámara hacia el focusPoint
    public float pauseBeforeStart = 0f; // Tiempo antes de iniciar el acercamiento

    private Camera mainCamera;
    private bool isFocusing = true; // Controla si la cámara está en el proceso de enfoque

    private void Start()
    {
        mainCamera = Camera.main; // Obtener la cámara principal

        if (focusPoint != null)
        {
            StartCoroutine(ZoomEffect());
        }
        else
        {
            Debug.LogError("Debes asignar un focusPoint en el inspector.");
        }
    }

    private IEnumerator ZoomEffect()
    {
        // Esperar antes de comenzar el acercamiento
        yield return new WaitForSeconds(pauseBeforeStart);

        // Acercamiento inicial hacia el focusPoint
        float elapsedTime = 0f;
        Vector3 startPos = mainCamera.transform.position;
        Quaternion startRot = mainCamera.transform.rotation;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / moveDuration;

            // Interpolar posición y rotación
            mainCamera.transform.position = Vector3.Lerp(startPos, focusPoint.position, t);
            mainCamera.transform.rotation = Quaternion.Lerp(startRot, focusPoint.rotation, t);

            yield return null;
        }

        // Asegurarse de que la cámara llegue exactamente al destino
        mainCamera.transform.position = focusPoint.position;
        mainCamera.transform.rotation = focusPoint.rotation;

        Debug.Log("Acercamiento inicial completado.");

        isFocusing = false; // Finaliza el proceso de enfoque
    }
}
