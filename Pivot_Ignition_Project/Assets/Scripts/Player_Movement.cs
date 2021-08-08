using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Rigidbody rBody;
    [SerializeField] float thrustForce = 1f;
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        
    }
    
    //This adds force on the local Y axis when Space is pressed.
    public void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Thrusters engaged");
            rBody.AddRelativeForce(Vector3.up * thrustForce * Time.deltaTime);
            
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


        //This version is cleaner, but accounts only for A and D and not arrow Keys, for now. Add arrow key input later
        if ((Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)))
        {
            Debug.Log("Rotate Left");
        } else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotate Right");
        } else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            Debug.Log("Both Side Thrusters Engaged");
            
            
        }
    }

   
}


