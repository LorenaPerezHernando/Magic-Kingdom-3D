using UnityEngine;
using UnityEngine.UI;

namespace Magic.UI
{
    public class UIUpgradeIcons : MonoBehaviour
    {
        [SerializeField] private Image[] iconSlots; 

        public void UpdateIcons(Sprite sprite, int activeCount)
        {
            for (int i = 0; i < iconSlots.Length; i++)
            {
                bool active = i < activeCount;
                iconSlots[i].gameObject.SetActive(active);
                if (active) iconSlots[i].sprite = sprite;
            }
        }
    }
}
