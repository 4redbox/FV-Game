using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


// Copyrights © Slver Studios 2020 All rights reserved.

public class Playercontrols : MonoBehaviour
{
    public Rigidbody2D gamer;
    public GameObject GameOverUI;
    public GameObject LevelUI;

    public GameObject MaskUI;

    public Animator animator;

    public GameObject Infected1;
    public GameObject Infected2;
    public GameObject Infected3;

    public GameObject MaskMsg;
    public GameObject SanitizeMsg;
    public GameObject PoliceMsg;
    public GameObject HitMsg;
    public GameObject VirusMsg;

    public float gamespeed;
    Vector3 startPos;
    public Color OrginalMat;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool onGround;

    public static bool GameIsOver = false;

    public float life;
    public static int infected;
    public static bool IsInfected = false;
    public static bool InfectedOver = false;
    public static bool IsMask = false;

    public static bool Gfirsttime;

    public static float deltaTime = 2f;

    public int Movetimecntr = 0;
    public static bool cantMove = false;

    public static bool LevelIsEnd = false;

    public static bool Increase = true;

    private int NextScene;
    private int CurrentScene;

    private string PlsPause;
    private bool PausefromMenu = false;


    void Start()
    {
        //Debug.Log("Started");

        gamer = GetComponent<Rigidbody2D>();

        startPos = gamer.transform.position;

        OrginalMat = GetComponent<Renderer>().material.color;

        gamespeed = 3;

        LevelIsEnd = false;

        Increase = true;

        life = 3;
        infected = 0;

        cantMove = false;

        Gfirsttime = true;

        Infected1.SetActive(false);
        Infected2.SetActive(false);
        Infected3.SetActive(false);

        MaskUI.SetActive(false);

        CurrentScene = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("CurrentScene: " + CurrentScene);

        NextScene = SceneManager.GetActiveScene().buildIndex + 1;

        PausefromMenu = false;

        //IsInfected = true;


    }

    void Update()
    {
        //Debug.Log("Update PausefromMenu" + PausefromMenu);


        if (PausefromMenu)
        {
            Time.timeScale = 0f;
        }
        else
        {
            //Debug.Log("Inside Update");

            gamer.velocity = new Vector2(gamespeed, gamer.velocity.y);

            onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

         //   Debug.Log("cantMove" + cantMove);
         //   Debug.Log("onGround: " + onGround);

            if (Input.GetMouseButtonDown(0) && onGround && !cantMove)
            {
                //Debug.Log("Inside jump");

                animator.SetBool("IsJump", true);

                gamer.velocity = new Vector2(gamer.velocity.x, 8);

                FindObjectOfType<AudioManager>().Play("Jump");
         
            }
            else
            {
                if (onGround)
                {
                    //Debug.Log("Inside OnGround");
                    animator.SetBool("IsJump", false);

                }
                else
                {
                    //Debug.Log("Inside OnGround else");

                    animator.SetBool("IsJump", true);
                }

            }

            //Debug.Log("OnGround: " + onGround);



            //Debug.Log("Y: " + gamer.position.y);


            if (gamer.position.y < -10 || InfectedOver)
            {
             //   Debug.Log("GameOver Hit");
                cantMove = true;
                Movetimecntr = 0;
                Increase = false;

                if (GameIsOver)
                {
                    Gameover();
                }
                else
                {
                    GameIsOver = true;
                }
            }

            //Debug.Log("cantMove: " + cantMove);
            //Debug.Log("Movetimecntr: " + Movetimecntr);

            if (cantMove && Movetimecntr < 200)
            {
                if (Increase)
                {
                    //Debug.Log("Conunter: " + Movetimecntr);

                    if (Movetimecntr > 50)
                    {
                        transform.GetComponent<Renderer>().material.color = OrginalMat;

                    }

                    if (Movetimecntr > 100)
                    {
                        transform.GetComponent<Renderer>().material.color = Color.red;

                    }

                    if (Movetimecntr > 150)

                    {
                        transform.GetComponent<Renderer>().material.color = OrginalMat;

                    }

                    if (Movetimecntr > 175)
                    {
                        transform.GetComponent<Renderer>().material.color = Color.red;

                    }


                    Movetimecntr += 5;
                }

                Time.timeScale = 0f;
            }
            else
            {
                cantMove = false;
                Time.timeScale = 1f;
            }
        }

    }

