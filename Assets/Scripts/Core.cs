using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PZL.Core
{
    public class Core : MonoBehaviour
    {
        [SerializeField] GameObject persistentGameObjects;
        static GameObject PGOinstance;
        
        private void Awake()
        {
            if (PGOinstance == null)
            {
                PGOinstance = Instantiate(persistentGameObjects);
                DontDestroyOnLoad(PGOinstance);
            }
        }
    }
}

