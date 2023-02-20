using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Copyrights © Slver Studios 2020 All rights reserved.

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public static bool GamePauseEnabled = false;
    public static bool Pausepls = false;
    

    public GameObject PauseMenuUI;

    private void Start()
    {

        PauseMenuUI.SetActive(false);
        GameIsPaused = false;
        GamePauseEnabled = false;
    }

    //Update is called once per frame
    void Update()
    {

        //Debug.Log("GamePauseEnabled: " + GamePauseEnabled);
        //Debug.Log("GameIsPaused: " + GameIsPaused);

         if (GamePauseEnabled)
        {
            if (GameIsPaused)
            {
                //Debug.Log("Inside Game Is Paused");
                Resume();
            }
            else
            {
                //Debug.Log("Inside Else");
                Pause();
            }

        }
   
    }
    

    public void EnablePauseMenu()
    {
        Debug.Log("Inside Enable PauseMenu");
        GamePauseEnabled = true;
        GameIsPaused = false;
        PauseMenuUI.SetActive(true);


    }

    public void Resume()
    {
        Debug.Log("Inside Resume()");

        PauseMenuUI.SetActive(false);

	    Time.timeScale = 1f;
        GameIsPaused = true;
        GamePauseEnabled = false;
        FindObjectOfType<Playercontrols>().PassPause("false");
    }

    void Pause()
    {

        //Debug.Log("Inside Paue()");
        //Time.timeScale = 0f;
        GameIsPaused = false;
        GamePauseEnabled = true;

        FindObjectOfType<Playercontrols>().PassPause("true");

    }

    public void LoadMenu()
    {
        Debug.Log("Inside LoadMenu()");

        Time.timeScale = 1f;
        SceneManager.LoadScene("Titel");
    }
    
    public void QuitGame()
    {
        Debug.Log("Inside Quit()");

        Debug.Log("Quit in MainMenu");
        Application.Quit();
    }

}
