using Magic.UI;
using UnityEngine;

[CreateAssetMenu(menuName = "Magic/Spirit Information")]
public class SpiritInfo : ScriptableObject
{
    public string id;
    public string spiritName;
    public string powerName;
    public Sprite icon;
    public Sprite portrait;
    [TextArea] public string story;

    public Attack[] attacks;

    public Sprite heartSprite;
    public Sprite starsSprite;
}

[System.Serializable]
public class Attack
{
    public string name;
    [TextArea] public string description;
    public Sprite icon;
}


