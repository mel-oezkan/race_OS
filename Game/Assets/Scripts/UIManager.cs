using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    //References
    [SerializeField] private FinishLine _finishLine;

    //Variables
    //Boolean Variables
    public bool _isFinished = false;

    //Array Variables
    GameObject[] pauseObjects;
    GameObject[] finishObjects;

    // Initialization 
    void Start()
    {
        // Marker that differenciates between paused and not paused
        Time.timeScale = 1;          

        //gets all objects with tag ShowOnPause
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");            
        
        //gets all objects with tag ShowOnFinish
        finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");          

        hidePaused();            //Hides the pause menu
        hideFinished();          //Hides the finish menu
    }

    //defined the p button to pause and unpause the game
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                hidePaused();
            }
        }

        //shows finish menu if isFinished is true 
        // (which is the case when the car passed the finish line)
        if (_isFinished == true)
        {
            showFinished();
        }
    }


    //Reloads the Level
    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
    }


    public void QuitButton()
    {
        #if UNITY_EDITOR
                // Code to handle quitting in the Unity Editor (optional)
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                // Code to handle quitting in a built version
                Application.Quit();
        #endif
    }

    //Controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    //Shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //Hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //Hides objects with ShowOnFinish tag
    public void hideFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(false);
        }
    }

    //Shows objects with ShowOnFinish tag
    public void showFinished()
    {
        StartCoroutine(ShowFinishedCoroutine());
    }

    private IEnumerator ShowFinishedCoroutine()
    {
        yield return new WaitForSeconds(4f);
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(true);
        }
    }

    //Loads inputted level
    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }
}
