using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    // Activates the pause menu
    public void Setup() {
        gameObject.SetActive(true);
    }
    
    //Restarts the game
    public void RestartButton() {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name ); //Loads the currently active scene
    }

    public void PauseButton()
    {

        Application.Quit();

    }

}
