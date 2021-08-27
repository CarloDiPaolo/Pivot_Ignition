using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingPosition;
    public Vector3 movementVector;
    //[SerializeField] [Range(0, 1)] float movementFactor;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
