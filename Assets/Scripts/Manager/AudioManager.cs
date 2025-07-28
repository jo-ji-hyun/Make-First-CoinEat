using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    AudioSource audioSource;

    public AudioClip clip;
    public AudioClip clip2;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;

            audioSource.loop = true;
            audioSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; 
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        stagebgm(scene.name); 
    }

    private void stagebgm(string sceneName)
    {
        AudioClip clipToPlay = null;


        switch (sceneName)
        {
            case "StartScene": 
                clipToPlay = clip;
                break;
            case "MainScene": 
                clipToPlay = clip2;
                break;
            case "MainScene 1": 
                clipToPlay = clip2;
                break;
                // 새로운 씬 추가시 추가해야함

            default: 

                if (audioSource != null && audioSource.isPlaying)
                {
                    audioSource.Stop(); 
                }
                break;
        }

        if (clipToPlay != null)
        {
         
            if (audioSource.clip != clipToPlay || !audioSource.isPlaying)
            {
                audioSource.clip = clipToPlay;
                audioSource.Play();
            }
        }
        else 
        {
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

    }
    public void StopBGM()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
