using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour
{
    static GameController _instance;


    private CommandHandler commandHandler;
    private ScoreManager scoreManager;
    private GameObjectsManager gameObjectManager;
    StateManager stateManager = StateManager.GetInstance();

    public bool extraTime = false;
    public bool endGame = false;
    public float timeDiff;
    private List<int> score;

    public bool isGamePaused = true;

    void Awake()
    {
        stateManager.ChangeState(new PreGameState());
        
        if (_instance == null)
        {
            _instance = this;
        }

        // Set the current state

        commandHandler = FindObjectOfType<CommandHandler>();

        gameObjectManager = GameObjectsManager.Instance;

        scoreManager = ScoreManager.Instance;

        if (PreGameData.GameMode is ("PvP" or "Arcade"))
        {
            timeDiff = 60f;
        }
        else
        {
            timeDiff = 40f;
        }
        

        stateManager.ChangeState(new PreGameState());
    }

    void Update()
    {
        // Detect different inputs
        if (Input.GetKeyDown(KeyCode.Return))
        {
            commandHandler.HandleInput(KeyCode.Return);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            commandHandler.HandleInput(KeyCode.Escape);
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            commandHandler.HandleInput(KeyCode.P);
        }

        // We control the time

        if (!isGamePaused)
        {
            if (!extraTime)
            {
                if (timeDiff - Time.deltaTime < 0)      // We check if the next update will be below 0 -> match finished
                {
                    score = scoreManager.GetScore();

                    if (score[0] == score[1])        // If it's a draw, we go to extra time
                    {
                        extraTime = true;
                        stateManager.ChangeState(new ExtraTimeState());

                        gameObjectManager.ChangeTime("SD");
                    }
                    else
                    {
                        endGame = true;
                        stateManager.ChangeState(new EndGameState());
                    }
                }
                else
                {

                    timeDiff -= Time.deltaTime;

                    int seconds = Mathf.FloorToInt(timeDiff);


                    gameObjectManager.ChangeTime(seconds.ToString());

                }
            }
        }

        
    }
}

