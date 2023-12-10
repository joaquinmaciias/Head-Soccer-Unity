using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class GameObjectsManager : MonoBehaviour
{
    private static GameObjectsManager instance;

    public GameObject ball;
    public GameObject player1;
    public GameObject player2;
    public GameObject pauseMenu;
    public GameObject preGameMenu;
    public GameObject extraTimeMenu;
    public GameObject endGameMenu;
    public GameObject trophy;
    public GameObject scoreboard;
    public GameObject goalAnimation;

    private Vector3 positionPlayer1 = new Vector3(-9f, -3.5f, 0f);
    private Vector3 positionPlayer2 = new Vector3(9f, -3.5f, 0f);
    private Vector3 positionBall = new Vector3(0f, 0f, 0f);
    public TextMeshProUGUI textTime;

    private Rigidbody2D ballRigidBody;
    private Rigidbody2D player1RigidBody;
    private Rigidbody2D player2RigidBody;
    private Vector3 storedVelocity;
    private float storedAngularVelocity;
    public GameController gameController;

    public ScoreManager scoreManager;

    public List<int> score;

    public static GameObjectsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameObjectsManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("GameObjectsManager");
                    instance = singletonObject.AddComponent<GameObjectsManager>();

                }

                instance.Initialize();

            }

            return instance;
        }
    }

    private void Awake()
    {
        scoreManager = ScoreManager.Instance;

        // Lets do DontDestroyOnLoad on all the GameObjectsManager

    }

    public void Initialize()
    {
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
        textTime = GameObject.FindGameObjectWithTag("timer").GetComponent<TextMeshProUGUI>();

        ballRigidBody = ball.GetComponent<Rigidbody2D>();
        player1RigidBody = player1.GetComponent<Rigidbody2D>();
        player2RigidBody = player2.GetComponent<Rigidbody2D>();

        gameController = FindObjectOfType<GameController>();

    }

    public void stopGame(bool action)
    {

        if (action)
        {
            gameController.isGamePaused = true;
            storedVelocity = ballRigidBody.velocity;
            storedAngularVelocity = ballRigidBody.angularVelocity;
            ballRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            player1RigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            player2RigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            gameController.isGamePaused = false;
            ballRigidBody.constraints = RigidbodyConstraints2D.None;
            ballRigidBody.velocity = storedVelocity;
            ballRigidBody.angularVelocity = storedAngularVelocity;
            player1RigidBody.constraints = RigidbodyConstraints2D.None;
            player2RigidBody.constraints = RigidbodyConstraints2D.None;
        }

    }

    public void moveInitialPositions()
    {
        player1.transform.position = positionPlayer1;
        player2.transform.position = positionPlayer2;
        ball.transform.position = positionBall;
        ballRigidBody.velocity = new Vector3(0, 0, 0);
        ballRigidBody.angularVelocity = 0f;
    }

    public void ChangeTime(string time)
    {
        textTime.text = time;
    }
}

    // These methods manage the states of the game
    /*
    public void ChangeState(IStateController newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();

            // Print

            Debug.Log("Exiting state: " + currentState);

        }

        currentState = newState;
        currentState.EnterState();
    }

    public IStateController GetCurrentState()
    {
        return currentState;
    }
    
}
    */
