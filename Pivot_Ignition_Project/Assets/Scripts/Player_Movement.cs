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
        /*mainThrust_VFX.Stop();
        leftThrust_VFX.Stop();
        rightThrust_VFX.Stop();
        */
        
        
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
                
            }
            if (!mainThrust_VFX.isPlaying) mainThrust_VFX.Play();
            
        } else if (Input.GetKeyUp(KeyCode.Space)){
            playerAudioSource.Stop();
            mainThrust_VFX.Stop();

        }

    
    }


    //This rotates the Player/Rocket depending on the input direction
    public void ProcessRotation()
    {
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

        } else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
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


