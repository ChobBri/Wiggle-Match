using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPacks : MonoBehaviour
{
    [SerializeField] SkinPack[] skins;
    [SerializeField] MusicPack[] musics;

    public static int skinIndex = 0;
    public static int musicIndex = 0;
    public SkinPack CurrentSkin { 
        get { 
            skinIndex = skinIndex < skins.Length ? skinIndex : 0;
            return skins[skinIndex]; 
        } 
    }

    public void IncrementSkinIndex()
    {
        skinIndex++;
        if (skinIndex >= skins.Length)
        {
            skinIndex = 0;
        }
    }

    public void DecrementSkinIndex()
    {
        skinIndex--;
        if (skinIndex < 0)
        {
            skinIndex = skins.Length - 1;
        }
    }

    public MusicPack CurrentMusic
    {
        get
        {
            musicIndex = musicIndex < musics.Length ? musicIndex : 0;
            return musics[musicIndex];
        }
    }

    public void IncrementMusicIndex()
    {
        musicIndex++;
        if (musicIndex >= musics.Length)
        {
            musicIndex = 0;
        }
    }

    public void DecrementMusicIndex()
    {
        musicIndex--;
        if (musicIndex < 0)
        {
            musicIndex = musics.Length - 1;
        }
    }
}
