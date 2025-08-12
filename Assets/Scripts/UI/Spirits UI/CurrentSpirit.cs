using Magic.UI;
using UnityEngine;

public class CurrentSpirit : MonoBehaviour
{
    [SerializeField] private UIUpgradeIcons heartsUI; // arrástralos en el inspector
    [SerializeField] private UIUpgradeIcons starsUI;

    private SpiritsPlayer currentSpirit; // si aún usas SpiritInstance, cambia el tipo

    public void SetCurrentSpirit(SpiritsPlayer inst) // o SpiritInstance
    {
        currentSpirit = inst;
        UpdateSpiritIcons();
    }

    public void UpdateSpiritIcons()
    {
        if (currentSpirit == null || currentSpirit.spiritInfo == null) return;

        heartsUI.UpdateIcons(currentSpirit.spiritInfo.heartSprite, currentSpirit.affection);
        starsUI.UpdateIcons(currentSpirit.spiritInfo.starsSprite, currentSpirit.level);
    }
}

