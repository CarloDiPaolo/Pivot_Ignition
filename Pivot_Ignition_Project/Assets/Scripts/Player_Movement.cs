using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Rigidbody rBody;
    [SerializeField] float thrustForce = 100f;
    [SerializeField] float rotationSpeed = 10f;
    private Vector3 originalPosition;
    private Vector3 originalRotation;
    private AudioSource playerAudioSource;
    public AudioClip mainThrust_SFX;
    public ParticleSystem mainThrust_VFX;
    public ParticleSystem rightThrust_VFX;
    public ParticleSystem leftThrust_VFX;



    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
        ///originalRotation = gameObject.transform.rotation;

        rBody = GetComponent<Rigidbody>();
        playerAudioSource = GetComponent<AudioSource>();
        //mainThrust_VFX.Stop();
        //leftThrust_VFX.Stop();
        //rightThrust_VFX.Stop();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        ProcessThrust();
        ProcessRotation();
        ResetCheck();
            

    }
    
    //This adds force on the local Y axis when Space is pressed.
    public void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("Thrusters engaged");
            rBody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
            if(!playerAudioSource.isPlaying)
            {
                playerAudioSource.PlayOneShot(mainThrust_SFX);
                mainThrust_VFX.Play();
            }
            
        } else if (Input.GetKeyUp(KeyCode.Space)){
            playerAudioSource.Stop();
            mainThrust_VFX.Stop();

        }

    
    }


    //This rotates the Player/Rocket depending on the input direction
    public void ProcessRotation()
    {
        //Trying to find the best way to not give one direction precendence over the other. Old version below. New Version after that
        /*if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            Debug.Log("Straight ahead");
        }else if(Input.GetKey(KeyCode.RightArrow)||Input.GetKey(KeyCode.D))
        {
            Debug.Log("Rotate Right");
        }else if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotate Left");
        } */


        //This version is cleaner, but accounts only for A and D and not arrow Keys, for now. Add arrow key input later(Done)
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Rotate Left");
            ApplyRotation(rotationSpeed);
            rightThrust_VFX.Play();
            leftThrust_VFX.Stop();

        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("Rotate Right");

            ApplyRotation(-rotationSpeed);
            leftThrust_VFX.Play();
            rightThrust_VFX.Stop();

        } else if  (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            rightThrust_VFX.Play();
            leftThrust_VFX.Play();
            Debug.Log("Both Side Thrusters Engaged");   
        } else {
            leftThrust_VFX.Stop();
            rightThrust_VFX.Stop();
        }
    }

    void ApplyRotation(float rotationDirection)
    {
        rBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationDirection * Time.deltaTime);
        rBody.freezeRotation = false;
    }

    void ResetCheck()
    {
        if (Input.GetKeyDown(KeyCode.AltGr))
        {
            gameObject.transform.position = originalPosition;
            gameObject.transform.rotation = Quaternion.Euler(0,0,0);
            rBody.velocity = Vector3.zero;
            rBody.angularVelocity = Vector3.zero;
        }
    }

}


