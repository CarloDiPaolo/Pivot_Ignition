using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Safe Collision");
                break;
            case "Finish":
                Debug.Log("You Win!");
                break;
            case "Fuel":
                Debug.Log("Fuel Recharged!");
                break;
            default:
                Debug.Log("HULL BREACHED");
                break;

        }
    }
}
