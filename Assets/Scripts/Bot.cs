using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    float speed = 10f;
    float force = 6f;
    public Transform ball;
    public Transform aimTarget;

    public Transform[] targets;

    Vector3 targetPosition;
    float reactionTime = 0.018f;
    float lastMoveTime = 0f;
    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        if (Time.time > lastMoveTime + reactionTime)
        {
            Move();
            lastMoveTime = Time.time;  
        }
    }

    void Move()
    {
        targetPosition.x = ball.position.x;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    
    Vector3 PickTarget()
    {
        int randomValue = Random.Range(0, targets.Length);
        return targets[randomValue].position;
    } 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Vector3 dir = PickTarget() - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 4, 0);
        }
    }
}
