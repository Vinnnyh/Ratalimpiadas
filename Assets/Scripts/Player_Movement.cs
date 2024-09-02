using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{   
    public Transform aimTarget;
    float speed = 3f;
    
    bool hitting;
    // Variable para medir el tiempo que se mantiene presionada la tecla
    float hitChargeTime = 0f; 

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
        
        // Detectar cuando se presiona la tecla de disparo (en este caso, espacio)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hitting = true;
            hitChargeTime = 0f; 
        }

        // Detectar cuando se mantiene presionada la tecla de disparo
        if (hitting)
        {
            hitChargeTime += Time.deltaTime; 
        }

        // Detectar cuando se suelta la tecla de disparo
        if (Input.GetKeyUp(KeyCode.Space))
        {
            hitting = false;
            PerformShot(); // Llamar a la función para disparar
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

    // Función que realiza el disparo
    private void PerformShot()
    {
        // Limitar el tiempo de carga para que no exceda un valor máximo
        float maxChargeTime = 3f; 
        float chargePercent = Mathf.Clamp01(hitChargeTime / maxChargeTime);

        // Fuerza base
        float baseHitForce = 5f;
        float baseUpForce = 2f;

        // Calcular la fuerza final en función del tiempo de carga
        float finalHitForce = baseHitForce + (currentShot.hitForce * chargePercent);
        float finalUpForce = baseUpForce + (currentShot.upForce * chargePercent);

        // Aplicar la fuerza calculada
        currentShot.hitForce = finalHitForce;
        currentShot.upForce = finalUpForce;
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

