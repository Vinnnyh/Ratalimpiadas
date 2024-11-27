using UnityEngine;

public class SwimmingController : MonoBehaviour
{
    public float swimSpeed = 10f;
    private bool canPressW = true;
    private bool canPressS = false;
    private Vector3 moveDirection = Vector3.forward;
    public RaceManager raceManager;

    void Start()
    {
        moveDirection = transform.forward;
        raceManager.RegisterRacer(transform, true); // Notifica al RaceManager que este es el jugador
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && canPressW)
        {
            SwimForward();
            canPressW = false;
            canPressS = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) && canPressS)
        {
            SwimForward();
            canPressS = false;
            canPressW = true;
        }

        if (Input.GetAxis("AtletismTriggerLeft") < 0f && Input.GetAxis("AtletismTriggerRight") < 0f) { }
        else
        {
            if (Input.GetAxis("AtletismTriggerLeft") < 0f && canPressW)
            {
                SwimForward();
                canPressW = false;
                canPressS = true;
            }
            else if (Input.GetAxis("AtletismTriggerRight") < 0f && canPressS)
            {
                SwimForward();
                canPressS = false;
                canPressW = true;
            }
        }
    }

    private void SwimForward()
    {
        transform.Translate(moveDirection * swimSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("End"))
        {
            raceManager.OnRacerReachEnd(transform);
            moveDirection = -moveDirection;
            transform.Rotate(0, 180, 0);
        }
        else if (other.CompareTag("Start"))
        {
            raceManager.OnRacerReachStart(transform);
        }
    }
}
