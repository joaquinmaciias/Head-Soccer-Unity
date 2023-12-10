using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;


public interface IStateController
{
    void EnterState();
    void ExitState();
}


// We will now define each chold class of IGameState, defining the objects that will be used in each state


public class PreGameState : IStateController
{
    private GameObjectsManager gameObjectsManager;

    public PreGameState()
    {
        gameObjectsManager = GameObjectsManager.Instance;
    }

    public void EnterState()
    {

        // We set active the preGameMenu
        gameObjectsManager.preGameMenu.SetActive(true);
        gameObjectsManager.player1.SetActive(true);
        gameObjectsManager.player2.SetActive(true);

        if (PreGameData.GameMode == "PvP")
        {
            gameObjectsManager.player1.GetComponent<MovementPlayerAI>().enabled = false;
        }
        else
        {
            gameObjectsManager.player1.GetComponent<MovementPlayerLeft>().enabled = false;
        }


        gameObjectsManager.stopGame(true);

        // Lets check we enter here

        // Now we deactivate the rest of the menus

        gameObjectsManager.pauseMenu.SetActive(false);
        gameObjectsManager.extraTimeMenu.SetActive(false);
        gameObjectsManager.endGameMenu.SetActive(false);
        gameObjectsManager.trophy.SetActive(false);
        gameObjectsManager.scoreboard.SetActive(false);
        gameObjectsManager.goalAnimation.SetActive(false);
        gameObjectsManager.ball.SetActive(false);
    }

    public void ExitState()
    {
        // We activate the ball and the scoreboard

        gameObjectsManager.ball.SetActive(true);
        gameObjectsManager.scoreboard.SetActive(true);

        // We deactivate the preGameMenu

        gameObjectsManager.preGameMenu.SetActive(false);

        // We move the ball and the players to their initial positions

        gameObjectsManager.stopGame(false);
        gameObjectsManager.moveInitialPositions();

        // Ajustar escalas de los jugadores

        gameObjectsManager.player1.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        gameObjectsManager.player2.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

    }

}


public class InGameState : IStateController
{
    private GameObjectsManager gameObjectsManager;

    public InGameState()
    {
        gameObjectsManager = GameObjectsManager.Instance;
    }

    public void EnterState()
    {
       gameObjectsManager.stopGame(false);
    }

    public void ExitState()
    {
        gameObjectsManager.stopGame(true);
    }
}


public class PauseState : IStateController
{
    private GameObjectsManager gameObjectsManager;

    public PauseState()
    {
        gameObjectsManager = GameObjectsManager.Instance;
    }

    public void EnterState()
    {
        gameObjectsManager.pauseMenu.SetActive(true);
    }

    public void ExitState()
    {
        gameObjectsManager.stopGame(false);
        gameObjectsManager.pauseMenu.SetActive(false);
    }
}



public class GoalState : IStateController
{
    private GameObjectsManager gameObjectsManager;
    private StateManager stateManager = StateManager.GetInstance();

    public GoalState()
    {
        gameObjectsManager = GameObjectsManager.Instance;
    }

    public void EnterState()
    {
        gameObjectsManager.goalAnimation.SetActive(true);

    }

    public void ExitState()
    {
        gameObjectsManager.stopGame(false);
        gameObjectsManager.goalAnimation.SetActive(false);
        gameObjectsManager.moveInitialPositions();
        
    }
}


public class ExtraTimeState : IStateController
{
    private GameObjectsManager gameObjectsManager;

    public ExtraTimeState()
    {
        gameObjectsManager = GameObjectsManager.Instance;
    }

    public void EnterState()
    {
        gameObjectsManager.extraTimeMenu.SetActive(true);
    }

    public void ExitState()
    {
        gameObjectsManager.stopGame(false);
        gameObjectsManager.extraTimeMenu.SetActive(false);
        gameObjectsManager.moveInitialPositions();

    }
}


public class EndGameState : IStateController
{
    private GameObjectsManager gameObjectsManager;
    private ScoreManager scoreManager;
    private PreGameData preGameData;
    private PostGameData postGameData;
    public Scene gameScene;
    private StateManager stateManager = StateManager.GetInstance();

    public EndGameState()
    {
        gameObjectsManager = GameObjectsManager.Instance;
        scoreManager = ScoreManager.Instance;
    }

