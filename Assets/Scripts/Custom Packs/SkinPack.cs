
using UnityEngine;
[CreateAssetMenu(fileName = "New Skin Pack", menuName = "Custom Packs/Create New Skin Pack")]
public class SkinPack : ScriptableObject
{
    [SerializeField] string skinPackName;
    [SerializeField] string artist;

    [SerializeField] Sprite redBlockSkin;
    [SerializeField] Sprite greenBlockSkin;
    [SerializeField] Sprite yellowBlockSkin;
    [SerializeField] Sprite blueBlockSkin;

    [SerializeField] Sprite redStaticBlockSkin;
    [SerializeField] Sprite greenStaticBlockSkin;
    [SerializeField] Sprite yellowStaticBlockSkin;
    [SerializeField] Sprite blueStaticBlockSkin;
    public string SkinPackName { get => skinPackName; }
    public string Artist { get => artist; }

    public Sprite RedBlockSkin { get => redBlockSkin; }
    public Sprite GreenBlockSkin { get => greenBlockSkin; }
    public Sprite YellowBlockSkin { get => yellowBlockSkin; }
    public Sprite BlueBlockSkin { get => blueBlockSkin; }

    public Sprite RedStaticBlockSkin { get => redStaticBlockSkin; }
    public Sprite GreenStaticBlockSkin { get => greenStaticBlockSkin; }
    public Sprite YellowStaticBlockSkin { get => yellowStaticBlockSkin; }
    public Sprite BlueStaticBlockSkin { get => blueStaticBlockSkin; }
}
