using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float timeCounter = 0;
    [SerializeField] private float speed;
    [SerializeField] float zAxis = 0f;
    [SerializeField] float yAxis = 0f;
    [SerializeField] float xAxis = 0f;


    void Update()
    {
        timeCounter = Time.deltaTime * speed;
        
        transform.Rotate(xAxis * timeCounter, yAxis * timeCounter, zAxis * timeCounter, Space.Self);
    }
}