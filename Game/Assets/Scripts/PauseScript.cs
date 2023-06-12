using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseScript : MonoBehaviour
{
   
    public void Setup() {
        gameObject.SetActive(true);
    }


    public void ResumeButton() {
        gameObject.SetActive(false);
    }

    public void RestartButton() {
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    } 

}
