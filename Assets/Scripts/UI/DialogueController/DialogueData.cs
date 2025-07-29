using Magic.Inventory;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Magic/Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public string[] frases;
    public bool autoAdvance;
    public bool hasReward;
    public Item rewardItem;
    public int rewardAmount = 1;  
}
