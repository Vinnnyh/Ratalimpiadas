using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilling : MonoBehaviour
{
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Obt�n el tama�o del objeto en el mundo
        Vector3 objectSize = transform.localScale;

        // Ajusta el tiling del material
        if (renderer.material != null)
        {
            renderer.material.mainTextureScale = new Vector2(objectSize.x, objectSize.z);
        }
    }
}
