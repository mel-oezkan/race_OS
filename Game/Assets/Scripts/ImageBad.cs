using UnityEngine;

public class ImageBad : MonoBehaviour
{
    //Hides image untill it becomes relevant
    private void Awake()
    {
        HideImage();
    }

    //Shows image
    public void ShowImage()
    {
        gameObject.SetActive(true);
    }

    //Hides image
    public void HideImage()
    {
        gameObject.SetActive(false);
    }
}
