using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    float timeCounter = 0f;

    [SerializeField]
    float speed = 0f;

    [SerializeField]
    float width = 0f;

    [SerializeField]
    float height = 0f;

    [SerializeField]
    float z = 0f;

    void Update()
    {
        timeCounter += Time.deltaTime * speed;

        float x = Mathf.Cos(timeCounter) * width;
        float y = Mathf.Sin(timeCounter) * height;

        transform.position = new Vector3(x, y, z);
    }
}
