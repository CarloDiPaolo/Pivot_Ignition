using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    int nextSceneIndex;
    private AudioSource playerAudioSource;
    public AudioClip crash;
    public AudioClip success;
    public AudioClip yay;
    public float loadDelay = 2f;


    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerAudioSource = GetComponent<AudioSource>();

    }
    void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Safe Collision");
                break;
            case "Finish":
                
                SuccessSequence();
                break;
            case "Obstacle":
                
                break;
            default:
                CrashSequence();
                break;

        }
    }

    void SuccessSequence()
    {
        Invoke("LoadNextLevel", loadDelay);
        Invoke("PlayCheer", 0.5f);
        Debug.Log("SUCCESS");
        playerAudioSource.PlayOneShot(success);
        
        
    }

    void CrashSequence()
    {
        GetComponent<Player_Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
        playerAudioSource.PlayOneShot(crash);
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

    void PlayCheer ()
    {
        playerAudioSource.PlayOneShot(yay);
    }

    
}
