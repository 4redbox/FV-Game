using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Studio : MonoBehaviour
{
    private int NextScene;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Scene scene = SceneManager.GetActiveScene();

            Debug.Log("Active scene: " + scene);

            NextScene = SceneManager.GetActiveScene().buildIndex + 1;

            Debug.Log("Next scene: " + NextScene);


            SceneManager.LoadScene(NextScene);
        }
    }
}
