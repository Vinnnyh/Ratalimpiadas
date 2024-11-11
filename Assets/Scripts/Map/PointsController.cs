using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsController : MonoBehaviour
{
    public static PointsController instance;
    [SerializeField] private float PointsAmount;

    private void Awake()
    {
        if (PointsController.instance == null)
        {
            PointsController.instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPoints(float points)
    {
        PointsAmount += points;
    }
}
