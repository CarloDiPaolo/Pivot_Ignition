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
    public ParticleSystem mainThrustSmoke_VFX;
    public ParticleSystem mainThrustJet_VFX;
    public Light mainThrust_Light;
    public ParticleSystem rightThrust_VFX;
    public Light rightThrust_Light;
    public ParticleSystem leftThrust_VFX;
    public Light leftThrust_Light;



    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
        ///originalRotation = gameObject.transform.rotation;

        rBody = GetComponent<Rigidbody>();
        playerAudioSource = GetComponent<AudioSource>();
        
        

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
            if(!playerAudioSource.isPlaying) playerAudioSource.PlayOneShot(mainThrust_SFX);
                
            
            if (!mainThrustSmoke_VFX.isEmitting) mainThrustSmoke_VFX.Play();
            if (!mainThrustJet_VFX.isEmitting) mainThrustJet_VFX.Play();

            mainThrust_Light.GetComponent<Light>().enabled = true;

           
            
        } else if (Input.GetKeyUp(KeyCode.Space)){
            playerAudioSource.Stop();
            mainThrustSmoke_VFX.Stop();
            mainThrustJet_VFX.Stop();

            mainThrust_Light.GetComponent<Light>().enabled = false;
            

        }

    
    }


    //This rotates the Player/Rocket depending on the input direction
    public void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Rotate Left");
            ApplyRotation(rotationSpeed);
            if(!rightThrust_VFX.isEmitting){
                rightThrust_VFX.Play();
                leftThrust_VFX.Stop();
                rightThrust_Light.GetComponent<Light>().enabled = true;
                leftThrust_Light.GetComponent<Light>().enabled = false;
            }

        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("Rotate Right");

            ApplyRotation(-rotationSpeed);
            if(!leftThrust_VFX.isEmitting){
                leftThrust_VFX.Play();
                rightThrust_VFX.Stop();
                leftThrust_Light.GetComponent<Light>().enabled = true;
                rightThrust_Light.GetComponent<Light>().enabled = false;
            }

        } else if  (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            rightThrust_VFX.Play();
            leftThrust_VFX.Play();
            //Debug.Log("Both Side Thrusters Engaged"); 
            rightThrust_Light.GetComponent<Light>().enabled = true;
            leftThrust_Light.GetComponent<Light>().enabled = true;

        } else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            leftThrust_VFX.Stop();
            rightThrust_VFX.Stop();
            rightThrust_Light.GetComponent<Light>().enabled = false;
            leftThrust_Light.GetComponent<Light>().enabled = false;
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


