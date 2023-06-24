using UnityEngine;

public class ImageNaja : MonoBehaviour
{
    private void Awake()
    {
        HideImage();
    }

    public void ShowImage()
    {
        gameObject.SetActive(true);
    }

    public void HideImage()
    {
        gameObject.SetActive(false);
    }
}
