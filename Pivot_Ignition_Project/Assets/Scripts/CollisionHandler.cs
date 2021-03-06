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
    public bool isTransitioning = false;

    bool collisionDisabled = false;

    void Start()
    {
        
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        playerAudioSource = GetComponent<AudioSource>();

        
        

    }
    void Update()
    {
        ProcessDebugInput();
        
    }

    private void ProcessDebugInput()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        } else if (Input.GetKeyDown(KeyCode.C)){
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || collisionDisabled){return;}


        
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
//Remember to stop thrusters VFX on Crash and Success!!
    void SuccessSequence()
    {
        isTransitioning = true;
        playerAudioSource.Stop();
        Invoke("LoadNextLevel", loadDelay);
        Invoke("PlayCheer", 0.5f);
        Debug.Log("SUCCESS");
        playerAudioSource.PlayOneShot(success_SFX);
        success_VFX.Play();
        
        
    }

    void CrashSequence()
    {
        isTransitioning = true;
        playerAudioSource.Stop();
        //GetComponent<Player_Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
        playerAudioSource.PlayOneShot(crash_SFX);
        crash_VFX.Play();
        
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
