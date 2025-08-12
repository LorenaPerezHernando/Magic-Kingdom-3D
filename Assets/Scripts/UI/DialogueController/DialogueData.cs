using Magic.Inventory;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Magic/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public string[] frases;
    public Sprite imageTalker;
    public bool hasReward;
    public Item rewardItem;
    public int rewardAmount = 1;
    [TextArea] public string rewardText;
}
