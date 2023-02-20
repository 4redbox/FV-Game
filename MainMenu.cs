using UnityEngine;
using UnityEngine.SceneManagement;

// Copyrights © Slver Studios 2020 All rights reserved.

public class MainMenu : MonoBehaviour 

{
    private void Start()
    {
        Time.timeScale = 1f;

    }

    private int NextScene;

    public void PlayGame () 
    {
        Time.timeScale = 1f;

        NextScene = SceneManager.GetActiveScene().buildIndex + 1;

        Debug.Log("NextScene: " + NextScene);

        SceneManager.LoadScene(NextScene);
    }
}