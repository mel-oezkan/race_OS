using UnityEngine;

public class ImageGood : MonoBehaviour
{
    private void Awake()
    {
        HideImage();
    }

    public void ShowImage1()
    {
        gameObject.SetActive(true);
    }

    public void HideImage()
    {
        gameObject.SetActive(false);
    }
}
