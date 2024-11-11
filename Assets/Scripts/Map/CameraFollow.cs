using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Transform cam;
    public float smoothness;
    public Vector3 offset;
    Vector3 vel;

    private void Update()
    {
        Vector3 target = player.position + offset;
        transform.position = Vector3.SmoothDamp(cam.position, target, ref vel, smoothness);
    }
}
