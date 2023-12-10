using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;

    public List<int> score = new List<int>() { 0, 0 };
    private List<bool> isScore = new List<bool>() { false, false };

    // Static lists to store score texts for each player
    private static GameObject[] scorePlayer1;
    private static GameObject[] scorePlayer2;

    // Private constructor so that this class cannot be instantiateds

    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ScoreManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("ScoreManager");
                    instance = singletonObject.AddComponent<ScoreManager>();

                }

                instance.Initialize();

            }

            return instance;
        }
    }

    public void Initialize()
    {
        scorePlayer1 = GameObject.FindGameObjectsWithTag("ScoreP1");
        scorePlayer2 = GameObject.FindGameObjectsWithTag("ScoreP2");
    }

    /*
    public static ScoreManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ScoreManager();

            scorePlayer1 = GameObject.FindGameObjectsWithTag("ScoreP1");
        scorePlayer2 = GameObject.FindGameObjectsWithTag("ScoreP2");
        }

        return _instance;
    }
    */

    public void AddScore(int player)
    {
        score[player] += 1;
        isScore[player] = true;
        UpdateScoreText(player);
    }

    public void UpdateScoreText(int player)
    {
        if (player == 0)
        {
            foreach (var obj in scorePlayer1)
            {
                TextMeshProUGUI compText = obj.GetComponent<TextMeshProUGUI>();
                if (compText != null)
                {
                    compText.text = score[player].ToString();
                }
            }
        }
        else
        {
            foreach (var obj in scorePlayer2)
            {
                TextMeshProUGUI compText = obj.GetComponent<TextMeshProUGUI>();

                if (compText != null)
                {
                    compText.text = score[player].ToString();
                }
            }
        }
        
        
        /*
        // Update the score text
        foreach (var obj in (player == 0 ? scorePlayer1 : scorePlayer2))
        {
            TextMeshProUGUI compText = obj.GetComponent<TextMeshProUGUI>();
            if (compText != null)
            {
                compText.text = score[player].ToString();
            }
        }
        */
    }

    public List<int> GetScore()
    {
        return score;
    }
}

