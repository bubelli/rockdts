using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 1300f;
    AudioSource audioSource;
    Rigidbody rigidbody; 
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }
    void Update()
    {
        Rotate();
        Thrust();
    }
    void Thrust()
    {
        float ThrustThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.AddRelativeForce(Vector3.up * ThrustThisFrame);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
    void Rotate()
    {
       
        rigidbody.freezeRotation = true;
        float RotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(Vector3.forward * RotationThisFrame );
        }
        else if (Input.GetKey(KeyCode.D))
        {
            
            transform.Rotate(-Vector3.forward * RotationThisFrame);            
        }
        rigidbody.freezeRotation = false;

    }
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "friendly":
                //do nothing
                print("OK");
                break;
            case "fuel":
                print("fuel");
                //for future
                break;
            case "WinnerPlatform":
                print("won");
                break;
            default:
                print("boom");
                // player dead
                break;
        }
    }
}
