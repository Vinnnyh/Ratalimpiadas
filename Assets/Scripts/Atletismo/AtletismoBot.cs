using UnityEngine;

public class AtletismoBot : MonoBehaviour
{
    public float minRunSpeed = 3f; // Velocidad mínima
    public float maxRunSpeed = 6f; // Velocidad máxima
    private float runSpeed; // Velocidad actual del bot
    public float gravityScale = 1f; // Escala de gravedad personalizada

    private Rigidbody rb;
    private bool isGravityInverted = false; // Estado de la gravedad actual
    public GameObject visualObject; // Objeto visual del sprite

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Desactivar la gravedad global predeterminada

        // Configura la velocidad inicial en un rango aleatorio
        runSpeed = Random.Range(minRunSpeed, maxRunSpeed);

        // Configura la gravedad inicial
        ApplyGravity();
    }

    void FixedUpdate()
    {
        // Movimiento horizontal continuo
        transform.Translate(Vector3.right * runSpeed * Time.fixedDeltaTime);

        // Aplicar gravedad personalizada
        Vector3 customGravity = (isGravityInverted ? Vector3.up : Vector3.down) * Physics.gravity.magnitude * gravityScale;
        rb.AddForce(customGravity, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            // Invertir la gravedad al tocar un waypoint
            ToggleGravity();
        }
        else if (other.CompareTag("End"))
        {
            Debug.Log("Bot alcanzó el final.");
            // Notificar al RaceManager que el bot llegó al final
        }
    }

    private void ToggleGravity()
    {
        isGravityInverted = !isGravityInverted;

        // Cambiar solo el objeto visual para reflejar el cambio de gravedad
        if (visualObject != null)
        {
            Vector3 newScale = visualObject.transform.localScale;
            newScale.y = isGravityInverted ? -Mathf.Abs(newScale.y) : Mathf.Abs(newScale.y);
            visualObject.transform.localScale = newScale;
        }

        Debug.Log($"Gravedad aplicada para el bot: {(isGravityInverted ? "Invertida" : "Normal")}");
    }

    private void ApplyGravity()
    {
        // Reinicia la velocidad vertical para evitar acumulaciones
        rb.velocity = new Vector3(rb.velocity.x, 0, 0);

        // Establece la dirección de la gravedad en función del estado actual
        Vector3 gravity = isGravityInverted ? Vector3.up : Vector3.down;
        rb.AddForce(gravity * Physics.gravity.magnitude * gravityScale, ForceMode.Acceleration);
    }
}
