using Magic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class SpiritsButton : MonoBehaviour
{
    public SpiritInfo spiritInfo;
    public Image componentImage;     
    public Sprite lockedSprite;

    public void ChangeButtonImage(Sprite unlockedSprite)
    {
            componentImage.sprite = unlockedSprite;
    }

    public void LockedButtonImage()
    {
            componentImage.sprite = lockedSprite;
    }
}
