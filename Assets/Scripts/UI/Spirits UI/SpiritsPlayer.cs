using UnityEngine;

public class SpiritsPlayer
{
    public SpiritInfo spiritInfo; 
    public int level = 1;    
    public int affection = 0; 
    public int selectedAttack1 = 0;
    public int selectedAttack2 = 0;

    public SpiritsPlayer(SpiritInfo info)
    {
        spiritInfo = info;
        selectedAttack1 = 0;
        selectedAttack2 = (info.attacks != null && info.attacks.Length > 1) ? 1 : 0;
    }
}
