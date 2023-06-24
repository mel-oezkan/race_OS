using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CarControls carControls;
    public FinishLine finishLine;
    GameObject[] pauseObjects;
    GameObject[] finishObjects;
    public bool isFinished = false;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;

        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");            //gets all objects with tag ShowOnPause
        finishObjects = GameObject.FindGameObjectsWithTag("ShowOnFinish");          //gets all objects with tag ShowOnFinish

        hidePaused();
        hideFinished();
    }

    // Update is called once per frame
    void Update()
    {
        //uses the p button to pause and unpause the game
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

        //shows finish gameobjects if player is dead and timescale = 0
        if (isFinished == true)
        {
            showFinished();
        }
    }

    //Reloads the Level
    public void Reload()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    //controls the pausing of the scene
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

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(true);
        }
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        foreach (GameObject g in pauseObjects)
        {
            g.SetActive(false);
        }
    }

    //hides objects with ShowOnFinish tag
    public void hideFinished()
    {
        foreach (GameObject g in finishObjects)
        {
            g.SetActive(false);
        }
    }

    //shows objects with ShowOnFinish tag
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

    //loads inputted level
    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }
}
