using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelNumber : MonoBehaviour
{
    [SerializeField] TMP_Text levelNumberText;
    const int LVL_OFFSET = 0; // 1 - Extra Scenes (ex. main menu)

    void Start()
    {
        levelNumberText.text = $"Level {SceneManager.GetActiveScene().buildIndex + LVL_OFFSET}";
    }

}
