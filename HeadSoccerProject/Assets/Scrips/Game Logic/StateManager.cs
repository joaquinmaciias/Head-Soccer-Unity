using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{
    private static StateManager _instance;
    IStateController currentState;

    private StateManager()
    {

    }

    public void ChangeState(IStateController newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;
        currentState.EnterState();
    }

    public IStateController GetCurrentState()
    {
        
        if (_instance == null)
        {
            _instance = this;
        }
        
        return _instance.currentState;
    }

    public static StateManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new StateManager();
        }
        return _instance;
    }
}
