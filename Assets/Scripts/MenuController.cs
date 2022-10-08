using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menuScreen;
    [SerializeField] GameObject optionsScreen;
    [SerializeField] CustomPacks customPacks;
    [SerializeField] GameObject skinOptions;

    private void Awake()
    {
        ScoreRecord.LoadHighScores();
        menuScreen.SetActive(true);
        optionsScreen.SetActive(false);
        UpdateSkinDisplay(customPacks.CurrentSkin);
    }

    public void StartGame()
    {
        ScoreRecord.totalSeconds = 0;
        SceneManager.LoadScene("Level 1");
    }

    public void OpenOptionsMenu()
    {
        menuScreen.SetActive(false);
        optionsScreen.SetActive(true);
    }

    public void ExitOptionsMenu()
    {
        menuScreen.SetActive(true);
        optionsScreen.SetActive(false);
        PlayerPrefs.Save();
    }

    public void EnterScoresMenu()
    {
        SceneManager.LoadScene("Score Screen");
    }

    public void ResetScores()
    {
        ScoreRecord.ResetHighScores();
    }


    public void PreviousSkin()
    {
        customPacks.DecrementSkinIndex();
        UpdateSkinDisplay(customPacks.CurrentSkin);
    }

    public void NextSkin()
    {
        customPacks.IncrementSkinIndex();
        UpdateSkinDisplay(customPacks.CurrentSkin);
    }

    private void UpdateSkinDisplay(Skin skin)
    {
        Transform skinImages = skinOptions.transform.Find("SkinImages");
        skinImages.GetChild(0).GetComponent<Image>().sprite = skin.redBlockSkin;
        skinImages.GetChild(1).GetComponent<Image>().sprite = skin.greenBlockSkin;
        skinImages.GetChild(2).GetComponent<Image>().sprite = skin.yellowBlockSkin;
        skinImages.GetChild(3).GetComponent<Image>().sprite = skin.blueBlockSkin;
        skinImages.GetChild(4).GetComponent<Image>().sprite = skin.redStaticBlockSkin;
        skinImages.GetChild(5).GetComponent<Image>().sprite = skin.greenStaticBlockSkin;
        skinImages.GetChild(6).GetComponent<Image>().sprite = skin.yellowStaticBlockSkin;
        skinImages.GetChild(7).GetComponent<Image>().sprite = skin.blueStaticBlockSkin;

        Transform skinPackName = skinOptions.transform.Find("Skin Pack Name");
        skinPackName.GetComponent<TMP_Text>().text = skin.skinPackName;
    }

    public void QuitGame()
    {
            // save any game data here
    #if UNITY_EDITOR
            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
            UnityEditor.EditorApplication.isPlaying = false;
    #else
             Application.Quit();
    #endif
    }
}
