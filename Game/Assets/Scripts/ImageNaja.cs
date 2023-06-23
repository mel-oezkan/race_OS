using UnityEngine;

public class ImageNaja : MonoBehaviour
{
    private void Awake()
    {
        HideImage();
    }

    public void ShowImage1()
    {
        gameObject1.SetActive(true);
    }

    public void HideImage()
    {
        gameObject1.SetActive(false);
    }
}
