using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidbody;
    bool RotatingLeft = false;
    bool RotatingRight = false;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        ProcessInput();
    }
    void ProcessInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up);
            print("Thrust");
        }
        if (Input.GetKey(KeyCode.A) && RotatingRight == false)
        {

            transform.Rotate(Vector3.forward * 0.2f);

        }
        else if (Input.GetKey(KeyCode.D) && RotatingLeft == false)
        {
            transform.Rotate(-Vector3.forward * 0.2f);
            RotatingRight = true;
        }
        RotatingLeft = false;
        RotatingRight = false;
    }
}
