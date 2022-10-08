
using UnityEngine;
[CreateAssetMenu(fileName = "NewSkin", menuName = "Skins/Create New Skin")]
public class Skin : ScriptableObject
{
    public string skinPackName;

    public Sprite redBlockSkin;
    public Sprite greenBlockSkin;
    public Sprite yellowBlockSkin;
    public Sprite blueBlockSkin;

    public Sprite redStaticBlockSkin;
    public Sprite greenStaticBlockSkin;
    public Sprite yellowStaticBlockSkin;
    public Sprite blueStaticBlockSkin;
}
