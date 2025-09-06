using Magic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class SpiritsButton : MonoBehaviour
{
    public string spiritId;    
    public Sprite iconImage;     
    public Sprite lockedSprite; 

    public void ChangeButtonImage(Sprite unlockedSprite)
    {
        if (iconImage != null)
            iconImage = unlockedSprite;
    }

    public void LockedButtonImage()
    {
        if (iconImage != null)
            iconImage = lockedSprite;
    }
}
