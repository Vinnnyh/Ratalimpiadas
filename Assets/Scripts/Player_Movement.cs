using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{   
    public Transform aimTarget;
    float speed = 3f;
    
    bool hitting;

    Vector3 aimTargetInitialPosition;

    ShotManager shotManager;
    Shot currentShot;
    private void Start()
    {
        aimTargetInitialPosition = aimTarget.position;
        shotManager = GetComponent<ShotManager>();
        currentShot = shotManager.topSpin;
    }
    

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        // Detectar cuando se presiona la tecla F
        if (Input.GetKeyDown(KeyCode.F))
        {
            hitting = true;
            currentShot = shotManager.topSpin;
        }

        // Detectar cuando se suelta la tecla F
        if (Input.GetKeyUp(KeyCode.F))
        {
            hitting = false;
        }

        // Detectar cuando se presiona la tecla E
        if (Input.GetKeyDown(KeyCode.E))
        {
            hitting = true;
            currentShot = shotManager.flat;
        }

        // Detectar cuando se suelta la tecla E
        if (Input.GetKeyUp(KeyCode.E))
        {
            hitting = false;
        }

        // Movimiento del aimTarget
        if (hitting)
        {
            aimTarget.Translate(new Vector3(h, 0, v) * speed * 2 * Time.deltaTime);
        }

        // Movimiento del jugador
        if (!hitting && h != 0)
        {
            transform.Translate(Vector3.right * h * speed * Time.deltaTime);
        }
    }

    // Detectar colisión con "Ball"
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0 , currentShot.upForce , 0);
        }
    }
}
