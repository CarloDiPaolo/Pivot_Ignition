using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_Application : MonoBehaviour
{
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
            Debug.Log("Quit Apllication");
        }
    }
}