    public void EnterState()
    {
        gameObjectsManager.endGameMenu.SetActive(true);
        gameObjectsManager.scoreboard.SetActive(false);
        gameObjectsManager.trophy.SetActive(true);
        gameObjectsManager.stopGame(true);

        if (scoreManager.score[0] > scoreManager.score[1])
        {
            gameObjectsManager.trophy.transform.position = new Vector3(-6.5f, -2.6f, -5f);
        }
        else
        {
            gameObjectsManager.trophy.transform.position = new Vector3(7.47f, -2.6f, -5f);
        }
        // We move the players to their new positions and change scale

        gameObjectsManager.player1.transform.position = new Vector3(-6.5f, -2.6f, -5f);
        gameObjectsManager.player2.transform.position = new Vector3(7.47f, -2.6f, -5f);
        gameObjectsManager.player1.transform.localScale = new Vector3(1f, 1f, 1f);
        gameObjectsManager.player2.transform.localScale = new Vector3(1f, 1f, 1f);

    }

    public void ExitState()
    {
        // Here we need to put the exit game logic
        //gameObjectsManager.endGameMenu.SetActive(false);

        // We edit the PostGameData object with the EXECUTION_ID

        // Lets activate all GO to have them ready for the next game
        /*
        gameObjectsManager.ball.SetActive(true);
        gameObjectsManager.scoreboard.SetActive(true);
        gameObjectsManager.player1.SetActive(true);
        gameObjectsManager.player2.SetActive(true);
        gameObjectsManager.pauseMenu.SetActive(true);
        gameObjectsManager.extraTimeMenu.SetActive(true);
        gameObjectsManager.endGameMenu.SetActive(true);
        gameObjectsManager.trophy.SetActive(true);
        gameObjectsManager.scoreboard.SetActive(true);
        gameObjectsManager.goalAnimation.SetActive(true);
        */

        if (!gameObjectsManager.gameController.endGame)
        {
            CallToSelecInfo.ExecutionID = 3;
            CallToSelecInfo.ResultState = null;

            SceneManager.LoadScene("MainMenu");
            
        }

        // We check the game mode in PreGameData

        else
        {
            if (PreGameData.GameMode == "Survival")
            {
                // We go back to Execution ID 1

                if (scoreManager.score[1] > scoreManager.score[0])
                {
                    // Player has won
                    
                    CallToSelecInfo.ExecutionID = 4;
                }
                else
                {
                    CallToSelecInfo.ExecutionID = 3;
                    CallToSelecInfo.ResultState = "loss";
                }

                
                

            }

            else if (PreGameData.GameMode == "Arcade")
            {
                // We go back to Execution ID 0

                CallToSelecInfo.ExecutionID = 3;

                if (scoreManager.score[1] > scoreManager.score[0])
                {
                    // Player has won
                    CallToSelecInfo.ResultState = "victory";

                }
                else
                {
                    CallToSelecInfo.ResultState = "loss";
                }

            }

            else if (PreGameData.GameMode == "PvP")
            {
                // We go back to Execution ID 2

                CallToSelecInfo.ExecutionID = 3;
                CallToSelecInfo.ResultState = null;
            }

            SceneManager.LoadScene("PlayerSelectMenu");

            // We change state to pregame
        }
    }
}





