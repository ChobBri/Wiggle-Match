using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PZL.Core;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip levelClearJingle;
    [SerializeField] AudioClip backgroundMusic;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += PlayLevelMusic;
    }

    void Start()
    {
    }

    public void PlayLevelClearJingle()
    {
        audioSource.clip = levelClearJingle;
        audioSource.Play();
        audioSource.loop = false;
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    void PlayLevelMusic(Scene prevScene, Scene currScene)
    {
        audioSource.clip = backgroundMusic;
        audioSource.Play();
        audioSource.loop = true;
    }

    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= PlayLevelMusic;
    }

}
