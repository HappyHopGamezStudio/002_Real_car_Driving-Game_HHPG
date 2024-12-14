using UnityEngine;

public class CarSpriteController : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    

    private void Awake()
    {
        // Ensure sprite renderer is assigned, either in inspector or find it on start
        if (spriteRenderer == null)
        {
            spriteRenderer = transform.Find("Player position").GetComponent<SpriteRenderer>();
        }
        else
        {
            Debug.LogWarning("CarSpriteController: SpriteRenderer or CarSprite not assigned.");
        }
    }

    // Method to update the sprite, in case you want to change it later dynamically
    public void UpdateSprite(Sprite newSprite)
    {
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
        }
        else
        {
            Debug.LogWarning("CarSpriteController: SpriteRenderer or newSprite is null.");
        }
    }
}