/*

public class StateController
{
    private StateController() { }

    public static StateController _instance;

    public GameState currentState;

    private void Awake()        // Wato puede ir dentro de GameState?
    {
        //currentState = new GameState.PreGameState();

        _instance = new StateController();
        _instance.currentState = new GameState.PreGameState();

        _instance.currentState.player1 = GameObject.FindGameObjectWithTag("Player1");
        _instance.currentState.player2 = GameObject.FindGameObjectWithTag("Player2");
        _instance.currentState.ball = GameObject.FindGameObjectWithTag("Ball");
        _instance.currentState.pauseMenu = GameObject.FindGameObjectWithTag("pauseMenu");
        _instance.currentState.preGameMenu = GameObject.FindGameObjectWithTag("preGameMenu");
        _instance.currentState.extraTimeMenu = GameObject.FindGameObjectWithTag("extraTimeMenu");
        _instance.currentState.endGameMenu = GameObject.FindGameObjectWithTag("endGameMenu");
        _instance.currentState.trophy = GameObject.FindGameObjectWithTag("trophy");
        _instance.currentState.scoreboard = GameObject.FindGameObjectWithTag("scoreboard");
        _instance.currentState.goalAnimation = GameObject.FindGameObjectWithTag("goalAnimation");

        _instance.currentState.ballRigidBody = currentState.ball.GetComponent<Rigidbody2D>();
        _instance.currentState.player1RigidBody = currentState.player1.GetComponent<Rigidbody2D>();
        _instance.currentState.player2RigidBody = currentState.player2.GetComponent<Rigidbody2D>();

        if(currentState.ball == null)
        {
            Debug.Log("Ball not found2");
        }

        if (_instance.currentState.ball == null)
        {
            Debug.Log("Ball not found3");
        }

    }

    public static StateController GetInstance()
    {
        if (_instance == null)
        {
            _instance = new StateController();
            _instance.currentState = new GameState.PreGameState();
            _instance.currentState.EnterState();
        }
        
        return _instance;
    }

    public abstract class GameState
    {
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

        public Vector3 positionPlayer1 = new Vector3(-9f, -3.5f, 0f);
        public Vector3 positionPlayer2 = new Vector3(9f, -3.5f, 0f);
        public Vector3 positionBall = new Vector3(0f, 0f, 0f);

        public Rigidbody2D ballRigidBody;
        public Rigidbody2D player1RigidBody;
        public Rigidbody2D player2RigidBody;
        private Vector3 storedVelocity;
        private float storedAngularVelocity;

        ScoreManager scoreManager = ScoreManager.GetInstance();
        public List<int> score;

        public GameController gameController;

        public PreGameState preGame;
        public InGameState inGame;
        public PauseState pause;
        public goalState goal;
        public ExtraTimeState extraTime;
        public EndGameState endGame;

        public abstract void EnterState();

        public abstract void ExitState();

        public void StopGame(bool action)
        {

            if (action)
            {
                storedVelocity = ballRigidBody.velocity;
                storedAngularVelocity = ballRigidBody.angularVelocity;
                ballRigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                player1RigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
                player2RigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {

                ballRigidBody.constraints = RigidbodyConstraints2D.None;
                ballRigidBody.velocity = storedVelocity;
                ballRigidBody.angularVelocity = storedAngularVelocity;
                player1RigidBody.constraints = RigidbodyConstraints2D.None;
                player2RigidBody.constraints = RigidbodyConstraints2D.None;
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

        public class PreGameState : GameState
        {

            public override void EnterState()
            {

                if (_instance.currentState.ball == null)
                {
                    Debug.Log("Ball not found");
                }


                // We set active the preGameMenu
                this.preGameMenu.SetActive(true);
                this.player1.SetActive(true);
                this.player2.SetActive(true);
                this.StopGame(true);


                // Now we deactivate the rest of the menus

                this.pauseMenu.SetActive(false);
                this.extraTimeMenu.SetActive(false);
                this.endGameMenu.SetActive(false);
                this.trophy.SetActive(false);
                this.scoreboard.SetActive(false);
                this.goalAnimation.SetActive(false);
            }

            public override void ExitState()
            {
                // We activate the ball and the scoreboard

                this.ball.SetActive(true);
                this.scoreboard.SetActive(true);

                // We deactivate the preGameMenu

                this.preGameMenu.SetActive(false);

                // We move the ball and the players to their initial positions

                this.MoveInitialPositions();
            }
        }

        public class InGameState : GameState
        {

            public override void EnterState()
            {
            }

            public override void ExitState()
            {
                this.StopGame(true);
            }
        }

        public class PauseState : GameState
        {

            public override void EnterState()
            {
                this.pauseMenu.SetActive(true);
            }

            public override void ExitState()
            {
                this.pauseMenu.SetActive(false);
                this.StopGame(false);
            }
        }

        public class goalState : GameState
        {
            public override void EnterState()
            {
                this.goalAnimation.SetActive(true);

            }

            public override void ExitState()
            {
                this.goalAnimation.SetActive(false);
                this.StopGame(false);
                this.MoveInitialPositions();

                if (gameController.extraTime)
                {
                    _instance.currentState = new GameState.EndGameState();
                    _instance.currentState.EnterState();
                    gameController.endGame = true;
                }
                // If in extraTime, we go to endGame



            }
        }

        public class ExtraTimeState : GameState
        {

            public override void EnterState()
            {
                this.extraTimeMenu.SetActive(true);
            }

            public override void ExitState()
            {
                this.extraTimeMenu.SetActive(false);
                this.StopGame(false);
            }
        }

        public class EndGameState : GameState
        {

            public override void EnterState()
            {
                this.endGameMenu.SetActive(true);
                this.scoreboard.SetActive(false);
                this.trophy.SetActive(true);

                // We move the players to their new positions and change scale

                this.player1.transform.position = new Vector3(-6.5f, -2.6f, -5f);
                this.player2.transform.position = new Vector3(7.47f, -2.6f, -5f);
                this.player1.transform.localScale = new Vector3(1f, 1f, 1f);
                this.player2.transform.localScale = new Vector3(1f, 1f, 1f);

            }

            public override void ExitState()
            {
                // Here we need to put the exit game logic
                this.endGameMenu.SetActive(false);


                if (gameController.endGame)
                {
                    // We return certain information
                }
                else
                {
                    // We return certain information
                }
            }
        }

    }



}

*/


