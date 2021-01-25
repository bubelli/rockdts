using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class OscillatorA : MonoBehaviour
{
    //maybe a project for future
    [SerializeField] Vector3 movementVector;
     float movementFactor;

    [SerializeField] float period = 0f;
    [SerializeField] bool advancedSettings = false;

    [SerializeField] Vector3 movementVectorA;//A is advanced
    float movementFactorA;
    [SerializeField] float periodA = 0f;
    [SerializeField] float delay;
    Vector3 startingPos;
    void Start()
    {
        print(Mathf.Epsilon);
        startingPos = transform.position;
    }
    void Update()
    {

        ApplyNormalSettings();
        Invoke("ApplyAdvancedSettings", period);
        
    }
    void ApplyNormalSettings() {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // gows continually from 0
        const float tau = Mathf.PI * 2f;// about 6.28
        float rawSinWave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
    void ApplyAdvancedSettings()
        {
        float cyclesA = Time.time / periodA;
        const float tauA = Mathf.PI * 2f;
        float rawSinWaveA = Mathf.Sin(cyclesA *tauA);
        movementFactorA = rawSinWaveA / 2f + 0.5f; ;
        Vector3 offset = movementVectorA * movementFactorA;
        transform.position = startingPos + offset;
        }

}