    public void OnTriggerEnter2D(Collider2D player)
    {
        Debug.Log(player.gameObject.tag + "script1");

        if (player.gameObject.CompareTag("Virus"))
        {
            MaskMsg.SetActive(false);
            SanitizeMsg.SetActive(false);
            PoliceMsg.SetActive(false);
            HitMsg.SetActive(false);
            VirusMsg.SetActive(true);

            cantMove = true;
            Movetimecntr = 0;

            transform.GetComponent<Renderer>().material.color = Color.red;
            FindObjectOfType<AudioManager>().Play("Virus");


            IsInfected = true;
            if (!IsMask)
            {
                gameObject.tag = "iPlayer";
            }
            
        }

        if (player.gameObject.CompareTag("Sanitize"))
        {
            Debug.Log("Sanitize");

            MaskMsg.SetActive(false);
            SanitizeMsg.SetActive(true);
            PoliceMsg.SetActive(false);
            HitMsg.SetActive(false);
            VirusMsg.SetActive(false);

            transform.GetComponent<Renderer>().material.color = OrginalMat;
            FindObjectOfType<AudioManager>().Play("Cure");

            IsInfected = false;
            gameObject.tag = "Player";
        }

        if (player.gameObject.CompareTag("Police"))
        {

            MaskMsg.SetActive(false);
            SanitizeMsg.SetActive(false);
            PoliceMsg.SetActive(true);
            HitMsg.SetActive(false);
            VirusMsg.SetActive(false);

            gamespeed = 5;
            //    Debug.Log("Police touched");

            if (IsInfected)
            {
                cantMove = true;
                Movetimecntr = 0;

                FindObjectOfType<AudioManager>().Play("Hit");
                infected += 1;
            }
        }

        if (player.gameObject.CompareTag("Human"))
        {
            Debug.Log("Inside Human");
            Debug.Log("IsMask: "+ IsMask);

            if (IsInfected && !IsMask)
            {
                Debug.Log("Infected Human");

                MaskMsg.SetActive(false);
                SanitizeMsg.SetActive(false);
                PoliceMsg.SetActive(false);
                HitMsg.SetActive(true);
                VirusMsg.SetActive(false);

                cantMove = true;
                Movetimecntr = 0;

                FindObjectOfType<AudioManager>().Play("Hit");
                infected += 1;
            }
        }

        if (player.gameObject.CompareTag("Mask"))
        {
            FindObjectOfType<AudioManager>().Play("Cure");

            IsInfected = false;
            IsMask = true;
            gameObject.tag = "Player";

            MaskMsg.SetActive(true);
            SanitizeMsg.SetActive(false);
            PoliceMsg.SetActive(false);
            HitMsg.SetActive(false);
            VirusMsg.SetActive(false);

            MaskUI.SetActive(true);

        }
            if (player.gameObject.CompareTag("LevelEnd"))
        {
            //    Debug.Log("Infected: " + infected);
            //    Debug.Log("life: " + life);

            infected = 0;
            InfectedOver = false;

            MaskMsg.SetActive(false);
            SanitizeMsg.SetActive(false);
            PoliceMsg.SetActive(false);
            HitMsg.SetActive(false);
            VirusMsg.SetActive(false);


            LevelUI.SetActive(true);
            Time.timeScale = 0f;



            //("Level End Reached");

            cantMove = true;
            Movetimecntr = 0;
            Increase = false;

            LevelIsEnd = true;

            Infected1.SetActive(true);
            Infected2.SetActive(true);
            Infected3.SetActive(true);

            MaskUI.SetActive(false);

            FindObjectOfType<AudioManager>().Play("LevelEnd");



        }

        switch (infected)
        {
            case 3:
         //       Debug.Log("Case3");
                Infected1.SetActive(true);
                Infected2.SetActive(true);
                Infected3.SetActive(true);

                InfectedOver = true;
                break;

            case 2:
        //        Debug.Log("Case 2");
                Infected1.SetActive(true);
                Infected2.SetActive(true);
                Infected3.SetActive(false);
                break;

            case 1:
        //        Debug.Log("Case1");
                Infected1.SetActive(true);
                Infected2.SetActive(false);
                Infected3.SetActive(false);
                break;

            case 0:
        //        Debug.Log("Case 0");

                Infected1.SetActive(false);
                Infected2.SetActive(false);
                Infected3.SetActive(false);
                break;

            default:
                break;
        }

    }

    public void Gameover()
    {
        //Debug.Log("Inside Gameover class");

        infected = 0;
        IsInfected = false;

        MaskMsg.SetActive(false);
        SanitizeMsg.SetActive(false);
        PoliceMsg.SetActive(false);
        HitMsg.SetActive(false);
        VirusMsg.SetActive(false);

        Infected1.SetActive(false);
        Infected2.SetActive(false);
        Infected3.SetActive(false);

        MaskUI.SetActive(false);


        if (Gfirsttime)
        {
            FindObjectOfType<AudioManager>().Play("Gameover");
            Gfirsttime = false;
        }

        GameOverUI.SetActive(true);

        Time.timeScale = 0f;
        GameIsOver = false;

    }

    public void QuitButton()
    {
        Application.Quit();

    }

    public void MainMenu()
    {
        GameOverUI.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Titel");

    }

    public void NextLevel()
    {
        GameOverUI.SetActive(false);
        Time.timeScale = 1f;

        //Debug.Log("NextScene: " + NextScene);

        SceneManager.LoadScene(NextScene);


    }

    public void Retry()
    {
        gamespeed = 3;
        infected = 0;
        InfectedOver = false;

        transform.GetComponent<Renderer>().material.color = OrginalMat;

        gamer.transform.position = startPos;

        Scene scene = SceneManager.GetActiveScene();

        //Debug.Log("Active scene: " + scene);

        SceneManager.LoadScene(scene.name);

        //SceneManager.LoadScene("Game");

        Time.timeScale = 1f;
        GameOverUI.SetActive(false);
        GameIsOver = false;

    }

    public void PassPause(string PlsPause)
    {

        if (PlsPause == "true")
        {
            PausefromMenu = true;
        }
        else
        {
            PausefromMenu = false;
        }
    }

}

// Copyrights © Slver Studios 2020 All rights reserved.