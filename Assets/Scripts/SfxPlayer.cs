using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip blockCrash;
    [SerializeField] AudioClip blockClear;
    [SerializeField] AudioClip blockSolidify;
    bool playBlockCrash = false;
    bool playBlockClear = false;
    bool playBlockSolidify = false;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBlockCrashSfx()
    {
        playBlockCrash = true;
    }

    public void PlayBlockClearSfx()
    {
        playBlockClear = true;
    }

    public void PlayBlockSolidfySfx()
    {
        playBlockSolidify = true;
    }

    private void Update()
    {
        if (playBlockCrash)
        {
            audioSource.PlayOneShot(blockCrash);
            playBlockCrash = false;
        }

        if (playBlockClear)
        {
            audioSource.PlayOneShot(blockClear);
            playBlockClear = false;
        }

        if (playBlockSolidify)
        {
            audioSource.PlayOneShot(blockSolidify);
            playBlockSolidify = false;
        }

    }
}
