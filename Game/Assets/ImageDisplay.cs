using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageDisplay : MonoBehaviour
{
    private Image imageComponent;

    private void Awake()
    {
        imageComponent = GetComponent<Image>();
    }

    public void DisplayImage(Sprite image)
    {
        if (imageComponent != null)
        {
            imageComponent.sprite = image;
        }
        else
        {
            Debug.LogWarning("Image component not found.");
        }
    }
}