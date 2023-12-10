using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelectData", menuName = "CallToSelecInfo")]
public class CallToSelecInfo : ScriptableObject
{
    private static int executionID = 1;
    private static string resultState = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int ExecutionID
    {
        get
        {
            return executionID;
        }
        set
        {
            executionID = value;
        }
    }

    public static string ResultState
    {
        get
        {
            return resultState;
        }
        set
        {
            resultState = value;
        }
    }

}
