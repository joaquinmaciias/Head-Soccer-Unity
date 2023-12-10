using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommandHandler : MonoBehaviour
{
    // Lets get the currentState

    GameObjectsManager gameObjectsManager;
    StateManager stateManager = StateManager.GetInstance();
    private PostGameData postGameData;

    public void Start()
    {
        if (stateManager != null)
        {
            IStateController currentState = stateManager.GetCurrentState();
            
        }

    }

    public void HandleInput(KeyCode input)
    {

        IStateController currentState = stateManager.GetCurrentState();
        gameObjectsManager = GameObjectsManager.Instance;

        //Print current state

        if (input == KeyCode.Return)
        {

            if (currentState is (PreGameState or PauseState or ExtraTimeState or EndGameState))
            {
                stateManager.ChangeState(new InGameState());

            }
            else if (currentState is GoalState)
            {
                if (gameObjectsManager.gameController.extraTime)   // If we are in extra time, we go to endGame
                {
                    stateManager.ChangeState(new EndGameState());
                    gameObjectsManager.gameController.endGame = true;
                }
                else
                {
                    stateManager.ChangeState(new InGameState());
                }
            }
        }
        else if (input == KeyCode.P)
        {
            if (currentState is InGameState)
            {
                stateManager.ChangeState(new PauseState());
            }
        }

        else if (input == KeyCode.Escape)
        {
            if (currentState is PauseState)
            {
                if (!gameObjectsManager.gameController.endGame)
                {
                    //postGameData.executionID = 3;
                    //postGameData.finalScore = null;

                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
            

                /*
                stateController.currentState.ExitState();

                if (stateController.currentState.GetType() == typeof(StateController.GameState.PreGameState))
                {
                    stateController.currentState = new StateController.GameState.InGameState();
                }
                else if (stateController.currentState.GetType() == typeof(StateController.GameState.PauseState))
                {
                    stateController.currentState = new StateController.GameState.InGameState();
                }
                else if (stateController.currentState.GetType() == typeof(StateController.GameState.ExtraTimeState))
                {
                    stateController.currentState = new StateController.GameState.InGameState();
                }
                else if (stateController.currentState.GetType() == typeof(StateController.GameState.EndGameState))
                {
                    stateController.currentState = new StateController.GameState.InGameState();
                }

                stateController.currentState.EnterState();
                
            }
            else if (input == KeyCode.P)
            {
                if (stateController.currentState.GetType() == typeof(StateController.GameState.InGameState))
                {
                    stateController.currentState.ExitState();
                    stateController.currentState = new StateController.GameState.PauseState();
                    stateController.currentState.EnterState();
                }
            }
            else if (input == KeyCode.Escape)
            {
                if (stateController.currentState.GetType() == typeof(StateController.GameState.PauseState))
                {
                    // Not sure what `endGame` is here, you might need to revise this line
                    stateController.currentState = new StateController.GameState.EndGameState();
                    stateController.currentState.ExitState();
                }
            }
          */      
        

    }
}


// The end of the match will work with an event system.


// How to difference when the game is over or not in escape
