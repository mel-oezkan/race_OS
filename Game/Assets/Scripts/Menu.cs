using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnQuitButton()
    {
        #if UNITY_EDITOR
                        // Code to handle quitting in the Unity Editor (optional)
                        UnityEditor.EditorApplication.isPlaying = false;
        #else
                // Code to handle quitting in a built version
                Application.Quit();
        #endif
    }
}
