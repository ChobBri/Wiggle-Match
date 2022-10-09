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
    [SerializeField] GameObject musicOptions;
    [SerializeField] MusicPlayer musicPlayer;

    int musicIndex = 0;
    const int MUSIC_MAX_INDEX = 3;

    private void Awake()
    {
        ScoreRecord.LoadHighScores();
        menuScreen.SetActive(true);
        optionsScreen.SetActive(false);
        UpdateSkinDisplay(customPacks.CurrentSkin);
        UpdateMusicDisplay(customPacks.CurrentMusic);
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
        musicPlayer.Pause();
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

    public void PreviousMusic()
    {
        customPacks.DecrementMusicIndex();
        musicPlayer.Pause();
        musicIndex = 0;
        UpdateMusicDisplay(customPacks.CurrentMusic);
    }

    public void NextMusic()
    {
        customPacks.IncrementMusicIndex();
        musicPlayer.Pause();
        musicIndex = 0;
        UpdateMusicDisplay(customPacks.CurrentMusic);
    }

    public void PlayMusic()
    {
        switch (musicIndex)
        {
            case 0:
                musicPlayer.SetBGMClip(customPacks.CurrentMusic.BGM1);
                break;
            case 1:
                musicPlayer.SetBGMClip(customPacks.CurrentMusic.BGM2);
                break;
            case 2:
                musicPlayer.SetBGMClip(customPacks.CurrentMusic.BGM3);
                break;
        }
        musicIndex++;
        musicIndex %= MUSIC_MAX_INDEX;
    }

    private void UpdateMusicDisplay(MusicPack music)
    {
        Transform skinPackName = musicOptions.transform.Find("Music Pack Name");

        skinPackName.GetComponent<TMP_Text>().text = $"{music.MusicPackName}{(music.MusicPackName.Contains(music.Artist + "'s")? "" : $" - {music.Artist}")}";
    }

    private void UpdateSkinDisplay(SkinPack skin)
    {
        Transform skinImages = skinOptions.transform.Find("SkinImages");
        skinImages.GetChild(0).GetComponent<Image>().sprite = skin.RedBlockSkin;
        skinImages.GetChild(1).GetComponent<Image>().sprite = skin.GreenBlockSkin;
        skinImages.GetChild(2).GetComponent<Image>().sprite = skin.YellowBlockSkin;
        skinImages.GetChild(3).GetComponent<Image>().sprite = skin.BlueBlockSkin;
        skinImages.GetChild(4).GetComponent<Image>().sprite = skin.RedStaticBlockSkin;
        skinImages.GetChild(5).GetComponent<Image>().sprite = skin.GreenStaticBlockSkin;
        skinImages.GetChild(6).GetComponent<Image>().sprite = skin.YellowStaticBlockSkin;
        skinImages.GetChild(7).GetComponent<Image>().sprite = skin.BlueStaticBlockSkin;

        Transform skinPackName = skinOptions.transform.Find("Skin Pack Name");
        skinPackName.GetComponent<TMP_Text>().text = $"{skin.SkinPackName}{(skin.SkinPackName.Contains(skin.Artist + "'s") ? "" : $" - {skin.Artist}")}";
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
