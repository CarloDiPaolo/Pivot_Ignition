using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    private Vector3 startingPosition;
    public Vector3 movementVector;
    [SerializeField] [Range(0, 1)] float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        Debug.Log(startingPosition);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(period == 0f) {return;}
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSineWave = Mathf.Sin(cycles * tau);

        //Debug.Log(rawSineWave);

        movementFactor= (rawSineWave + 1f)/ 2f;

        

        Vector3 offset = movementVector * movementFactor;

        transform.position = startingPosition + offset;
    }
}
