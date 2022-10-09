
using UnityEngine;
[CreateAssetMenu(fileName = "New Music Pack", menuName = "Custom Packs/Create New Music Pack")]
public class MusicPack : ScriptableObject
{
    [SerializeField] string musicPackName;
    [SerializeField] string artist;

    [SerializeField] AudioClip bgm1;
    [SerializeField] AudioClip bgm2;
    [SerializeField] AudioClip bgm3;
    [SerializeField] AudioClip levelClear;
    [SerializeField][Range(0, 10)] float levelClearTime;

    public string MusicPackName { get => musicPackName; }
    public string Artist { get => artist; }
    public AudioClip BGM1 { get => bgm1; }
    public AudioClip BGM2 { get => bgm2; }
    public AudioClip BGM3 { get => bgm3; }
    public AudioClip LevelClear { get => levelClear; }
    public float LevelClearTime { get => levelClearTime; }
}
