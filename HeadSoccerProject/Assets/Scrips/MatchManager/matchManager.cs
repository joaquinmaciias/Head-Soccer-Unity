using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class matchManager : MonoBehaviour
{

    public static matchManager instance;

    public GameObject ball;         // Needed to stop -> GameController
    public GameObject player1;      // Needed to stop -> GameCOntroller
    public GameObject player2;      // Needed to stop -> GameController
    public GameObject pauseMenu ;   // A PauseMenu
    public GameObject preGameMenu ; // A Pregamemenu
    public GameObject extraTimeMenu ;   // A extraTimeMenu
    public GameObject endGameMenu ;     // A endGameMenu
    public GameObject trophy ;          // A endGameMenu
    public GameObject scoreboard ;      // A endGameMenu
    public GameObject goalAnimation;

    public Vector3 positionPlayer1 = new Vector3(-9f, -3.5f, 0f);       // A GameController
    public Vector3 positionPlayer2 = new Vector3(9f, -3.5f, 0f);        // A GameController
    public Vector3 positionBall = new Vector3(0f, 0f, 0f);              // A GameController
        
    public bool freezeObjects = true;       // A GameController                              
    private Vector3 storedVelocity;         // A GameController
    private float storedAngularVelocity;    // A GameController
        
    public Rigidbody2D ballRigidBody;       // A GameController
    public Rigidbody2D player1RigidBody;    // A GameController
    public Rigidbody2D player2RigidBody;    // A GameController

    [Header("Dependencies")]
    public TextMeshProUGUI textTime;        // A GameController
    public GameObject[] scorePlayer1;       // A GameController
    public GameObject[] scorePlayer2;       // A GameController
    public List<TextMeshProUGUI> scoresText;    // A GameController

    public float timeDiff;                  // A gamecontroller
    public List<int> score = new List<int>() { 0, 0 };                 // A gamecontroller
    public List<bool> isScore = new List<bool>() { false, false };              // A gamecontroller
    public List<bool> stateManager = new List<bool>(6) { false, false, false, true, false, false };     // [isGame, isPause, isGoal, isPreGame, isExtraTime, isEndGame]     // A gamecontroller
    public bool timeRunning;                // A gamecontroller

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        ball = GameObject.FindGameObjectWithTag("Ball");
        pauseMenu = GameObject.FindGameObjectWithTag("pauseMenu");
        preGameMenu = GameObject.FindGameObjectWithTag("preGameMenu");
        extraTimeMenu = GameObject.FindGameObjectWithTag("extraTimeMenu");
        endGameMenu = GameObject.FindGameObjectWithTag("endGameMenu");
        trophy = GameObject.FindGameObjectWithTag("trophy");
        scoreboard = GameObject.FindGameObjectWithTag("scoreboard");
        goalAnimation = GameObject.FindGameObjectWithTag("goalAnimation");

        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        player1RigidBody = player1.GetComponent<Rigidbody2D>();
        player2RigidBody = player2.GetComponent<Rigidbody2D>();


        // Lets add the TextMeshComponents to the scoresText list

        scorePlayer1 = GameObject.FindGameObjectsWithTag("ScoreP1");
        scorePlayer2 = GameObject.FindGameObjectsWithTag("ScoreP2");

        foreach (var obj in scorePlayer1)
        {
            TextMeshProUGUI compText = obj.GetComponent<TextMeshProUGUI>();
            scoresText.Add(compText);
        }

        foreach (var obj in scorePlayer2)
        {
            TextMeshProUGUI compText = obj.GetComponent<TextMeshProUGUI>();
            scoresText.Add(compText);
        }



    }

    // Start is called before the first frame update
    void Start()
    {
        Play();
    }

    void Play()
    {
        timeDiff = 10f;
        textTime.text = timeDiff.ToString();
        timeRunning = false;
        player1.SetActive(true);
        player2.SetActive(true);
        
        
        goalAnimation.SetActive(false);
        showPreGame(true);
        showPauseMenu(false);
        showExtraTimeMenu(false);
        showEndGameMenu(false);


    }

    public void Update()
    {
        //updateScores();

        if (stateManager[3])        // PreGame phase
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                timeRunning = true;
                stateManager[3] = false;
                stateManager[0] = true;

                showPreGame(false);

                MoveInitialPositions();

                // Cambiar escalas



            }
        }

        else if (stateManager[5])           // EndGame 
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                backToMenu();
            }
        }

        // Any gamePhase
        else
        {
            if (stateManager[1])    // Pause the Game
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    stateManager[1] = false;

                    showPauseMenu(false);
                    stopGame(false);


                    if (stateManager[0])
                    {
                        timeRunning = true;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    // Logic for going back to main menu
                }
            }

            else if (stateManager[2])   // There is a goal
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    showGoalAnimation(false);
                    stateManager[2] = false;        // End goal Animation

                    if (stateManager[0])            // The game continues and no goals
                    {
                        timeRunning = true;
                        isScore = new List<bool>() { false, false };         // We reset to zero

                        // The player and ball need to be moved to initial position

                        MoveInitialPositions();
                    }
                    else if (stateManager[4])       // The game ends
                    {
                        stateManager[5] = true;
                        stateManager[4] = false;

                        showEndGameMenu(true);
                    }
                }
            }

            else
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    stateManager[1] = true;
                    timeRunning = false;
                    // Activate pause menu objects

                    showPauseMenu(true);
                    stopGame(true);
                }

                if (isScore[0])
                {
                    // Goal for team 1
                    stateManager[2] = true;
                    score[0]++;
                }
                else if (isScore[1])
                {
                    // Goal for team 2
                    stateManager[2] = true;
                    score[1]++;
                }

                if (stateManager[2])
                {
                    timeRunning = false;
                    showGoalAnimation(true);

                }
                else
                {
                    if (stateManager[0])
                    {
                        if (timeDiff - Time.deltaTime < 0)      // We check if the next update will be below 0
                        {
                            stateManager[0] = false;
                            timeRunning = false;
                            stopGame(true);

                            if (score[0] == score[1])
                            {
                                if (Input.GetKeyDown(KeyCode.Return))
                                {
                                    stateManager[4] = true;
                                    textTime.text = "E.T";
                                    // Activate pause menu objects

                                    stopGame(false);
                                    MoveInitialPositions();
                                }
                                else
                                {
                                    showExtraTimeMenu(true);
                                }
                                // Logic for ExtraTime objects and conditions
                            }
                            else
                            {
                                stateManager[5] = true;
                                showEndGameMenu(true);
                            }
                        }
                        else
                        {

                            timeDiff -= Time.deltaTime;

                            int seconds = Mathf.FloorToInt(timeDiff);

                            textTime.text = seconds.ToString();

                        }
                    }
                }
            }
        }
    }






    public void MoveInitialPositions()
    {
        player1.transform.position = positionPlayer1;
        player2.transform.position = positionPlayer2;
        ball.transform.position = positionBall;
        ballRigidBody.velocity = new Vector3(0, 0, 0);
        ballRigidBody.angularVelocity = 0f;
    }
    
    public void showPreGame(bool action)
    {
        preGameMenu.SetActive(action);

        stopGame(action);
        ball.SetActive(!action);

        if (!action)
        {
            player1.transform.localScale = new Vector3(0.65f, 0.65f, 1f);
            player2.transform.localScale = new Vector3(0.65f, 0.65f, 1f);
        }
    }

    public void showPauseMenu(bool action)
    {
        pauseMenu.SetActive(action);
    }

    public void showGoalAnimation(bool action)
    {

    }

    public void showExtraTimeMenu(bool action)
    {
        extraTimeMenu.SetActive(action);
    }

    public void showEndGameMenu(bool action)
    {
        scoreboard.SetActive(!action);
        endGameMenu.SetActive(action);

        trophy.SetActive(action);

        // We give the trophy to the winning player

        if (score[0] < score[1])
        {
            trophy.transform.position = new Vector3(4.23f, -3.6f, 0f);
        }
        else
        {
            trophy.transform.position = new Vector3(-5.13f, -3.5f, 0f);
        }
        

        if (action)
        {
            ball.SetActive(!action);

            // we move player positions and change scales

            player1.transform.position = new Vector3(-6.5f, -2.6f, -5f);
            player2.transform.position = new Vector3(7.47f, -2.6f, -5f);
            player1.transform.localScale = new Vector3(1f, 1f, 1f);
            player2.transform.localScale = new Vector3(1f, 1f, 1f);

            freezeObjects = true;
            player1RigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            player2RigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        
    }   

    public void backToMenu()
    {

    }

    public void updateScores()
    {
        // Scores text is of length 4, being the 2 first elements of P1 and the last 2 of P2

        scoresText[0].text = score[0].ToString();
        scoresText[1].text = score[0].ToString();
        scoresText[2].text = score[1].ToString();
        scoresText[3].text = score[1].ToString();
    }

    public void stopGame(bool action)
    {
        if (action)
        {
            freezeObjects = action;
            storedVelocity = ballRigidBody.velocity;
            storedAngularVelocity = ballRigidBody.angularVelocity;
            ballRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            player1RigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            player2RigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            freezeObjects = !action;
            ballRigidBody.constraints = RigidbodyConstraints2D.None;
            ballRigidBody.velocity = storedVelocity;
            ballRigidBody.angularVelocity = storedAngularVelocity;
            player1RigidBody.constraints = RigidbodyConstraints2D.None;
            player2RigidBody.constraints = RigidbodyConstraints2D.None;
        }
    }



    

    
}