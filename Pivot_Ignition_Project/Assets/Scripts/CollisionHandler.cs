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
    public AudioClip crash_SFX;
    public AudioClip success_SFX;
    public AudioClip yay;
    public ParticleSystem crash_VFX;
    public ParticleSystem success_VFX;

    public float loadDelay = 2f;
    bool isTransitioning = false;



    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerAudioSource = GetComponent<AudioSource>();

    }
    void OnCollisionEnter(Collision collision)
    {
        if (!isTransitioning){
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
    }

    void SuccessSequence()
    {
        isTransitioning = true;
        playerAudioSource.Stop();
        Invoke("LoadNextLevel", loadDelay);
        Invoke("PlayCheer", 0.5f);
        Debug.Log("SUCCESS");
        playerAudioSource.PlayOneShot(success_SFX);
        
        
    }

    void CrashSequence()
    {
        isTransitioning = true;
        playerAudioSource.Stop();
        GetComponent<Player_Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
        playerAudioSource.PlayOneShot(crash_SFX);
        
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        isTransitioning = true;
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
