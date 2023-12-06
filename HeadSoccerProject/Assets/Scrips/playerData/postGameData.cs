using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class postGameData : ScriptableObject
{
    public List<int> finalScore; // ["scoreP1", "scoreP2"] 
    public bool endMatch;

}

