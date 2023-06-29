using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageHandler : MonoBehaviour
{
    // simple class to handle the showing and hiding of images

    private void Awake()
    {
        // inital state is hidden
        HideImage();
    }

    // Shows image
    public void ShowImage()
    {
        gameObject.SetActive(true);
    }

    // Hides image
    public void HideImage()
    {
        gameObject.SetActive(false);
    }
}
