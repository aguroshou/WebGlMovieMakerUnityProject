using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    private float rotationX;

    // Update is called once per frame
    void Update()
    {
        rotationX += -2.4f;
        transform.rotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
    }
}