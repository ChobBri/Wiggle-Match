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
    [SerializeField] AudioClip gameOverJingle;
    [SerializeField] AudioClip newHighScoreMusic;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
    public void PlayLevelClearJingle(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        audioSource.loop = false;
    }

    public void PlayGameOverJingle()
    {
        audioSource.clip = gameOverJingle;
        audioSource.Play();
        audioSource.loop = false;
    }

    public void PlayNewHighScoreMusic()
    {
        audioSource.clip = newHighScoreMusic;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void SetBGMClip(AudioClip audioClip)
    {
        backgroundMusic = audioClip;
        audioSource.clip = backgroundMusic;
        audioSource.Play();
        audioSource.loop = true;
    }

    void PlayLevelMusic(Scene prevScene, Scene currScene)
    {
        audioSource.clip = backgroundMusic;
        audioSource.Play();
        audioSource.loop = true;
    }

    private void OnDestroy()
    {
    }

}
