using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPacks : MonoBehaviour
{
    [SerializeField] Skin[] skins;

    public static int skinIndex = 0;
    public Skin CurrentSkin { 
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

    private void Update()
    {
        
    }
}
