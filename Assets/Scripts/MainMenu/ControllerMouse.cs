using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerMouse : MonoBehaviour
{
    public float mouseSpeed = 100f;  // Velocidad del movimiento del mouse
    public string horizontalAxis = "Horizontal";  // Eje horizontal del joystick
    public string verticalAxis = "Vertical";  // Eje vertical del joystick
    public string clickButton = "Submit";  // Botón para hacer clic (botón A del mando)

    private Vector2 mousePosition;

    void Start()
    {
        // Inicializa la posición del mouse en el centro de la pantalla
        mousePosition = new Vector2(Screen.width / 2, Screen.height / 2);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
    }

    void Update()
    {
        // Obtener el movimiento del joystick
        float moveX = Input.GetAxis(horizontalAxis) * mouseSpeed * Time.deltaTime;
        float moveY = Input.GetAxis(verticalAxis) * mouseSpeed * Time.deltaTime;

        // Actualizar la posición del mouse
        mousePosition += new Vector2(moveX, moveY);

        // Limitar la posición del mouse a los límites de la pantalla
        mousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);
        mousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);

        // Actualizar la posición del cursor en pantalla
        Vector3 screenPosition = new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
        transform.position = screenPosition;

        // Simular el clic izquierdo del mouse con el botón A
        if (Input.GetButtonDown(clickButton))
        {
            ExecuteEvents.Execute<IPointerClickHandler>(
                EventSystem.current.currentSelectedGameObject,
                new PointerEventData(EventSystem.current),
                ExecuteEvents.pointerClickHandler);
        }
    }
}
