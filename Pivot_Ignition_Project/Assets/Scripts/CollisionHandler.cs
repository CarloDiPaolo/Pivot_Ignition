using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    int nextSceneIndex;
     public float loadDelay = 2f;


    void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

    }
    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Safe Collision");
                break;
            case "Finish":
                //Debug.Log("You Win!");
                SuccessSequence();
                break;
            case "Fuel":
                Debug.Log("Fuel Recharged!");
                break;
            default:
                CrashSequence();
                break;

        }
    }

    private void SuccessSequence()
    {
        Invoke("LoadNextLevel", loadDelay);
        
    }

    void CrashSequence()
    {
        GetComponent<Player_Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        nextSceneIndex = currentSceneIndex +1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    
}
