using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZL.Core
{
    public class Core : MonoBehaviour
    {
        [SerializeField] GameObject persistentGameObjects;
        MusicPlayer audioManager;
        static GameObject PGOinstance;
        bool paused = false;
        [SerializeField] PuzzleSystem puzzleSystem;

        private void Awake()
        {
            if (PGOinstance == null)
            {
                PGOinstance = Instantiate(persistentGameObjects);
                DontDestroyOnLoad(PGOinstance);
            }
        }

        private void Start()
        {
            audioManager = FindObjectOfType<MusicPlayer>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;
                if (paused)
                {
                    Time.timeScale = 0;
                    audioManager.GetComponent<AudioSource>().Pause();
                }
                else
                {
                    Time.timeScale = 1;
                    audioManager.GetComponent<AudioSource>().Play();
                }
            }


        }
    }
}

