using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Rigidbody rBody;
    [SerializeField] public float thrustForce = 100f;
    [SerializeField] float rotationSpeed = 10f;
    private Vector3 originalPosition;
    private Vector3 originalRotation;
    private AudioSource playerAudioSource;
    public AudioClip mainThrust_SFX;
    public ParticleSystem mainThrustSmoke_VFX;
    public ParticleSystem mainThrustJet_VFX;
    public Light mainThrust_Light;
    public ParticleSystem rightThrustSmoke_VFX;
    public ParticleSystem rightThrustJet_VFX;
    public Light rightThrust_Light;
    public ParticleSystem leftThrustSmoke_VFX;
    public ParticleSystem leftThrustJet_VFX;
    public Light leftThrust_Light;

    public CollisionHandler cH;
    private bool isTransitioning;



    
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = gameObject.transform.position;
        ///originalRotation = gameObject.transform.rotation;

        rBody = GetComponent<Rigidbody>();
        playerAudioSource = GetComponent<AudioSource>();

        cH = FindObjectOfType<CollisionHandler>();
        
        

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        ResetCheck();
        isTransitioning = cH.isTransitioning;

        if (isTransitioning){

            mainThrustSmoke_VFX.Stop();
            mainThrustJet_VFX.Stop();
            mainThrust_Light.GetComponent<Light>().enabled = false;

            rightThrustSmoke_VFX.Stop();
            rightThrustJet_VFX.Stop();
            rightThrust_Light.GetComponent<Light>().enabled = false;

            leftThrustSmoke_VFX.Stop();
            leftThrustJet_VFX.Stop();
            leftThrust_Light.GetComponent<Light>().enabled = false;
        }
            

    }
    
    //This adds force on the local Y axis when Space is pressed.
    public void ProcessThrust()
    {
        if (isTransitioning){ return; }

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
        if (isTransitioning){ return; }

        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Rotate Left");
            ApplyRotation(rotationSpeed);
           
             if(!rightThrustSmoke_VFX.isEmitting) {
                PlayFX(rightThrustSmoke_VFX, rightThrustJet_VFX, rightThrust_Light, null);
                StopFX(leftThrustSmoke_VFX, leftThrustJet_VFX, leftThrust_Light, null);
             }
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("Rotate Right");

            ApplyRotation(-rotationSpeed);

            if (!leftThrustSmoke_VFX.isEmitting) {
                PlayFX(leftThrustSmoke_VFX, leftThrustJet_VFX, leftThrust_Light, null);
                StopFX(rightThrustSmoke_VFX, rightThrustJet_VFX, rightThrust_Light, null);
            }

        } else if  (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            if (!rightThrustSmoke_VFX.isEmitting || !leftThrustSmoke_VFX.isEmitting){
                PlayFX(rightThrustSmoke_VFX, rightThrustJet_VFX, rightThrust_Light, null);
                PlayFX(leftThrustSmoke_VFX, leftThrustJet_VFX, leftThrust_Light, null);
            }
        } else if (!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            if (rightThrustSmoke_VFX.isEmitting || leftThrustSmoke_VFX.isEmitting){
                StopFX(rightThrustSmoke_VFX, rightThrustJet_VFX, rightThrust_Light, null);
                StopFX(leftThrustSmoke_VFX, leftThrustJet_VFX, leftThrust_Light, null);
            }
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
    private void PlayFX(ParticleSystem smoke, ParticleSystem jet, Light light, AudioClip sfx)
    {
       
        smoke.Play();
        jet.Play();
        light.GetComponent<Light>().enabled = true;

        playerAudioSource.PlayOneShot(sfx);

    }

    private void StopFX(ParticleSystem smoke, ParticleSystem jet, Light light, AudioClip sfx)
    {

        smoke.Stop();
        jet.Stop();
        light.GetComponent<Light>().enabled = false;

        //playerAudioSource.Stop;
        
    }
}


