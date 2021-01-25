using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [Range(0, 1)] [SerializeField] float movementFactor;

    [SerializeField] float period = 0f;
    Vector3 startingPos;
    void Start()
    {
       
        startingPos = transform.position;
    }
    void Update()
    {
        
        if (period <=  Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // gows continually from 0
        const float tau = Mathf.PI * 2f;// about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;        
        transform.position = startingPos + offset;
       
    }

}
