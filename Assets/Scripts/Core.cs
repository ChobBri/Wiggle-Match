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
                    puzzleSystem.gameObject.SetActive(false);
                    audioManager.GetComponent<AudioSource>().Pause();
                }
                else
                {
                    puzzleSystem.gameObject.SetActive(true);
                    audioManager.GetComponent<AudioSource>().Play();
                }
            }


        }
    }
}

