using UnityEngine;
using UnityEngine.SceneManagement;

public class AtletismoController : MonoBehaviour
{
    public float runSpeed = 5f; 
    public float gravityScale = 1f;

    private Rigidbody rb;
    private bool isGravityInverted = false; 
    private KeyCode gravityToggleKey = KeyCode.Space; 
    private bool isGameEnded = false; 

    public AtletismoRaceManager raceManager;

    public GameObject visualObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Desactivamos la gravedad global predeterminada
        raceManager.RegisterRacer(transform, true);
    }

    void FixedUpdate()
    {
        if (isGameEnded) return; // Evitar actualizaciones si el juego ha terminado

        // Movimiento horizontal continuo
        transform.Translate(Vector3.right * runSpeed * Time.fixedDeltaTime);

        // Aplicar gravedad personalizada
        Vector3 customGravity = (isGravityInverted ? Vector3.up : Vector3.down) * Physics.gravity.magnitude * gravityScale;
        rb.AddForce(customGravity, ForceMode.Acceleration);
    }

    void Update()
    {
        if (isGameEnded) return; // Evitar actualizaciones si el juego ha terminado

        // Detectar si se presiona la tecla para invertir la gravedad
        if (Input.GetKeyDown(gravityToggleKey) || Input.GetButtonDown("gravitychange"))
        {
            ToggleGravity();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGameEnded) return;

        if (other.CompareTag("End"))
        {
            Debug.Log("Jugador alcanzó el final.");
            isGameEnded = true;
            raceManager.OnAthleticsRacerReachEnd(transform);
        }
        else if (other.CompareTag("Void"))
        {
            Debug.Log("El jugador cayó en un agujero. Reiniciando el juego...");
            ResetGame();
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
    }

    private void ResetGame()
    {
        SceneManager.LoadScene("MedallasScene");
    }
}
