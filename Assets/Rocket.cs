using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    [SerializeField] float rcsThrust = 150f;
    [SerializeField] float mainThrust = 1300f;
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] float deathDelay = 1f;

    int nextSceneIndex;
    int currentSceneIndex;  
    AudioSource audioSource;
    Rigidbody rigidbody;
    bool toggleCollisionDetection = false;
    enum State { Alive, Dying, Transending }
    State state = State.Alive;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
       
        if (state == State.Alive)
        {
            RespondToRotateInput();
            RespondToThrustInput();
        }
        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.F)) { LoadingNewLevel(); }
        if (Input.GetKeyDown(KeyCode.C) && toggleCollisionDetection == false)
        {
            toggleCollisionDetection = true;
        } else if (Input.GetKeyDown(KeyCode.C))
        {
            toggleCollisionDetection = false;
        }
        
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || toggleCollisionDetection) {  return; }

        switch (collision.gameObject.tag)
        {
            case "friendly":
                break;
            case "fuel":
                print("fuel");
                break;
            case "WinnerPlatform":
                audioSource.Stop();
                state = State.Transending;
                audioSource.PlayOneShot(success);
                successParticles.Play();
                Invoke("LoadingNewLevel", levelLoadDelay);
                break;
            default:
                audioSource.Stop();
                state = State.Dying;
                audioSource.PlayOneShot(death);
                deathParticles.Play();
                Invoke("Dying", deathDelay);
                break;
        }
    }
    void Dying()
    {

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex;
        SceneManager.LoadScene(nextSceneIndex);

    }
    void LoadingNewLevel()
    {
        
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 6)
        {
            nextSceneIndex = 0;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            nextSceneIndex = currentSceneIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
    void RespondToThrustInput()
    {
        float ThrustThisFrame = mainThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust(ThrustThisFrame);
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    } 
    void RespondToRotateInput()
    {

        rigidbody.freezeRotation = true;
        float RotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(Vector3.forward * RotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * RotationThisFrame);
        }
        rigidbody.freezeRotation = false;

    }
    void ApplyThrust(float ThrustThisFrame)
    {
        rigidbody.AddRelativeForce(Vector3.up * ThrustThisFrame);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
            mainEngineParticles.Play();
        }

    }
}
